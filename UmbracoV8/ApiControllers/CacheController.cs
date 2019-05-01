using System.Linq;
using System.Web.Http;
using Umbraco.Web;
using Umbraco.Web.WebApi;

namespace UmbracoV8.ApiControllers
{
    public class CacheController : UmbracoApiController
    {
        [HttpGet]
        public int NumberOfTestNodes()
        {
            return Umbraco.ContentAtRoot().First().Descendants().Count();
        }

        [HttpGet]
        public int NumberOfTestNodesContainsInName(string searchString)
        {
            return Umbraco.ContentAtRoot().First().Descendants().Count(d => d.Name.Contains(searchString));
        }
    }
}