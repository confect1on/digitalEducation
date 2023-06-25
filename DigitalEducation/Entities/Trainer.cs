namespace DigitalEducation.Entities;

public class Trainer
{
    public int Id { get; set; }
    public ICollection<TrainerTheory> TrainerTheories { get; set; } = new List<TrainerTheory>();

    public int ProblemId { get; set; }

    public Problem? Problem { get; set; }
}