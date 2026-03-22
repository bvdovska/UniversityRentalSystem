# System Uczelnianej Wypozyczalni Sprzetu

---

## Opis projektu
Aplikacja konsolowa w jezyku **C#** sluzaca do zarzadzania zasobami technicznymi uczelni. System pozwala na rejestracje sprzetu (**Laptopy, Projektory, Aparaty**), zarzadzanie uzytkownikami (**Studenci, Pracownicy**) oraz pelna obsluge procesu wypozyczen wraz z automatycznym naliczaniem kar za przekroczenie terminu zwrotu.

---

## Decyzje projektowe i architektura

### 1. Kohezja (Cohesion) i Zasada Jednej Odpowiedzialnosci (SRP)
Projekt zostal podzielony na logiczne warstwy, aby zapewnic wysoka spojnosc klas:
* **Warstwa Domeny (Core/Models)**: Zawiera definicje obiektow takich jak `Equipment`, `User` oraz `Rental`. Klasy te sa odpowiedzialne wylacznie za przechowywanie stanu danych.
* **Warstwa Logiki (Services)**: Klasa `RentalService` skupia w sobie calosc logiki biznesowej. Odpowiada za walidacje limitow, sprawdzanie dostepnosci sprzetu oraz wyliczanie kar.
* **Warstwa Prezentacji (Program.cs)**: Pelni role interfejsu uzytkownika. Odpowiada za wyswietlanie komunikatow i realizacje scenariusza testowego, nie ingerujac w mechanizmy logiczne systemu.

### 2. Zarzadzanie sprzezeniem (Low Coupling)
W celu zmniejszenia zaleznosci miedzy klasami zastosowano mechanizm polimorfizmu:
* Klasa bazowa **User** definiuje abstrakcyjna wlasciwosc `MaxActiveRentals`.
* Logika w **RentalService** korzysta z tej wlasciwosci w sposob ogolny. Dzieki temu serwis nie musi znac konkretnych typow uzytkownikow (Student/Pracownik), co ulatwia rozbudowe systemu o nowe role bez modyfikacji istniejącego kodu logiki.

### 3. Implementacja zasad SOLID
* **S (SRP)**: Rozdzielenie modeli danych od serwisu operacyjnego.
* **O (Open/Closed)**: System umozliwia dodawanie nowych typow urzadzen poprzez dziedziczenie po klasie `Equipment` bez koniecznosci zmian w mechanizmie wypozyczania.
* **L (Liskov Substitution)**: Klasy pochodne moga byc uzywane zamiennie w miejscach oczekujacych typu bazowego, zachowujac poprawnosc dzialania programu.

---

## Scenariusz demonstracyjny
Program w metodzie `Main` realizuje nastepujace punkty wymagane w instrukcji zadania:
* Dodanie egzemplarzy sprzetu roznych typow oraz uzytkownikow.
* Realizacja poprawnego wypozyczenia sprzetu.
* Proba wykonania operacji niepoprawnych: wypozyczenie sprzetu zajetego oraz przekroczenie limitu przez uzytkownika (**limit 2 dla Studenta**).
* Zwrot sprzetu w terminie.
* Zwrot opozniony skutkujacy naliczeniem kary wedlug wzoru: $$Penalty = DelayDays \times 15 PLN$$
* Wyswietlenie raportu podsumowujacego stan magazynowy i aktywne wypozyczenia.

---

## Instrukcja uruchomienia
1. Pobierz repozytorium na dysk lokalny.
2. Otworz plik rozwiazania (**sln**) w srodowisku **JetBrains Rider** lub **Visual Studio**.
3. Skompiluj projekt (**Build**).
4. Uruchom aplikacje (**Run**). Scenariusz testowy wyswietli przebieg operacji bezposrednio w oknie konsoli.

---

## Technologie
* **Jezyk**: C#
* **Platforma**: .NET
* **Srodowisko**: JetBrains Rider
* **System kontroli wersji**: Git