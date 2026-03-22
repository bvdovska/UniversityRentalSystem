using UniversityRentalSystem.Core.Models;

namespace UniversityRentalSystem.Services;

public class RentalService
{
    private readonly List<Equipment> _inventory = new();
    private readonly List<User> _users = new();
    private readonly List<Rental> _rentals = new();
    
    private const decimal DailyPenaltyRate = 15.00m;

    public void AddEquipment(Equipment e) => _inventory.Add(e);
    public void AddUser(User u) => _users.Add(u);

    public void ProcessRental(User user, Equipment equipment)
    {
        if (!equipment.IsAvailable || !equipment.IsFunctional)
            throw new InvalidOperationException($"Sprzęt '{equipment.Name}' jest obecnie niedostępny.");
        
        int activeRentalsCount = _rentals.Count(r => r.User.Id == user.Id && r.IsActive);
        if (activeRentalsCount >= user.MaxActiveRentals)
            throw new InvalidOperationException($"Użytkownik {user.LastName} osiągnął limit ({user.MaxActiveRentals}).");
        
        equipment.UpdateAvailability(false);
        _rentals.Add(new Rental(user, equipment, 7)); // Standardowo na 7 dni
    }

    public decimal ProcessReturn(Equipment equipment, DateTime actualReturnDate)
    {
        var rental = _rentals.FirstOrDefault(r => r.Equipment.Id == equipment.Id && r.IsActive)
            ?? throw new Exception("Nie znaleziono aktywnego wypożyczenia dla tego sprzętu.");

        decimal penalty = 0;
        if (actualReturnDate > rental.DueDate)
        {
            int delayDays = (actualReturnDate - rental.DueDate).Days;
            penalty = delayDays * DailyPenaltyRate;
        }

        rental.MarkReturned(actualReturnDate, penalty);
        equipment.UpdateAvailability(true);
        return penalty;
    }
    
    public List<Equipment> GetAllItems() => _inventory;
    public List<Equipment> GetAvailableItems() => _inventory.Where(e => e.IsAvailable && e.IsFunctional).ToList();
    public List<Rental> GetOverdueRentals() => _rentals.Where(r => r.IsActive && DateTime.Now > r.DueDate).ToList();
    public List<Rental> GetUserRentals(User u) => _rentals.Where(r => r.User.Id == u.Id && r.IsActive).ToList();
}