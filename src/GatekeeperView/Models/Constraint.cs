using k8s.Models;

namespace GatekeeperView.Models
{
    public class Constraint
    {
        public string ApiVersion { get; set; }
        public string Kind { get; set; }
        public V1ObjectMeta Metadata { get; set; }
        public dynamic Spec { get; set; }
        public ConstraintStatus Status { get; set; }
        
    }
}