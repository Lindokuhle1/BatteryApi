namespace BatteryApi;
public class Battery
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public double Voltage { get; set; }
    public double Temperature { get; set; }
    public double StateOfCharge { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}