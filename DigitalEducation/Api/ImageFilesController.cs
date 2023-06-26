using DigitalEducation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DigitalEducation.Api;

[ApiController]
[Route("api/[controller]")]
public class ImageFilesController : ControllerBase
{
    private readonly IImageFileStore imageFileStore;

    public ImageFilesController(IImageFileStore imageFileStore)
    {
        this.imageFileStore = imageFileStore;
    }

    [HttpGet]
    public async Task<FileResult> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var bytes = await imageFileStore.GetFileBytesAsync(id, cancellationToken);
        var base64String = Convert.ToBase64String(bytes);
        return File(Convert.FromBase64String(base64String), "image/jpg");
    }
}