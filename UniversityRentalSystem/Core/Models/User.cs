namespace UniversityRentalSystem.Core.Models;

public abstract class User
{
    public Guid Id { get; } = Guid.NewGuid();
    public string FirstName { get; init; }
    public string LastName { get; init; }
    
    public abstract int MaxActiveRentals { get; }

    protected User(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public class Student : User
{
    public Student(string fn, string ln) : base(fn, ln) { }
    public override int MaxActiveRentals => 2;
}

public class Employee : User
{
    public Employee(string fn, string ln) : base(fn, ln) { }
    public override int MaxActiveRentals => 5;
}