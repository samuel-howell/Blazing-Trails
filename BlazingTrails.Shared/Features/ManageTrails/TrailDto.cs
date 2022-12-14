using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BlazingTrails.Shared.Features.ManageTrails
{
    public class TrailDto // The TrailDto class will be bound to our form to collect values entered by the user
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public int TimeInMinutes { get; set; }
        public int Length { get; set; }
        public List<RouteInstruction> Route { get; set; } = new List<RouteInstruction>();

        public class RouteInstruction // is a nested class
        {
            public int Stage { get; set; }
            public string Description { get; set; } = "";
        }
    }

    // using Fluent Validation
    public class TrailValidator : AbstractValidator<TrailDto> // Validation classes must inherit from the AbstractValidator<T> class, T being the class to be validated
    {
        public TrailValidator()  // Validation rules are defined in the constructor of the validation class
        {
            // Validation rules are defined using a fluent syntax, hence the name. Each rule defines the property it’s for, the criteria, and the error message to show if the criteria isn’t met

            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a name.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description.");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a location.");
            RuleFor(x => x.Length).GreaterThan(0).WithMessage("Please enter a length.");
            RuleFor(x => x.Route).NotEmpty().WithMessage("Please add a route instruction.");

            RuleForEach(x => x.Route).SetValidator(new RouteInstructionValidator()); // wire up the RouteInstructionValidator to the Trail Validator
        }
    }

    public class RouteInstructionValidator : AbstractValidator<TrailDto.RouteInstruction>
    {
        public RouteInstructionValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description.");
            RuleFor(x => x.Stage).NotEmpty().WithMessage("Please enter a stage.");
        }
    }
}

    
