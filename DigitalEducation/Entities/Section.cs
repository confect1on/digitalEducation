using System.ComponentModel;

namespace DigitalEducation.Entities;

public class Section
{
    public int Id { get; set; }
    
    [DisplayName("Название")]
    required public string Name { get; set; }

    public ICollection<Subsection> Subsections { get; set; } = new List<Subsection>();
    
    [DisplayName("Порядковый номер")]
    public int Priority { get; set; }
}