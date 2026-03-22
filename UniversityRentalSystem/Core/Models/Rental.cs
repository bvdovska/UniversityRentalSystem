namespace UniversityRentalSystem.Core.Models;

public class Rental
{
    public Guid Id { get; } = Guid.NewGuid();
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentalDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public decimal PenaltyPaid { get; private set; }

    public bool IsActive => !ReturnDate.HasValue;

    public Rental(User user, Equipment equipment, int days)
    {
        User = user;
        Equipment = equipment;
        RentalDate = DateTime.Now;
        DueDate = DateTime.Now.AddDays(days);
    }

    public void MarkReturned(DateTime returnDate, decimal penalty)
    {
        ReturnDate = returnDate;
        PenaltyPaid = penalty;
    }
}