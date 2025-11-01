using System;
using System.Collections.Generic;
using System.Linq;
using Zad2.Models;
using Zad2.Services;

namespace Zad2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MyDbContext())
            {
                // Usuwanie i tworzenie nowej bazy danych
                db.Database.Delete();
                db.Database.Create();

                var team1 = new Team { Name = ".NET" };
                var team2 = new Team { Name = "Java" };
                db.Teams.AddRange(new List<Team> { team1, team2 });
                db.SaveChanges();

                var vp1 = new VacationPackage { Name = "Standard", GrantedDays = 26, Year = 2025 };
                db.VacationPackages.Add(vp1);
                db.SaveChanges();

                var emp1 = new Employee { Name = "Jan Kowalski", TeamId = team1.Id, VacationPackageId = vp1.Id };
                var emp2 = new Employee { Name = "Anna Nowak", TeamId = team1.Id, VacationPackageId = vp1.Id };
                var emp3 = new Employee { Name = "Tomasz Lis", TeamId = team2.Id, VacationPackageId = vp1.Id };
                db.Employees.AddRange(new List<Employee> { emp1, emp2, emp3 });
                db.SaveChanges();

                var vac1 = new Vacation
                {
                    EmployeeId = emp1.Id,
                    DateSince = new DateTime(2019, 1, 5),
                    DateUntil = new DateTime(2019, 1, 15),
                    NumberOfHours = 0,
                    IsPartialVacation = false
                };
                var vac2 = new Vacation
                {
                    EmployeeId = emp2.Id,
                    DateSince = new DateTime(2024, 12, 25),
                    DateUntil = new DateTime(2025,1, 5),
                    NumberOfHours = 0,
                    IsPartialVacation = false
                };
                db.Vacations.AddRange(new List<Vacation> { vac1, vac2 });
                db.SaveChanges();

                // Wywolanie serwisu
                var service = new VacationService(db);

                // Zad 2: a, b, c; wywolanie metod
                var employees2019 = service.GetEmployeesWithVacation2019(".NET");
                var daysUsed = service.GetVacationDaysUsedCurrentYear();
                var teamsNoVacation = service.GetTeamsNoVacation2019();

                // Wyswietlenie wynikow
                Console.WriteLine("Pracownicy z zespolu .NET, ktorzy wnioskowali o urlop w 2019:");
                foreach (var e in employees2019)
                    Console.WriteLine($"- {e.Name}");

                Console.WriteLine("\nPracownicy i ich dni urlopowe wykorzystane w biezacym roku:");
                foreach (var x in daysUsed)
                    Console.WriteLine($"- {x.EmployeeName}: {x.DaysUsed} dni");

                Console.WriteLine("\nZespoly bez urlopow w 2019:");
                foreach (var t in teamsNoVacation)
                    Console.WriteLine($"- {t.Name}");

                // Wczytanie danych z db
                var allEmployees = db.Employees.ToList();
                var allVacations = db.Vacations.ToList();
                var allPackages = db.VacationPackages.ToList();

                // Zad 3
                var remainingDays = new List<(string EmployeeName, int FreeDays)>();

                foreach (var emp in allEmployees)
                {
                    var vp = allPackages.First(v => v.Id == emp.VacationPackageId);
                    int freeDays = service.CountFreeDaysForEmployee(emp, allVacations, vp);
                    remainingDays.Add((emp.Name, freeDays));
                }

                Console.WriteLine("\nIlosc dni urlopowych do wykorzystania przez pracownika w biezacym roku:");
                foreach (var x in remainingDays)
                {
                    Console.WriteLine($"- {x.EmployeeName}: {x.FreeDays} dni");
                }

                // Zad 4
                Console.WriteLine("\nCzy pracownik moze zglosic wniosek urlopowy?");
                foreach (var emp in allEmployees)
                {
                    var vp = allPackages.First(v => v.Id == emp.VacationPackageId);
                    bool canRequest = service.IfEmployeeCanRequestVacation(emp, allVacations, vp);
                    Console.WriteLine($"- {emp.Name}: {(canRequest ? "Tak" : "Nie")}");
                }
            }

            Console.WriteLine("\nTest zakonczony. Nacisnij dowolny klawisz...");
            Console.ReadKey();

            }
        }
    }





