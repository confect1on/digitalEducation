using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalEducation.Entities;
using Microsoft.Build.Framework;

namespace DigitalEducation.Models.Theory;


public class TheoryCreateViewModel
{
    [Required]
    [DisplayName("Заголовок")]
    required public string Title { get; init; }

    [Required]
    [NotMapped]
    [DisplayName("Изображения теории")]
    required public IFormFileCollection FormFileCollection { get; init; }

    [Required]
    [DisplayName("ID секции")]
    public int SectionId { get; init; }

    [DisplayName("ID подсекции")]
    public int? SubsectionId { get; init; }
    
    [DisplayName("Подраздел отсутствует")]
    public bool IsSubsectionNotSpecified { get; init; }
}