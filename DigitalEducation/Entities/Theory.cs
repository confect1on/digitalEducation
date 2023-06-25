using System.ComponentModel;

namespace DigitalEducation.Entities;

public class Theory
{
    public int Id { get; set; }
    
    [DisplayName("Заголовок")]
    required public string Theme { get; set; }

    public ICollection<TheoryImageFile> TheoryImageFiles { get; set; } = new List<TheoryImageFile>();
    
    public int SectionId { get; set; }
    
    public Section? Section { get; set; }
    
    public int? SubsectionId { get; set; }
    
    public Subsection? Subsection { get; set; }
}