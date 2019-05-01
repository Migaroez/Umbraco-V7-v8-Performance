using System.Web.Http;
using Umbraco.Web.WebApi;

namespace UmbracoV7.ApiControllers
{
    public class BaselineController : UmbracoApiController
    {
        [HttpGet]
        public bool BoolRequest()
        {
            return true;
        }
    }
}