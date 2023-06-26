namespace DigitalEducation.Models;

public class ProblemValidationModel
{
    public bool IsAnswerValid { get; init; }
    
    required public Entities.ProblemWithAnswer ProblemWithAnswer { get; init; } 
}