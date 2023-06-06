using System.ComponentModel;

namespace DigitalEducation.Entities;

public class Subsection
{
    public int Id { get; set; }
    
    [DisplayName("Название")]
    required public string Name { get; set; }
    
    [DisplayName("ID раздела")]
    public int SectionId { get; set; }
    
    [DisplayName("Раздел")]
    public Section? Section { get; set; }
}