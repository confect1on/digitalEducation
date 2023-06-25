using DigitalEducation.Entities;

namespace DigitalEducation.Models;

public class TheoryListModel
{
    public IList<Section> Sections { get; set; }
    
    public IList<Subsection> Subsections { get; set; }
    
    public IList<Entities.Theory> Theories { get; set; }
}