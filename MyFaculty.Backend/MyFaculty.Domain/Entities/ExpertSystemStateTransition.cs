namespace MyFaculty.Domain.Entities
{
    public class ExpertSystemStateTransition
    {
        public int Id { get; set; }
        public int InitialStateId { get; set; }
        public int FinalStateId { get; set; }
        public bool IsLast { get; set; }
        public int AnswerId { get; set; }
        public ExpertSystemState InitialState { get; set; }
        public ExpertSystemState FinalState { get; set; }
        public ExpertSystemAnswer Answer { get; set; }
    }
}
