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
        Vehicle myVehicle = new Vehicle()
        {
            Components = new List<Component>()
            {
                new Transmission()
                {
                    GearRatios = new[] {(0,0),(1, 7), (2, 4.2), (3, 3), (4, 2.16), (5, 1.5), (6, 0.9)},
                    FinalDriveRatio = 3.55,
                    MaxRpm = 8000,
                    MaxTorque = 10000,
                    Automatic = false,
                    CurrentGear = TransmissionGear.One
                },
                new Wheel()
                {
                    TreadDepth = 1,
                    MaxSpeed = 300,
                    Height = 20,
                    Diamater = 0.7,
                    Inch = 17,
                    MaxLoad = 1000,
                    RatedPressure = 2.5f,
                }
            },
            CurrentSpeed = 10,
            ThrottleStrength = 1
        };
        // List<Vehicle> vehicles = new()
        // {
        //     new Vehicle() {AccelerationStrength = 0.1, Model = "Vehicle1"},
        //     new Vehicle() {BrakeStrength = 0.2, Model = "Vehicle2"},
        //     new Vehicle() {AccelerationStrength = 0.3, Model = "Vehicle3", BrakeStrength = 0.1},
        //     new Vehicle() {AccelerationStrength = 0, Model = "Vehicle4", BrakeStrength = 1, CurrentSpeed = 350}
        // };


        Ticker = new SimulationTicker(new List<Vehicle>());
        Ticker.Vehicles.Add(myVehicle);
        await Ticker.StartAsync(new CancellationToken());
        while (true)
        {
            myVehicle.ThrottleStrength = myVehicle.ThrottleStrength >= 1 ? 1 : myVehicle.ThrottleStrength + 0.1;
            await Task.Delay(1500);
        }
    }
}