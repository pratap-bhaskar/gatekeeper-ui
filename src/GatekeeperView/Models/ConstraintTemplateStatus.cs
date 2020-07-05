using System.Collections.Generic;

namespace GatekeeperView.Models
{
    public class ConstraintTemplateStatus
    {
        public IEnumerable<ConstraintTemplatePodStatus> ByPod { get; set; }
        public bool Created { get; set; }
    }

    public class ConstraintTemplatePodStatus
    {
        public string Id { get; set; }
        public IEnumerable<ConstraintTemplateError> Errors { get; set; }

    }

    public class ConstraintTemplateError
    {
        public string Code { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
    }
}