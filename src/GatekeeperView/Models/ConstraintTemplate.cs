namespace GatekeeperView.Models
{
    public class ConstraintTemplate
    {
        public k8s.Models.V1ObjectMeta Metadata { get; set; }
        public ConstraintTemplateSpec Spec { get; set; }
        public ConstraintTemplateStatus Status { get; set; }
    }
}