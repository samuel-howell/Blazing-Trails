using BlazingTrails.Client.Features.Home;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace BlazingTrails.Api.Persistence.Entities
{
    public class Trail
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!; 
        public string Description { get; set; } = default!; 
        public string? Image { get; set; }
        public string Location { get; set; } = default!; 
        public int TimeInMinutes { get; set; }
        public int Length { get; set; }
        public ICollection<RouteInstruction> Route { get; set; } = default!;  // route is a naviagion property that will help create a one to many relationship between a trail and routeinstructions. here we are saying that a trail has many route instructions.
    }

    public class TrailConfig : IEntityTypeConfiguration<Trail> // IEntityTypeConfiguration<T> allows us to specify the configuration for the entity defined asT.
    {
        public void Configure(EntityTypeBuilder<Trail> builder) {  // IEntityType-Configuration<T> defines the Configure method; in here, rules can be specifiedfor each property on the model.
            builder.Property(x => x.Name).IsRequired();              
            builder.Property(x => x.Description).IsRequired(); 
            builder.Property(x => x.Location).IsRequired(); 
            builder.Property(x => x.TimeInMinutes).IsRequired(); 
            builder.Property(x => x.Length).IsRequired(); }
    }
}
