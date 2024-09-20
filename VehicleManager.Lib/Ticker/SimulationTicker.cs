using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using VehicleManager.Model.Components;
using VehicleManager.Model.Vehicles;

namespace VehicleManager.Lib.Ticker;

public class SimulationTicker : IHostedService, IDisposable
{
    public List<Vehicle> Vehicles { get; set; } = new();
    private Task TickTask { get; set; }
    private CancellationTokenSource Token { get; set; }

    public SimulationTicker()
    {

    }

    public SimulationTicker(List<Vehicle> vehicles)
    {
        Vehicles = vehicles;
    }



    public Task StartAsync(CancellationToken cancellationToken)
    {
        Token = new CancellationTokenSource();
        TickTask = Task.Run(async () =>
        {
            while (!Token.IsCancellationRequested)
            {
                Console.WriteLine("Tick");
                await Tick();
                await Task.Delay(1000);
            }
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Token.CancelAsync();
        return Task.CompletedTask;
    }

    private async Task Tick()
    {
        foreach (var vehicle in Vehicles)
        {
            if (vehicle.AccelerationStrength > 0)
                AccelerateVehicle(vehicle);
            if(vehicle.BrakeStrength > 0)
                BreakVehicle(vehicle);
            Console.WriteLine(vehicle.Model + " is moving at " + vehicle.CurrentSpeed + " km/h");
        }
    }

    private void AccelerateVehicle(Vehicle vehicle)
    {
        double maxSpeed = vehicle.Components.OfType<Transmission>().First().GearMaxSpeed();
        vehicle.CurrentSpeed += (float)(50 * Math.Cbrt(vehicle.AccelerationStrength * 10));
        vehicle.CurrentSpeed = vehicle.CurrentSpeed > maxSpeed ? (float)maxSpeed : vehicle.CurrentSpeed;
    }

    private void BreakVehicle(Vehicle vehicle)
    {
        vehicle.CurrentSpeed -= (float)(50 * Math.Cbrt(vehicle.BrakeStrength * 10));
        if (vehicle.CurrentSpeed <= 0)
            vehicle.CurrentSpeed = 0;
    }


    public void Dispose()
    {
        Token.Cancel();
    }
}