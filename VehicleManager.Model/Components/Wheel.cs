namespace VehicleManager.Model.Components;

public enum WheelPosition
{
    Front,
    FrontRight,
    FrontLeft,
    Rear,
    RearRight,
    RearLeft
}

public record Wheel : Component
{
    public required float TreadDepth { get; set; }
    public required float MaxSpeed { get; set; }
    public required int Width { get; set; }
    public required int Height { get; set; }
    public required int Inch { get; set; }
    public required float MaxLoad { get; set; }
    public required float RatedPressure { get; set; }
    
    public float Pressure { get; set; } = 0;
    public float WearOut { get; set; } = 0;
    public WheelPosition WheelPosition { get; set; }
}