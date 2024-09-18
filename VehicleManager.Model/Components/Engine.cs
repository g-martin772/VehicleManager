#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
namespace VehicleManager.Model.Components;

public record Engine : Component
{
    public override string Type { get; init; } = nameof(Engine);
    public override required string Serial { get; init; }
    public override required string Model { get; init; }
    public override required decimal Price { get; init; }
    public override required string Company { get; init; }

    public required int Cylinders { get; set; }
    public required int ValvesPerCylinder { get; set; }
    public required float Displacement { get; set; }
    public required float HorsePower { get; set; }
    public required float Torque { get; set; }
    public required float Bore { get; set; }
    public required float Stroke { get; set; }
    public required float OilCapacity { get; set; }
}