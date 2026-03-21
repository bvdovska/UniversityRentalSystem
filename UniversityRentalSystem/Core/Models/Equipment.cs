namespace UniversityRentalSystem.Core.Models;

public abstract class Equipment
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; init; }
    public string SerialNumber { get; init; }
    public bool IsAvailable { get; private set; } = true;
    public bool IsFunctional { get; private set; } = true;

    protected Equipment(string name, string serialNumber)
    {
        Name = name;
        SerialNumber = serialNumber;
    }

    public void UpdateAvailability(bool status) => IsAvailable = status;
    public void UpdateFunctionalStatus(bool status) => IsFunctional = status;
}

public class Laptop : Equipment
{
    public string Processor { get; init; }
    public int RamGb { get; init; }
    public Laptop(string name, string sn, string cpu, int ram) : base(name, sn) 
    { Processor = cpu; RamGb = ram; }
}

public class Projector : Equipment
{
    public string Resolution { get; init; }
    public int Lumens { get; init; }
    public Projector(string name, string sn, string res, int lm) : base(name, sn) 
    { Resolution = res; Lumens = lm; }
}

public class Camera : Equipment
{
    public string SensorType { get; init; }
    public bool Supports4K { get; init; }
    public Camera(string name, string sn, string sensor, bool is4K) : base(name, sn) 
    { SensorType = sensor; Supports4K = is4K; }
}