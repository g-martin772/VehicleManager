using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VehicleManager.Model.Factories;
using Xunit.Abstractions;

namespace VehicleManager.Tests;

[TestSubject(typeof(VehicleFactory))]
public class VehicleFactoryTest(ITestOutputHelper testOutputHelper)
{
    private static readonly string s_Json = File.ReadAllText("../../../TestData/data.json");
    private readonly VehicleFactory m_Factory = new(s_Json);

    public static IEnumerable<object[]> GetModels() =>
        JObject.Parse(s_Json)["components"]?
            .Children()
            .SelectMany(component => component.Children())
            .SelectMany(component => component.Children())
            .OfType<JObject>()
            .Select(obj => obj["model"])
            .Where(model => model is { Type: JTokenType.String })
            .Select(model => new object[] { model!.ToString() }) ?? [];

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
    [MemberData(nameof(GetModels))]
    public void TestComponents(string model)
    {
        Assert.False(string.IsNullOrEmpty(model));

        var engine = m_Factory.GetComponent(model);
        var engineBought = m_Factory.OrderComponent(model);

        Assert.Equal(model, engine.Model);
        Assert.Null(engine.Serial);

        Assert.Equal(model, engineBought.Model);
        Assert.NotNull(engineBought.Serial);

        testOutputHelper.WriteLine(engine.ToString());
    }
}