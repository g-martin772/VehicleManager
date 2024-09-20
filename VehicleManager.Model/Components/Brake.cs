namespace VehicleManager.Model.Components;

public record Brake : Component
{
    public required string Material { get; set; }
    public required float BrakeFluidCapacity { get; set; }
    public required float Size { get; set; }
    public required float MaxBreakForce { get; set; }
    
    public float BrakeFluidLevelL { get; set; }
    public WheelPosition WheelPosition { get; set; }
    public float WearOut { get; set; }
    public float BreakForce { get; set; }
}