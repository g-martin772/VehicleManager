namespace VehicleManager.Model.Components;

public enum TransmissionGear
{
    Neutral,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Reverse,
    Reverse2
}

public record Transmission : Component
{
    public required (int Gear, int Ratio)[] GearRatios { get; set; }
    public required float MaxRpm { get; set; }
    public required float MaxTorque { get; set; }
    public required bool Automatic { get; set; }

    public TransmissionGear CurrentGear { get; set; }
}