using MediatR;
using Microsoft.AspNetCore.Components.Forms; 

namespace BlazingTrails.Shared.Features.ManageTrails
{
    public record UploadTrailImageRequest(int TrailId, IBrowserFile File) : IRequest<UploadTrailImageRequest.Response> // The record is defined with two properties for trailId and the file to be uploaded
    {
        public const string RouteTemplate = "/api/trails/{trailId}/images";

        public record Response(string ImageName);
    }
}
