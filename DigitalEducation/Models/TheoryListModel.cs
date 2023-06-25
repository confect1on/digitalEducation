using DigitalEducation.Entities;

namespace DigitalEducation.Models;

public class TheoryListModel
{
    public IList<Section> Sections { get; set; } = new List<Section>();

    public IList<Subsection> Subsections { get; set; } = new List<Subsection>();

    public IList<Entities.Theory> Theories { get; set; } = new List<Entities.Theory>();
}