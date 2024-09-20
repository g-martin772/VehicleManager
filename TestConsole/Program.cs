using System.ComponentModel;
using VehicleManager.Lib.Ticker;
using VehicleManager.Model.Components;
using VehicleManager.Model.Vehicles;
using Component = VehicleManager.Model.Components.Component;

namespace TestConsole;

class Program
{
    private static SimulationTicker Ticker { get; set; }
    static async Task Main(string[] args)
    {
        List<Vehicle> vehicles = new()
        {
            new Vehicle() {AccelerationStrength = 0.1, Model = "Vehicle1"},
            new Vehicle() {BrakeStrength = 0.2, Model = "Vehicle2"},
            new Vehicle() {AccelerationStrength = 0.3, Model = "Vehicle3", BrakeStrength = 0.1},
            new Vehicle() {AccelerationStrength = 0, Model = "Vehicle4", BrakeStrength = 1, CurrentSpeed = 350}
        };

        foreach (var vehicle in vehicles)
        {
            vehicle.Components = new List<Component>();
            vehicle.Components.Add(new Transmission()
            {
                GearRatios = new[] {(1, 3), (2, 2), (3, 1)},
                MaxRpm = 6000,
                MaxTorque = 300,
                Automatic = true,
                CurrentGear = TransmissionGear.One
            });
        }

        Ticker = new SimulationTicker(vehicles);
        await Ticker.StartAsync(new CancellationToken());
        while (true)
        {
            await Task.Delay(1500);
            foreach (var vehicle in vehicles)
            {
                var transmission = vehicle.Components.OfType<Transmission>().First();
                if(transmission.CurrentGear == TransmissionGear.Six)
                    continue;
                transmission.CurrentGear = (TransmissionGear) (int)transmission.CurrentGear + 1;

            }
        }
    }
}