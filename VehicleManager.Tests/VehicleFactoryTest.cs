using JetBrains.Annotations;
using VehicleManager.Model.Factories;
using Xunit.Abstractions;

namespace VehicleManager.Tests;

[TestSubject(typeof(VehicleFactory))]
public class VehicleFactoryTest(ITestOutputHelper testOutputHelper)
{
    private readonly VehicleFactory m_Factory = new("../../../TestData/data.json");

    [Fact]
    public void TestVehicles()
    {
        var models = m_Factory.AvailableVehicles.ToList();

        Assert.Single(models);
        Assert.Equal("Engine Model 1", models[0].Components.First(c => c.Type == "Engine").Model);

        testOutputHelper.WriteLine(models[0].ToString());
        
        models[0].Components
            .Select(c => $"{c.Type}: {c.Model}")
            .ToList()
            .ForEach(testOutputHelper.WriteLine);
    }

    [Theory]
    [InlineData("Engine Model 1")]
    [InlineData("Engine Model 2")]
    public void TestEngineModelsAvailable(string model)
    {
        var engine = m_Factory.GetComponent(model);

        Assert.Equal(model, engine.Model);
        Assert.Null(engine.Serial);

        testOutputHelper.WriteLine(engine.ToString());
    }

    [Theory]
    [InlineData("Engine Model 1")]
    [InlineData("Engine Model 2")]
    public void TestEngineModelsOrder(string model)
    {
        var engine = m_Factory.OrderComponent(model);

        Assert.Equal(model, engine.Model);
        Assert.NotNull(engine.Serial);

        testOutputHelper.WriteLine(engine.ToString());
    }

    [Fact]
    public void TestEnginesAvailable()
    {
        var engines = m_Factory.AvailableEngines.ToList();

        Assert.Equal(2, engines.Count);

        foreach (var engine in engines)
        {
            testOutputHelper.WriteLine(engine.ToString());
        }
    }
}