using Microsoft.AspNetCore.Mvc;

namespace covid_api.Controllers
{
    [Route("/")]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class SwaggerController : ControllerBase
    {
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }

    }
}