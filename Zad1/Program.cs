using System;
using System.Collections.Generic;
using Zad1.Models;
using Zad1.Services;

namespace Zad1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Uzupelnianie danych
            var jan = new Employee { Id = 1, Name = "Jan Kowalski" };
            var kamil = new Employee { Id = 2, Name = "Kamil Nowak", Superior = jan };
            var anna = new Employee { Id = 3, Name = "Anna Mariacka", Superior = jan };
            var andrzej = new Employee { Id = 4, Name = "Andrzej Abacki", Superior = kamil };

            var employees = new List<Employee> { jan, kamil, anna, andrzej };

            var service = new EmployeeHierarchyService();

            // Wywolanie metody wypelniajacej strukture danych 
            var structures = service.FillEmployeesStructure(employees);

            // Wywolanie metody zwracajacej rzad przelozonego
            foreach (var employee in employees)
            {
                Console.WriteLine($"Pracownik: {employee.Name}");

                foreach (var e in employees)
                {
                    if (employee.Id == e.Id)
                        continue;

                    var row = service.GetSuperiorRowOfEmployee(structures, employee.Id, e.Id);
                    if (row.HasValue)
                    {
                        Console.WriteLine($"  Przelozony: {e.Name}, rzad: {row.Value}");
                    }
                }

                Console.WriteLine(); 
            }

        }
    }
}
