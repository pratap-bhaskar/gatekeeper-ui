using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatekeeperView.Models;
using k8s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GatekeeperView.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly ILogger<TemplatesController> _logger;
        private readonly IKubernetes _k8sClient;

        public TemplatesController(IKubernetes k8sClient, ILogger<TemplatesController> logger)
        {
            _logger = logger;
            _k8sClient = k8sClient;
        }

        public async Task<IActionResult> Index()
        {
            var templateResponse = await _k8sClient.GetClusterCustomObjectAsync("templates.gatekeeper.sh", "v1beta1", "constrainttemplates", "");
            var templatesAsToken = (templateResponse as JObject).SelectToken("items");
            var templates = JsonConvert.DeserializeObject<IEnumerable<ConstraintTemplate>>(templatesAsToken.ToString());
            return View(templates);
        }

        public async Task<IActionResult> Details(string name)
        {
            var templateResponse = await _k8sClient.GetClusterCustomObjectAsync("templates.gatekeeper.sh", "v1beta1", "constrainttemplates", "");
            var templatesAsToken = (templateResponse as JObject).SelectToken("items");
            var templates = JsonConvert.DeserializeObject<IEnumerable<ConstraintTemplate>>(templatesAsToken.ToString());
            return View(templates.FirstOrDefault(a => a.Metadata.Name.ToLowerInvariant() == name.ToLowerInvariant()));
        }
    }
}