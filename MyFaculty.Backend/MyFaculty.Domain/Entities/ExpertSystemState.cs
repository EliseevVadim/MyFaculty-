using System.Collections.Generic;

namespace MyFaculty.Domain.Entities
{
    public class ExpertSystemState
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Explanation { get; set; }
        public List<ExpertSystemAnswer> Answers { get; set; }
        public List<ExpertSystemStateTransition> ExpertSystemInitialStateTransitions { get; set; }
        public List<ExpertSystemStateTransition> ExpertSystemFinalStateTransitions { get; set; }
    }
}
