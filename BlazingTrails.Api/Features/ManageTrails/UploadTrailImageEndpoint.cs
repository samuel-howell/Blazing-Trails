using Ardalis.ApiEndpoints;
using BlazingTrails.Api.Persistence;
using BlazingTrails.Shared.Features.ManageTrails;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Net.NetworkInformation;

namespace BlazingTrails.Api.Features.ManageTrails
{
    public class UploadTrailImageEndpoint : BaseAsyncEndpoint.WithRequest<int>.WithResponse<string>
    {
        private readonly BlazingTrailsContext _database;

        public UploadTrailImageEndpoint (BlazingTrailsContext database)
        {
            _database = database;
        }

        [HttpPost(UploadTrailImageRequest.RouteTemplate)] public override async Task<ActionResult<string>> HandleAsync([FromRoute] int trailId, CancellationToken cancellationToken = default)
        {
            // Attempts to load the trail matching the trailId and returns a bad request if it doesn’t exist
            var trail = await _database.Trails.SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken);

            if(trail is null)
            {
                return BadRequest("Trail does not exist.");
            }

            // Using the Request object, attempts to load the file posted in the request and returns a badrequest if it can’t be found
            var file = Request.Form.Files[0];
            if(file.Length == 0)
            {
                return BadRequest("No image found.");
            }

            // Creates a new filename for the uploaded image that is safe to use in the application
            var filename = $"{Guid.NewGuid()}.jpg";

            // Specifies the save location for the file
            var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "Images", filename);

            var resizeOptions = new ResizeOptions
            {
                Mode = ResizeMode.Pad, // This ImageSharp pad feature  allows the uploaded image to keep itsoriginal aspect ratio and allows additional padding to beadded to either the top and bottom or sides of the image asrequired
                Size = new SixLabors.ImageSharp.Size(640, 426)
            };

            // Using ImageSharp, resize the uploaded image to the correct dimensions and save it to thefilesystem
            using var image = Image.Load(file.OpenReadStream());
            image.Mutate(x => x.Resize(resizeOptions));
            await image.SaveAsJpegAsync(saveLocation, cancellationToken: cancellationToken);

            // Update the trail with the location of the trail image. This will be used later in the UI to loadthe image
            trail.Image = filename;
            await _database.SaveChangesAsync(cancellationToken);

            return Ok(trail.Image);
        }
    }
}
