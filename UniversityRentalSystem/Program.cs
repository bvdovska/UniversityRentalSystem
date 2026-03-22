using UniversityRentalSystem.Core.Models;
using UniversityRentalSystem.Services;

var service = new RentalService();

var laptop = new Laptop("MacBook Pro", "SN-LAP-01", "M3 Max", 32);
var projector = new Projector("Epson Ultra", "SN-PROJ-99", "4K", 5000);
var camera = new Camera("Canon R5", "SN-CAM-07", "Full Frame", true);

service.AddEquipment(laptop);
service.AddEquipment(projector);
service.AddEquipment(camera);

var student = new Student("Marek", "Zdolny");
var prof = new Employee("Janina", "Mądra");

service.AddUser(student);
service.AddUser(prof);

Console.WriteLine("=== SCENARIUSZ DEMONSTRACYJNY ===\n");

service.ProcessRental(student, laptop);
Console.WriteLine($"[OK] {student.LastName} wypożyczył {laptop.Name}");

try {
    service.ProcessRental(prof, laptop);
} catch (Exception ex) {
    Console.WriteLine($"[BŁĄD SPODZIEWANY]: {ex.Message}");
}

service.ProcessRental(student, projector);
try {
    service.ProcessRental(student, camera);
} catch (Exception ex) {
    Console.WriteLine($"[BŁĄD LIMITU]: {ex.Message}");
}

service.ProcessReturn(laptop, DateTime.Now.AddDays(3));
Console.WriteLine("[OK] Laptop zwrócony w terminie.");

service.ProcessRental(prof, camera);
decimal penalty = service.ProcessReturn(camera, DateTime.Now.AddDays(10));
Console.WriteLine($"[ZWROT] Kamera zwrócona. Nalioczona kara: {penalty:C2}");
Console.WriteLine("\n=== RAPORT STANU SYSTEMU ===");
Console.WriteLine($"Dostępne urządzenia: {service.GetAvailableItems().Count}");
foreach(var item in service.GetAvailableItems()) Console.WriteLine($"- {item.Name} ({item.GetType().Name})");