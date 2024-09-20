namespace VehicleManager.Model.Components;

public record Battery : Component
{
    public required float Voltage { get; set; }
    public required float MaxPowerA { get; set; }

    public float Charge { get; set; }
}