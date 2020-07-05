using System;
using System.Collections.Generic;

namespace GatekeeperView.Models
{
    public class ConstraintStatus
    {
        public DateTime AuditTimestamp { get; set; }
        public int TotalViolations { get; set; }
        public IEnumerable<Violation> Violations { get; set; }   
    }
}