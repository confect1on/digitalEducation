namespace DigitalEducation.Entities;

public class TheoryImageFile
{
    public int Id { get; set; }
    
    public int TheoryId { get; set; }
    
    public Theory? Theory { get; set; }
    
    public int ImageFileId { get; set; }
    
    public ImageFile? ImageFile { get; set; }
}