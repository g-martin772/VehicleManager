namespace VehicleManager.Model.Components;

public record Component()
{
    public virtual string? Serial { get; init; }
    public virtual string? Model { get; init; }
    public virtual string Type { get; init; } = "";
    public virtual decimal Price { get; init; }
    public virtual string? Company { get; init; }

    public override string ToString()
    {
        return $"{Type} {Model}({Serial}) by {Company} for {Price:C}";
    }
}