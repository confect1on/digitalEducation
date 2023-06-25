namespace DigitalEducation.Entities;

public class Test
{
    public ICollection<Problem> Problems { get; set; } = new List<Problem>();
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    
    public TimeSpan TestDuration { get; set; }
}