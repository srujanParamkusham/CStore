// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace CStore.Web.Areas.Admin.Controllers
{
    public partial class AppEmailTemplateMaintenanceController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AppEmailTemplateMaintenanceController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Index()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Delete()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult DownloadFile()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DownloadFile);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AppEmailTemplateMaintenanceController Actions { get { return MVC.Admin.AppEmailTemplateMaintenance; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "AppEmailTemplateMaintenance";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "AppEmailTemplateMaintenance";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string List = "List";
            public readonly string Edit = "Edit";
            public readonly string Delete = "Delete";
            public readonly string DownloadFile = "DownloadFile";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string List = "List";
            public const string Edit = "Edit";
            public const string Delete = "Delete";
            public const string DownloadFile = "DownloadFile";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_List s_params_List = new ActionParamsClass_List();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_List ListParams { get { return s_params_List; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_List
        {
            public readonly string model = "model";
            public readonly string dataTablesModel = "dataTablesModel";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Delete s_params_Delete = new ActionParamsClass_Delete();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Delete DeleteParams { get { return s_params_Delete; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Delete
        {
            public readonly string ids = "ids";
        }
        static readonly ActionParamsClass_DownloadFile s_params_DownloadFile = new ActionParamsClass_DownloadFile();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DownloadFile DownloadFileParams { get { return s_params_DownloadFile; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DownloadFile
        {
            public readonly string mimeType = "mimeType";
            public readonly string downloadedFileName = "downloadedFileName";
            public readonly string bytes = "bytes";
            public readonly string dispositionType = "dispositionType";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string Edit = "Edit";
                public readonly string Index = "Index";
            }
            public readonly string Edit = "~/Areas/Admin/Views/AppEmailTemplateMaintenance/Edit.cshtml";
            public readonly string Index = "~/Areas/Admin/Views/AppEmailTemplateMaintenance/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_AppEmailTemplateMaintenanceController : CStore.Web.Areas.Admin.Controllers.AppEmailTemplateMaintenanceController
    {
        public T4MVC_AppEmailTemplateMaintenanceController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, CStore.Domain.Domains.Admin.Models.ViewModels.AppEmailTemplateMaintenance.AppEmailTemplateMaintenanceListViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(CStore.Domain.Domains.Admin.Models.ViewModels.AppEmailTemplateMaintenance.AppEmailTemplateMaintenanceListViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            IndexOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void ListOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, CStore.Domain.Domains.Admin.Models.ViewModels.AppEmailTemplateMaintenance.AppEmailTemplateMaintenanceListViewModel model, Catalyst.MVC.Infrastructure.Models.JQueryDataTables.JQueryDataTablesParameterModel dataTablesModel);

        [NonAction]
        public override System.Web.Mvc.ActionResult List(CStore.Domain.Domains.Admin.Models.ViewModels.AppEmailTemplateMaintenance.AppEmailTemplateMaintenanceListViewModel model, Catalyst.MVC.Infrastructure.Models.JQueryDataTables.JQueryDataTablesParameterModel dataTablesModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.List);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dataTablesModel", dataTablesModel);
            ListOverride(callInfo, model, dataTablesModel);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? id, CStore.Domain.Domains.Admin.Models.ViewModels.AppEmailTemplateMaintenance.AppEmailTemplateMaintenanceEditViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(int? id, CStore.Domain.Domains.Admin.Models.ViewModels.AppEmailTemplateMaintenance.AppEmailTemplateMaintenanceEditViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditOverride(callInfo, id, model);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, CStore.Domain.Domains.Admin.Models.ViewModels.AppEmailTemplateMaintenance.AppEmailTemplateMaintenanceEditViewModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(CStore.Domain.Domains.Admin.Models.ViewModels.AppEmailTemplateMaintenance.AppEmailTemplateMaintenanceEditViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void DeleteOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Collections.Generic.IEnumerable<int> ids);

        [NonAction]
        public override System.Web.Mvc.ActionResult Delete(System.Collections.Generic.IEnumerable<int> ids)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Delete);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "ids", ids);
            DeleteOverride(callInfo, ids);
            return callInfo;
        }

        [NonAction]
        partial void DownloadFileOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string mimeType, string downloadedFileName, byte[] bytes);

        [NonAction]
        public override System.Web.Mvc.ActionResult DownloadFile(string mimeType, string downloadedFileName, byte[] bytes)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DownloadFile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "mimeType", mimeType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "downloadedFileName", downloadedFileName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "bytes", bytes);
            DownloadFileOverride(callInfo, mimeType, downloadedFileName, bytes);
            return callInfo;
        }

        [NonAction]
        partial void DownloadFileOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string dispositionType, string mimeType, string downloadedFileName, byte[] bytes);

        [NonAction]
        public override System.Web.Mvc.ActionResult DownloadFile(string dispositionType, string mimeType, string downloadedFileName, byte[] bytes)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DownloadFile);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "dispositionType", dispositionType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "mimeType", mimeType);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "downloadedFileName", downloadedFileName);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "bytes", bytes);
            DownloadFileOverride(callInfo, dispositionType, mimeType, downloadedFileName, bytes);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
