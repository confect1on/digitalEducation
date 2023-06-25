namespace DigitalEducation.Entities;

public class TrainerTheory
{
    public int TrainerId { get; set; }
    
    public Trainer? Trainer { get; set; }
    
    public int TheoryId { get; set; }
    
    public Theory? Theory { get; set; }
}