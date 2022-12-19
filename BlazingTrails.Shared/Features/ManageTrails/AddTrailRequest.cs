using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails
{
    public record AddTrailRequest(TrailDto Trail) : IRequest<AddTrailRequest.Response> // AddTrailRequest is defined as a C# record as opposed to a class. Records are consideredpreferable for data transfer objects due to their immutability and value type qualities. Therecord implements the IRequest<T> interface that is used by MediatR when locating ahandler. T defines the response type of the request
    {
        public const string RouteTemplate = "/api/trails"; //  This constant defines the address of the API endpoint for the request
        public record Response(int TrailId); // This nested C# record defines the response data for the request
    }

    public class AddTrailRequestValidator : AbstractValidator<AddTrailRequest> // A validator for the request. This will be executed by the API to make sure the request datais valid.
    {
        public AddTrailRequestValidator() 
        {
            RuleFor(x => x.Trail).SetValidator(new TrailValidator());  // Specifies the TrailValidator as the validator for the Trail property. This allows us to reusethe validation rules we created earlier
        }

    }
}
