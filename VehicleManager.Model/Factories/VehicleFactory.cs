using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VehicleManager.Model.Components;
using VehicleManager.Model.Vehicles;

namespace VehicleManager.Model.Factories;

public class VehicleFactory
{
    public string Name { get; }
    public List<Component> AvailableComponents { get; } = [];
    public List<Vehicle> AvailableVehicles { get; } = [];
    
    public VehicleFactory(string json)
    {
        var jObject = JObject.Parse(json);

        Name = jObject["name"]?.Value<string>() ?? throw new JsonException();
        var components = jObject["components"] ?? throw new JsonException();
        var vehicles = jObject["vehicles"]?.Value<JArray>() ?? throw new JsonException();

        var assembly = typeof(VehicleFactory).Assembly;

        var componentTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Component)) && !t.IsAbstract);
        
        foreach (var type in componentTypes)
        {
            typeof(VehicleFactory).GetMethod("ParseComponents", BindingFlags.Instance | BindingFlags.NonPublic)!
                .MakeGenericMethod(type)
                .Invoke(this, new object[] { components[type.Name.ToLower()]?.Value<JArray>()! });
        }
        
        foreach (var vehicle in vehicles)
        {
            AvailableVehicles.Add(JsonConvert.DeserializeObject<Vehicle>(vehicle.ToString())!);
        }
    }

    private void ParseComponents<T>(JArray? data) where T : Component
    {
        if (data is null) return;

        foreach (var item in data)
        {
            AvailableComponents.Add(JsonConvert.DeserializeObject<T>(item.ToString())!);
        }
    }

    public Component GetComponent(string model) =>
        AvailableComponents.FirstOrDefault(c => c.Model == model)
        ?? throw new ArgumentException("Component not found");

    public Component OrderComponent(string model)
        => GetComponent(model) with { Serial = Guid.NewGuid().ToString() };

    public decimal OrderVehicle(string model, out Vehicle vehicle)
    {
        vehicle = AvailableVehicles.FirstOrDefault(v => v.Model == model)
                  ?? throw new ArgumentException("Vehicle not found");

        vehicle.Components = vehicle.Components.Select(component => OrderComponent(component.Model!)).ToList();
        var componentsPrice = vehicle.Components.Sum(c => c.Price);

        return componentsPrice + vehicle.BasePrice;
    }
}