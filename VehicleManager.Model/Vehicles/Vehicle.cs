using VehicleManager.Model.Components;

namespace VehicleManager.Model.Vehicles;

public enum VehicleType
{
    Car,
    Truck,
    Motorcycle,
    Bicycle,
    Boat,
    Plane,
    Helicopter,
    Drone
}

public record Vehicle
{
    public VehicleType Type { get; init; }
    public string Model { get; init; }
    public float MaxSpeed { get; init; }
    public decimal BasePrice { get; init; }
    public List<Component> Components { get; set; }

    #region SimulationParameters

    public float CurrentSpeed { get; set; } = 0;
    public double Distance { get; set; } = 0;

    public double AccelerationStrength { get; set; } = 0;
    public double BrakeStrength { get; set; } = 0;

    #endregion

    public void Accelerate() {}
    public void Brake() {}
    public void TurnLeft(float degrees) {}
    public void TurnRight(float degrees) {}
    public void Idle() {}
    public void ShiftGear(int gear) {}
}