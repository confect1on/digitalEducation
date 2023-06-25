using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DigitalEducation.Models;

public class CreateTrainerViewModel
{
    [Range(1, int.MaxValue)]
    public int SectionId { get; set; }
    
    public int SubsectionId { get; set; }
    
    [DisplayName("Подраздел не указан")]
    public bool IsSubsectionNotSpecified { get; set; }
}