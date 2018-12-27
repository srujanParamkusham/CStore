using System;
using Catalyst.MVC.Infrastructure.Providers.Security;
using CStore.Domain.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Catalyst.MVC.Infrastructure.Attributes.Controllers;
using CStore.Domain.Domains.Example.Models.ServiceModels.Example;
using CStore.Domain.Domains.Example.Services;
using CStore.Domain.Services.State;
using CStore.Web.App_Start;

namespace CStore.Web.Controllers
{
    /// <summary>
    /// Example Web API Controller application
    /// </summary>
    public partial class ExampleApiController : DomainApiController
    {
        #region Member Variables

        /// <summary>
        /// Service to handle the controller actions
        /// </summary>
        private readonly IExampleService _service;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="securityProvider"></param>
        public ExampleApiController(IExampleService service, ISecurityProvider securityProvider)
            : base(securityProvider)
        {
            _service = service;
        }

        #endregion

        #region Public API Methods

        /// <summary>
        /// GET api/<controller> 
        /// </summary>
        /// <returns></returns>
        public List<String> Get()
        {
            var request = new ExampleServiceMethodRequest()
            {

            };
            var response = _service.ExampleServiceMethod(request);

            //Add a message to the profiler
            DomainHTTPContextService.Instance.ProfilerAdditionalInfo = String.Format("{0} users retrieved.", response.Users.Count);

            return response.Users.Select(p => p.UserName).ToList();
        }

        /// <summary>
        /// GET api/<controller>/5 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public string Get(int id)
        {
            return "value=" + id.ToString();
        }

        /// <summary>
        /// GET api/<controller>/Echo 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public string Echo(string value = null)
        {
            return value;
        }

        /// <summary>
        /// GET api/<controller>/Echo2Test/MYVALUE
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Echo2Test")]
        public string Echo2(string value = null)
        {
            return value;
        }

        /// <summary>
        /// GET api/customroute/echo3/MYVALUE
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(WebApiConfig.UrlPrefix + "/customroute/echo3/{value?}")]
        public string Echo3(string value = null)
        {
            return value;
        }

        /// <summary>
        /// GET api/<controller>/CompressionTest 
        /// Use this and view the content in Chrome Web Developer tools to see the size difference in the 
        /// compressed and raw content.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public string CompressionTest(string value = null)
        {
            //return value;
            String text = "";
            for (int i = 0; i < 100; i++)
            {
                text += "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vos squalidius, illorum vides quam niteat oratio. Iam contemni non poteris. An est aliquid, quod te sua sponte delectet? At enim sequor utilitatem. Satis est tibi in te, satis in legibus, satis in mediocribus amicitiis praesidii. Quid ergo attinet gloriose loqui, nisi constanter loquare? Hoc enim constituto in philosophia constituta sunt omnia. Duo Reges: constructio interrete. " +
                        "Verba tu fingas et ea dicas, quae non sentias? Paria sunt igitur.Quid igitur, inquit, eos responsuros putas? Non est enim vitium in oratione solum, sed etiam in moribus. " +
                        "Scaevola tribunus plebis ferret ad plebem vellentne de ea re quaeri.Qualem igitur hominem natura inchoavit ? " +
                        "Hic nihil fuit, quod quaereremus.Erit enim mecum, si tecum erit.Expectoque quid ad id, quod quaerebam, respondeas.Haec bene dicuntur, nec ego repugno, sed inter sese ipsa pugnant. " +
                        "Hoc etsi multimodis reprehendi potest, tamen accipio, quod dant.Etiam beatissimum? Tum Triarius: Posthac quidem, inquit, audacius. Non igitur bene.Nummus in Croesi divitiis obscuratur, pars est tamen divitiarum. Nihil opus est exemplis hoc facere longius.Videamus animi partes, quarum est conspectus illustrior";
            }
            return text;
        }

        /// <summary>
        /// GET api/<controller>/ErrorTest
        /// Use this to prove that the error logging is properly working. This will cause a divide by 0 exception and return a 500 server error.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string ErrorTest()
        {
            //This will cause a divide by 0 exception
            var t = 0;
            var i = 10 / t;

            return "ErrorTest";
        }

        /// <summary>
        /// POST api/<controller> 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody]string value)
        {
            //return HttpResponseHelper.GetOkResponse();
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent("Status-OK", Encoding.UTF8, "text/plain");
            return resp;
        }

        /// <summary>
        /// PUT api/<controller>/5 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            //return HttpResponseHelper.GetOkResponse();
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent("Status-OK", Encoding.UTF8, "text/plain");
            return resp;
        }

        /// <summary>
        /// DELETE api/<controller>/5 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            //return HttpResponseHelper.GetOkResponse();
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent("Status-OK", Encoding.UTF8, "text/plain");
            return resp;
        }
        #endregion

    }
}