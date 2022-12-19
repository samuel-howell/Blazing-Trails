using BlazingTrails.Shared.Features.ManageTrails;
using MediatR;
using System.Net.Http.Json;

namespace BlazingTrails.Client.Features.ManageTrails
{
    public class AddTrailHandler : IRequestHandler<AddTrailRequest, AddTrailRequest.Response> //  Request handlers implement the IRequestHandler<TRequest, TResponse> interface.TRequest is the type of request the handler handles. TResponse isthe type of the responsethe handler will return
    {
        private readonly HttpClient _httpClient;

        public AddTrailHandler(HttpClient httpClient)
        {
            _httpClient = httpClient; // An HttpClient is injected and stored in a field to be used to make API calls
        }

        public async Task<AddTrailRequest.Response> Handle (AddTrailRequest request, CancellationToken cancellationToken) // The Handler method is specified by the IRequestHandler interface and is the methodcalled to handle the request by MediatR.
        {
            var response = await _httpClient.PostAsJsonAsync(AddTrailRequest.RouteTemplate, request, cancellationToken); // The HttpClient is used to call the API using the route template we defined on the request.

            if (response.IsSuccessStatusCode)
            {
                var trailId = await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken); //  If the request was successful, then the trailId is read from the response and returned usingthe AddTrail-Request.Response record we previously defined
                return new AddTrailRequest.Response(trailId);
            }
            else
            {
                return new AddTrailRequest.Response(-1); // If the request failed, a response is returned containing a negative number. This will beused in the calling code to identify a problem
            }
        }
    }
}
