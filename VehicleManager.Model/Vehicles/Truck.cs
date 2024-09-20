using VehicleManager.Model.Components;

namespace VehicleManager.Model.Vehicles;

public record Truck : Vehicle
{
    public override required string Model { get; init; }
    public override required float MaxSpeed { get; init; }
    public override decimal BasePrice { get; init; }
    public override required List<Component> Components { get; set; }
    
    public override void Accelerate(float force)
    {
        throw new NotImplementedException();
    }

    public override void Brake(float force)
    {
        throw new NotImplementedException();
    }

    public override void TurnLeft(float degrees)
    {
        throw new NotImplementedException();
    }

    public override void TurnRight(float degrees)
    {
        throw new NotImplementedException();
    }

    public override void Idle()
    {
        throw new NotImplementedException();
    }

    public override void ShiftGear(int gear)
    {
        throw new NotImplementedException();
    }
}