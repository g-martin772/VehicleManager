using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VehicleManager.Model.Components;
using VehicleManager.Model.Vehicles;

namespace VehicleManager.Lib;

public class SimulationTicker(
    VehicleProvider vehicleProvider,
    ILogger<SimulationTicker> logger)
    : IHostedService, IDisposable
{
    private Timer? _timer;
    private static readonly TimeSpan TickInterval = TimeSpan.FromMilliseconds(1000);

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Vehicle Simulation started");

        _timer = new Timer(Tick, null, TimeSpan.Zero, TickInterval);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Vehicle Simulation stopped");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private void Tick(object? state)
    {
        foreach (var vehicle in vehicleProvider.Vehicles)
        {
            logger.LogInformation($"""
                                     Simulating vehicle {vehicle.Model}
                                     Speed: {vehicle.CurrentSpeed} km/h,
                                     RPM: {vehicle.Components.OfType<Engine>().First().Rpm},
                                     Gear: {vehicle.Components.OfType<Transmission>().First().CurrentGear},
                                     Distance: {Math.Round((decimal)vehicle.Distance, 3)}
                                     """);

            double increase = 0;
            if (vehicle.ThrottleStrength > 0)
                increase = CalculateSpeedIncrease(vehicle);
            // if(vehicle.BrakeStrength > 0)
            //     BreakVehicle(vehicle);
            vehicle.CurrentSpeed += (float)increase;
            vehicle.Components.OfType<Engine>().First().Rpm = (float)CalculateRPM(vehicle);
            vehicle.Distance += CalculateDistance(vehicle);
        }
    }

    private double CalculateSpeedIncrease(Vehicle vehicle)
    {
        var throttleStrength = vehicle.ThrottleStrength;

        var transmission = vehicle.Components.OfType<Transmission>().First();

        double maxEngineForce = 4000;
        double vehicleMass = 1570;
        var currentRPM = CalculateRPM(vehicle);
        var maxForceAtRPM = maxEngineForce * Math.Min(currentRPM / 5000, 1.0);
        var maxSpeedForGear = transmission.GearMaxSpeed(vehicle.Components.OfType<Wheel>().First());

        if (vehicle.CurrentSpeed >= maxSpeedForGear ||
            currentRPM > transmission.MaxRpm)
        {
            transmission.CurrentGear =
                (TransmissionGear)((int)transmission.CurrentGear >= 6 ? 6 : (int)transmission.CurrentGear + 1);
            return (int)transmission.CurrentGear < 6 ? CalculateSpeedIncrease(vehicle) : 0;
        }

        var acceleration = throttleStrength * maxForceAtRPM / vehicleMass;
        var speedIncrease = acceleration * TickInterval.Seconds;

        var speedIncreaseKMH = speedIncrease * 3.6;
        var speed = vehicle.CurrentSpeed + speedIncreaseKMH;

        if (!(speedIncreaseKMH + vehicle.CurrentSpeed > maxSpeedForGear) &&
            !(CalculateRPM(vehicle) > transmission.MaxRpm) &&
            !(speed > vehicle.MaxSpeed))
            return speedIncreaseKMH;

        transmission.CurrentGear =
            (TransmissionGear)((int)transmission.CurrentGear >= 6
                ? 6
                : (int)transmission.CurrentGear + 1); // Leave for now change later

        return (int)transmission.CurrentGear < 6 ? CalculateSpeedIncrease(vehicle) : 0;

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

        var gearRatio = transmission.GetGearRatio();
        var finalDriveRatio = transmission.FinalDriveRatio;
        var wheelDiameter = vehicle.Components.OfType<Wheel>().First().Diamater;

        double speedMetersperSeond = vehicle.CurrentSpeed * 1000 / 3600;
        var wheelRPM = speedMetersperSeond * 60 / (Math.PI * wheelDiameter);
        var engineRPM = wheelRPM * gearRatio * finalDriveRatio;

        return engineRPM;
    }

    private double CalculateDistance(Vehicle vehicle)
    {
        return vehicle.CurrentSpeed * TickInterval.Seconds / 3600;
    }
}