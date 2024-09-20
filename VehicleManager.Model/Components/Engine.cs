#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
namespace VehicleManager.Model.Components;

public record Engine : Component
{
    public required int Cylinders { get; set; }
    public required int ValvesPerCylinder { get; set; }
    public required float Displacement { get; set; }
    public required float HorsePower { get; set; }
    public required float Torque { get; set; }
    public required float Bore { get; set; }
    public required float Stroke { get; set; }
    public required float OilCapacity { get; set; }
    public required float CoolantCapacity { get; set; }

    public float Temp { get; set; } = 20.0f;
    public float OilLevelL { get; set; } = 1;
    public float CoolantLevelL { get; set; } = 1;
    public float OilTempC { get; set; } = 20.0f;
    public float Rpm { get; set; } = 0;
    public float WearOut { get; set; } = 0;
}