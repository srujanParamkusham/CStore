using Catalyst.MVC.Infrastructure.DataAccess.EntityFramework;
using Catalyst.MVC.Infrastructure.Models.ServiceModels;
using Catalyst.MVC.Infrastructure.Providers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using AutoMapper;
using Catalyst.MVC.Domain.Entities;
using Catalyst.MVC.Infrastructure.Util.DataTable;
using CsvHelper;
using CStore.Domain.Domains.Reports.Models.ServiceModels.SecurityUserLoginHistoryReport;
using CStore.Domain.Services.Report;
using CStore.Domain.SQLRequests;
using CStore.Domain.Entities;
using Catalyst.MVC.Infrastructure.Util.Excel;
using DocumentFormat.OpenXml.Bibliography;

namespace CStore.Domain.Domains.Reports.Services
{
    /// <summary>
    /// Service used to generate the report
    /// </summary>
    public class SecurityUserLoginHistoryReportService : DomainReportService, ISecurityUserLoginHistoryReportService
    {
        #region Internals
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sendMail"></param>
        public SecurityUserLoginHistoryReportService(IRepository repository, ISendMailProvider sendMail) : base(repository, sendMail)
        {
            _reportOutputBaseFileName = String.Format("LoginHistoryReport-{0:yyyy-MM-dd_hh-mm-ss}", DateTime.Now);
            _rdlcReportAssemblyName = "CStore.Domain.dll";
            _rdlcReportAssemblyManifestResourceName = "CStore.Domain.Domains.Reports.RDLC.SecurityUserLoginHistoryReport.rdlc";
        }
        #endregion

        /// <summary>
        /// Generate the report
        /// </summary>
        /// <param name="request"></param>
        public SecurityUserLoginHistoryReportGenerateReportResponse GenerateReport(SecurityUserLoginHistoryReportGenerateReportRequest request)
        {
            return GenerateReport<SecurityUserLoginHistoryReportGenerateReportResponse>(request);
        }

        #region OnValidateGenerateReportRequest
        /// <summary>
        /// Validate the parameters to generate the report.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override TResponse OnValidateGenerateReportRequest<TResponse>(BaseServiceGenerateReportRequest request)
        {
            var reportRequest = request as SecurityUserLoginHistoryReportGenerateReportRequest;

            //Ensure start and end date are specified. Also output type must be specified.
            if (reportRequest.StartDate == null || reportRequest.EndDate == null)
            {
                var response = Activator.CreateInstance<TResponse>();
                response.IsSuccessful = false;
                response.Message = "The from and to dates must be specified to generate the report.";
                return response;
            }
            else if (reportRequest.StartDate > reportRequest.EndDate)
            {
                var response = Activator.CreateInstance<TResponse>();
                response.IsSuccessful = false;
                response.Message = "The from date must be less than the to date.";
                return response;
            }
            else if (String.IsNullOrWhiteSpace(reportRequest.OutputFormat))
            {
                var response = Activator.CreateInstance<TResponse>();
                response.IsSuccessful = false;
                response.Message = "The output format must be specified to generate the report.";
                return response;
            }

            //Successful validation
            return null;
        }
        #endregion

        #region PopulateReportParameters
        /// <summary>
        /// Populate the parameters for the report. 
        /// </summary>
        /// <param name="request"></param>
        protected override void PopulateReportParameters(BaseServiceGenerateReportRequest request)
        {
            var reportRequest = request as SecurityUserLoginHistoryReportGenerateReportRequest;
            String reportCriteria = "";

            //Date filters
            reportCriteria += String.Format("From Date: {0:d}, ", reportRequest.StartDate);
            reportCriteria += String.Format("To Date: {0:d}, ", reportRequest.EndDate);

            //User name
            if (reportRequest.SecurityUserId != null)
            {
                var user = _repository.GetAll<SecurityUser>().FirstOrDefault(p => p.SecurityUserId == reportRequest.SecurityUserId);
                reportCriteria += String.Format("User: {0} ", user.UserName);
            }
            else
            {
                reportCriteria += String.Format("User: All ");
            }

            this.AddReportParameter("ReportCriteria", reportCriteria);
        }
        #endregion

        #region PopulateReportDataSources
        /// <summary>
        /// Populate the data sources for the report. 
        /// </summary>
        /// <param name="request"></param>
        protected override void PopulateReportDataSources(BaseServiceGenerateReportRequest request)
        {
            var reportRequest = request as SecurityUserLoginHistoryReportGenerateReportRequest;

            //
            //Ensure to cast the end date to be the date at 11:59:59.999 pm so we include the whole day of activity
            //
            DateTime? endDate = null;
            if (reportRequest.EndDate != null)
            {
                endDate = reportRequest.EndDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            //
            //Call stored proc to get data for report
            //
            var sqlRequest = new USPReportSecurityUserLoginHistorySQLRequest()
            {
                StartDate = reportRequest.StartDate,
                EndDate = endDate,
                SecurityUserId = reportRequest.SecurityUserId
            };
            var results = _repository.ExecuteList<USPReportSecurityUserLoginHistory>(sqlRequest);

            this.AddDataSource("DataSet1", results);
        }
        #endregion

        #region Generate the CSV Output
        /// <summary>
        /// Generate the CSV output version of the report.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        protected override void GenerateCSVOutput(BaseServiceGenerateReportRequest request, BaseServiceGenerateReportResponse response)
        {
            //
            //Render the report
            //
            using (var memoryStream = new MemoryStream())
            {
                //Open the write the CSV to a memory stream
                using (var writer = new StreamWriter(memoryStream))
                {
                    var csv = new CsvWriter(writer);

                    var csvRecords = new List<SecurityUserLoginHistoryReportCSVRecord>();
                    var dataSet = this.GetDataSource("DataSet1") as IEnumerable<USPReportSecurityUserLoginHistory>;

                    foreach (var dataRow in dataSet)
                    {
                        var csvRecord = new SecurityUserLoginHistoryReportCSVRecord();
                        //Map the stored proc row into the csv record
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<USPReportSecurityUserLoginHistory, SecurityUserLoginHistoryReportCSVRecord>();
                        });
                        IMapper mapper = config.CreateMapper();
                        mapper.Map(dataRow, csvRecord);

                        csvRecords.Add(csvRecord);
                    }
                    csv.WriteRecords(csvRecords);
                }

                //
                //Return the CSV data
                //
                response.IsSuccessful = true;
                response.ReportBytes = memoryStream.ToArray();
            }
        }
        #endregion

        #region Generate the Excel Output
        /// <summary>
        /// Generate the Excel output version of the report.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        protected override void GenerateExcelOutput(BaseServiceGenerateReportRequest request, BaseServiceGenerateReportResponse response)
        {
            using (var excelWriter = new ExcelWriter())
            {
                var excelRecords = new List<SecurityUserLoginHistoryReportExcelRecord>();
                var dataSet = this.GetDataSource("DataSet1") as IEnumerable<USPReportSecurityUserLoginHistory>;

                foreach (var dataRow in dataSet)
                {
                    var excelRecord = new SecurityUserLoginHistoryReportExcelRecord();
                    //Map the stored proc row into the csv record
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<USPReportSecurityUserLoginHistory, SecurityUserLoginHistoryReportExcelRecord>();
                    });
                    IMapper mapper = config.CreateMapper();
                    mapper.Map(dataRow, excelRecord);

                    excelRecords.Add(excelRecord);
                }

                var dataTableHelper = new DataTableHelper<SecurityUserLoginHistoryReportExcelRecord>();
                var dataTable = dataTableHelper.GetTable(excelRecords);

                //
                //Specify the excel formatting
                //
                var headers = new string[]
                {
                    "SecurityUserLoginHistoryId",
                    "SecurityUserId",
                    "UserName",
                    "MachineName",
                    "ApplicationCode",
                    "SuccessfulLogin",
                    "AccountWasLocked",
                    "IPAddress",
                    "Browser",
                    "ScreenResolution",
                    "Message",
                    "SessionId",
                    "SessionEndDate",
                    "LastRequestDate",
                    "SessionTimeoutDate",
                    "LastRequestUrl",
                    "CreateDate",
                    "CreateUser",
                    "ModifyDate",
                    "ModifyUser"
                };

                var format = new string[]
                {
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                };

                var columns = dataTable.Columns.Cast<DataColumn>().Where(c => c.ColumnName.ToLower() != "key").
                                  Select(column => column.ColumnName).
                                  ToArray();
                excelWriter.AddSection(headers, columns, format, dataTable, 0);

                //
                //Return the Excel data
                //
                response.IsSuccessful = true;
                response.ReportBytes = excelWriter.SaveFile();
            }
        }

        #endregion

    }
}


