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

    public void Accelerate(float force) {}
    public void Brake(float force) {}
    public void TurnLeft(float degrees) {}
    public void TurnRight(float degrees) {}
    public void Idle() {}
    public void ShiftGear(int gear) {}
}