using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DigitalEducation.Models;

public class ProblemSolvingViewModel
{
    required public Entities.Problem Problem { get; set; }
    public int? ProblemId { get; set; }

    [DisplayName("Ответ:")] 
    [Required]
    required public string UserAnswer { get; set; } = "";
}