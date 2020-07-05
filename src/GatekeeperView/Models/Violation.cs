namespace GatekeeperView.Models
{
    public class Violation
    {
        public string EnforcementAction {get;set;}
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }   
        public string Message { get; set; }
    }
}