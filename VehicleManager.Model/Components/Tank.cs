namespace VehicleManager.Model.Components;

public enum FuelType
{
    None,
    Diesel,
    Super,
    SuperPlus
}

public record Tank() : Component
{
    public required FuelType FuelType { get; set; }
    public required float Capacity { get; set; }

    public float CurrentLevelLiters { get; set; }
}