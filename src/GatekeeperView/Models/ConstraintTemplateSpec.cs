using System.Collections.Generic;

namespace GatekeeperView.Models
{
    public class ConstraintTemplateSpec
    {
        public k8s.Models.V1CustomResourceDefinition Crd { get; set; }
        public IEnumerable<ConstraintTemplateTarget> Targets {get;set;}
    }
}