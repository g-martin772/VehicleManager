namespace VehicleManager.Model.Components;

public record Component()
{
    public string? Serial { get; init; }
    public string? Model { get; init; }
    public string Type { get; init; } = "";
    public decimal Price { get; init; }
    public string? Company { get; init; }

    public override string ToString()
    {
        return $"{Type} {Model}({Serial}) by {Company} for {Price:C}";
    }
}