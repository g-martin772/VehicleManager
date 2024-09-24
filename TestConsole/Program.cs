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
        Vehicle vehicle = new Vehicle()
        {
            Components = new List<Component>()
            {
                new Transmission()
                {
                    GearRatios = new[] {(0,0),(1, 5.3), (2, 3.5), (3, 2.3), (4, 1.65), (5, 1.15), (6, 0.75)},
                    FinalDriveRatio = 3.55,
                    MaxRpm = 8000,
                    MaxTorque = 10000, // except for this one
                    Automatic = false,  // and this one
                    CurrentGear = TransmissionGear.Four // Not TestData :3
                },
                new Wheel()
                {
                    TreadDepth = 1,
                    MaxSpeed = 300,
                    Height = 20,
                    Diamater = 0.7,
                    Inch = 17,
                    MaxLoad = 1000,
                    RatedPressure = 2.5f, // testData
                },
                new Engine()
                {
                    Cylinders = 4,
                    ValvesPerCylinder = 4,
                    Displacement = 2.0f,
                    HorsePower = 200,
                    Torque = 200,
                    Bore = 80,
                    Stroke = 80,
                    OilCapacity = 5,
                    CoolantCapacity = 5 // TestData
                }
            },
            CurrentSpeed = 100,
            ThrottleStrength = 1,
            MaxSpeed = 292
        };


        Ticker = new SimulationTicker(new List<Vehicle>());
        Ticker.Vehicles.Add(vehicle);
        await Ticker.StartAsync(new CancellationToken());
        while (true)
        {
            // myVehicle.ThrottleStrength = myVehicle.ThrottleStrength >= 1 ? 1 : myVehicle.ThrottleStrength + 0.1;
            await Task.Delay(1500);
            Console.WriteLine($"Speed: {vehicle.CurrentSpeed} km/h, \nRPM: {vehicle.Components.OfType<Engine>().First().Rpm}, \nGear: {vehicle.Components.OfType<Transmission>().First().CurrentGear}\nDistance: {Math.Round((decimal) vehicle.Distance, 3)}km\n");

        }
    }
}