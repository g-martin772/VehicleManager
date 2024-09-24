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
    private double deltaTime = 0.01;
    private readonly int waitTime = 10;

    public SimulationTicker()
    {
    }

    public SimulationTicker(List<Vehicle> vehicles) : base()
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
                await Tick();
                await Task.Delay(waitTime, Token.Token);
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
            double increase = 0;
            if (vehicle.ThrottleStrength > 0)
                increase = CalculateSpeedIncrease(vehicle);
            // if(vehicle.BrakeStrength > 0)
            //     BreakVehicle(vehicle);
            vehicle.CurrentSpeed += (float)increase;
            vehicle.Components.OfType<Engine>().First().Rpm = (float) CalculateRPM(vehicle);
            vehicle.Distance += CalculateDistance(vehicle);
        }
    }

    private double CalculateSpeedIncrease(Vehicle vehicle)
    {
        double throttleStrength = vehicle.ThrottleStrength;

        var transmission = vehicle.Components.OfType<Transmission>().First();

        double maxEngineForce = 4000;
        double vehicleMass = 1570;
        double currentRPM = CalculateRPM(vehicle);
        double maxForceAtRPM = maxEngineForce * Math.Min(currentRPM / 5000, 1.0);
        double maxSpeedForGear = transmission.GearMaxSpeed(vehicle.Components.OfType<Wheel>().First());

        if (vehicle.CurrentSpeed >= maxSpeedForGear ||
            currentRPM > transmission.MaxRpm)
        {
            transmission.CurrentGear = (TransmissionGear) ((int)transmission.CurrentGear >= 6 ? 6 : (int)transmission.CurrentGear + 1);
            return (int)transmission.CurrentGear < 6 ? CalculateSpeedIncrease(vehicle) : 0;
        }

        double acceleration = (throttleStrength * maxForceAtRPM) / vehicleMass;
        double speedIncrease = acceleration * deltaTime;

        double speedIncreaseKMH = speedIncrease * 3.6;
        double speed = vehicle.CurrentSpeed + speedIncreaseKMH;
        if(speedIncreaseKMH + vehicle.CurrentSpeed > maxSpeedForGear || CalculateRPM(vehicle) > transmission.MaxRpm ||speed > vehicle.MaxSpeed)
        {
            transmission.CurrentGear = (TransmissionGear) ((int)transmission.CurrentGear >= 6 ? 6 : (int)transmission.CurrentGear + 1); // Leave for now change later
            return (int)transmission.CurrentGear < 6 ? CalculateSpeedIncrease(vehicle) : 0;
        }

        return speedIncreaseKMH;

    }

    private void BreakVehicle(Vehicle vehicle)
    {
        vehicle.CurrentSpeed -= (float)(50 * Math.Cbrt(vehicle.BrakeStrength * 10));
        if (vehicle.CurrentSpeed <= 0)
            vehicle.CurrentSpeed = 0;
    }

    private double CalculateRPM(Vehicle vehicle)
    {
        var transmission = vehicle.Components.OfType<Transmission>().First();

        double gearRatio = transmission.GetGearRatio();
        double finalDriveRatio = transmission.FinalDriveRatio;
        double wheelDiameter = vehicle.Components.OfType<Wheel>().First().Diamater;

        double speedMetersperSeond = vehicle.CurrentSpeed  * 1000 / 3600;
        double wheelRPM = (speedMetersperSeond * 60) / (Math.PI * wheelDiameter);
        double engineRPM = wheelRPM * gearRatio * finalDriveRatio;

        return engineRPM;
    }

    private double CalculateDistance(Vehicle vehicle) => vehicle.CurrentSpeed * deltaTime / 3600;


    public void Dispose()
    {
        Token.Cancel();
    }
}