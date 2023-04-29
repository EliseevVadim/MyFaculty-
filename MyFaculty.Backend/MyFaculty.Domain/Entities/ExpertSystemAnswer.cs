namespace MyFaculty.Domain.Entities
{
    public class ExpertSystemAnswer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? StateTransitionId { get; set; }
        public int StateId { get; set; }
        public ExpertSystemState ExpertSystemState { get; set; }
        public ExpertSystemStateTransition ExpertSystemStateTransition { get; set; }
    }
}
