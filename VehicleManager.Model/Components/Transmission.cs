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
    public required (int Gear, double Ratio)[] GearRatios { get; set; }
    public required double FinalDriveRatio { get; set; }
    public required float MaxRpm { get; set; }
    public required float MaxTorque { get; set; }
    public required bool Automatic { get; set; }

    public TransmissionGear CurrentGear { get; set; }

    public double GetGearRatio()
    {
        return GearRatios[(int)CurrentGear].Ratio;
    }

    public double GearMaxSpeed(Wheel wheel)
    {
        double maxWheelRPM = MaxRpm / (GearRatios[(int)CurrentGear].Ratio * FinalDriveRatio);
        double maxSpeed = (maxWheelRPM * Math.PI * wheel.Diamater * 60) / 1000;
        return maxSpeed;
    }
}