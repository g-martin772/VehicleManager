namespace VehicleManager.Model.Components;

public enum TransmissionGear
{
    Neutral = 0,
    One = 1,
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

    public double GearMaxSpeed()
    {
        switch ((int)CurrentGear)
        {
            case 0:
                return 0;
            case 1:
                return 30;
            case 2:
                return 80;
            case > 2:
                return 50 *(int) CurrentGear;
        }

        throw new InvalidOperationException();
    }
}