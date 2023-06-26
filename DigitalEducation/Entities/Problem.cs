using System.ComponentModel;

namespace DigitalEducation.Entities;

public class Problem
{
    public int Id { get; set; }
    
    [DisplayName("Задание")]
    required public string Question { get; set; }
    
    public int? ProblemImageFileId { get; set; }
    
    [DisplayName("Изображение задания")]
    public ImageFile? ProblemImageFile { get; set; }
    
    [DisplayName("Ответ")]
    required public string Answer { get; set; }
    
    [DisplayName("ID файла")]
    public int SolutionImageFileId { get; set; }
    
    [DisplayName("Файл решения")]
    public ImageFile? SolutionImageFile { get; set; }
    
    public int? HintImageFileId { get; set; }
    
    [DisplayName("Файл подсказки")]
    public ImageFile? HintImageFile { get; set; }

    [DisplayName("ID Подраздела")]
    public int SubsectionId { get; set; }
    
    [DisplayName("Подраздел")]
    public Subsection? Subsection { get; set; }
}