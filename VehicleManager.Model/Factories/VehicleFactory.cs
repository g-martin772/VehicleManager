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

    public IEnumerable<Engine> AvailableEngines => AvailableComponents.OfType<Engine>();

    public VehicleFactory(string filePath)
    {
        var jsonString = File.ReadAllText(filePath);
        var jObject = JObject.Parse(jsonString);
        
        Name = jObject["name"]?.Value<string>() ?? throw new JsonException();
        var components = jObject["components"]?.Value<JArray>() ?? throw new JsonException();
        var vehicles = jObject["vehicles"]?.Value<JArray>() ?? throw new JsonException();

        foreach (var component in components)
        {
            switch (component["type"]?.Value<string>() ?? throw new JsonException())
            {
                case "Engine":
                    AvailableComponents.Add(JsonConvert.DeserializeObject<Engine>(component.ToString())!);
                    break;
            }
        }
        
        foreach (var vehicle in vehicles)
        {
            AvailableVehicles.Add(JsonConvert.DeserializeObject<Vehicle>(vehicle.ToString())!);
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
        
        vehicle.Components = vehicle.Components.Select(component => OrderComponent(component.Model)).ToList();
        var componentsPrice = vehicle.Components.Sum(c => c.Price);
        
        return componentsPrice + vehicle.BasePrice;
    }
}