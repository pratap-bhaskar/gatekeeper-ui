using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GatekeeperView.Models;
using k8s;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace GatekeeperView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IKubernetes _k8sClient;
        public HomeController(IKubernetes k8sClient, ILogger<HomeController> logger)
        {
            _logger = logger;
            _k8sClient = k8sClient;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation($"Fetching templates.gatekeeper.sh/v1beta1 from the cluster");
            var templateResponse = await _k8sClient.GetClusterCustomObjectAsync("templates.gatekeeper.sh", "v1beta1", "constrainttemplates", "");
            var templatesAsToken = (templateResponse as JObject).SelectToken("items");
            var templates = JsonConvert.DeserializeObject<IEnumerable<ConstraintTemplate>>(templatesAsToken.ToString());
            var listOfConstraints = new List<Constraint>();

            foreach (var template in templates)
            {
                var constraintResponse = await _k8sClient.GetClusterCustomObjectAsync("constraints.gatekeeper.sh", "v1beta1", template.Metadata.Name, "");
                var constraints = JsonConvert.DeserializeObject<IEnumerable<Constraint>>(
                    ((constraintResponse as JObject).SelectToken("items")).ToString()
                );
                listOfConstraints.AddRange(constraints);
            }

            return View(listOfConstraints.OrderByDescending(a => a.Status.TotalViolations));
        }

        public async Task<IActionResult> Details(string name, string template)
        {
            var constraintResponse = await _k8sClient.GetClusterCustomObjectAsync("constraints.gatekeeper.sh", 
                "v1beta1", template.ToLowerInvariant(), name);
            return View(JsonConvert.DeserializeObject<Constraint>(constraintResponse.ToString()));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
             Message = exceptionHandlerPathFeature.Error.Message });
        }
       
    }
}