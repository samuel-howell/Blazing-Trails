using BlazingTrails.Shared.Features.ManageTrails;
using MediatR;

namespace BlazingTrails.Client.Features.ManageTrails.Shared
{
    public class UploadTrailImageHandler : IRequestHandler<UploadTrailImageRequest, UploadTrailImageRequest.Response>
    {
        private readonly HttpClient _httpClient;

        public UploadTrailImageHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UploadTrailImageRequest.Response> Handle(UploadTrailImageRequest request, CancellationToken cancellationToken)
        {
            var fileContent = request.File.OpenReadStream(request.File.Size, cancellationToken); //  The IBrowserFile type includes a helper method that allows the file to be read as a stream.

            using var content = new MultipartFormDataContent(); // A MultipartFormDataContent type is created, and the file is added to it
            content.Add(new StreamContent(fileContent), "image", request.File.Name);

            var response = await _httpClient.PostAsync(UploadTrailImageRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString()), content, cancellationToken); // The file is posted to the API.

            if (response.IsSuccessStatusCode)
            {
                var fileName = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken); // If the upload was successful, the API response is deserialized and returned.
                return new UploadTrailImageRequest.Response(fileName);
            }
            else
            {
                return new UploadTrailImageRequest.Response(""); //  If the upload failed, a response containing an empty string is returned
            }
        }
    }
}
