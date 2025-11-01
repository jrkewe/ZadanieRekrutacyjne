using System;
using System.Collections.Generic;
using System.Linq;
using Zad1.Models;

namespace Zad1.Services
{
    public class EmployeeHierarchyService
    {
        // Wypelnianie struktury danych
        public List<EmployeeStructure> FillEmployeesStructure(List<Employee> employees)
        {
            var structure = new List<EmployeeStructure>();

            foreach (var employee in employees)
            {
                var superior = employee.Superior;
                int row = 1;

                while (superior != null)
                {
                    structure.Add(new EmployeeStructure
                    {
                        EmployeeId = employee.Id,
                        SuperiorId = superior.Id,
                        Row = row
                    });

                    superior = superior.Superior;
                    row++;
                }
            }

            return structure;
        }

        // Metoda zwracajaca rzad przelozonego
        public int? GetSuperiorRowOfEmployee(List<EmployeeStructure> structure, int employeeId, int superiorId)
        {
            return structure
                .FirstOrDefault(x => x.EmployeeId == employeeId && x.SuperiorId == superiorId)
                ?.Row;
        }
    }
}
