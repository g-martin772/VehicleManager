using VehicleManager.Model.Components;

namespace VehicleManager.Model.Vehicles;

public abstract record Vehicle
{
    public abstract string Model { get; init; }
    public abstract float MaxSpeed { get; init; }
    public abstract decimal BasePrice { get; init; }
    public abstract List<Component> Components { get; set; }

    public abstract void Accelerate(float force);
    public abstract void Brake(float force);
    public abstract void TurnLeft(float degrees);
    public abstract void TurnRight(float degrees);
    public abstract void Idle();
    public abstract void ShiftGear(int gear);
}