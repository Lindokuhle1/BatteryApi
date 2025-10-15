using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BatteryApi;

public class BatteryRepository
{
    private readonly ConcurrentDictionary<string, Battery> _data = new();

    public IEnumerable<Battery> GetAll() => _data.Values;

    public Battery? Get(string id) => _data.GetValueOrDefault(id);

    public Battery Add(Battery battery)
    {
        _data[battery.Id] = battery;
        return battery;
    }

    public Battery AddRandom()
    {
        var b = new Battery
        {
            Voltage = Math.Round(Random.Shared.NextDouble() * 10 + 48, 2),
            Temperature = Math.Round(Random.Shared.NextDouble() * 20 + 25, 1),
            StateOfCharge = Math.Round(Random.Shared.NextDouble() * 100, 1)
        };
        _data[b.Id] = b;
        return b;
    }

    public bool Delete(string id) => _data.TryRemove(id, out _);
}
