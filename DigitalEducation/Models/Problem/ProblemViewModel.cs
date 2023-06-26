using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DigitalEducation.Models.Problem;

/// <summary>
/// Represents a view model for creating a problem.
/// </summary>
public class ProblemViewModel
{
    [Required]
    [DisplayName("Задание")]
    required public string Question { get; set; }
    
    [DisplayName("Изображение")]
    public IFormFile? Picture { get; set; }
    
    [Required]
    [DisplayName("Решение")]
    required public IFormFile SolutionPicture { get; set; }
    
    [Required]
    [DisplayName("Ответ")]
    required public string Answer { get; set; }
    
    [Required]
    [DisplayName("ID Подраздела")]
    public int SubsectionId { get; set; }
    
    [DisplayName("Подсказка")]
    public IFormFile? HintPicture { get; set; }
}