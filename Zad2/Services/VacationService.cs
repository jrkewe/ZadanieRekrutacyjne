using System;
using System.Collections.Generic;
using System.Data.Entity; 
using System.Linq;
using Zad2.Models;

namespace Zad2.Services
{
    public class VacationService
    {
        private readonly MyDbContext _db;

        public VacationService(MyDbContext db)
        {
            _db = db;
        }

        // Podpunkt a
        public List<Employee> GetEmployeesWithVacation2019(string teamName)
        {
            var result = (from e in _db.Employees
                          join t in _db.Teams on e.TeamId equals t.Id
                          join v in _db.Vacations on e.Id equals v.EmployeeId
                          where t.Name == teamName &&
                                (v.DateSince.Year == 2019 || v.DateUntil.Year == 2019)
                          select e)
                         .Distinct()
                         .ToList();

            return result;
        }

        // Podpunkt b 
        public List<(string EmployeeName, int DaysUsed)> GetVacationDaysUsedCurrentYear()
        {
            int currentYear = DateTime.Now.Year;
            DateTime yearStart = new DateTime(currentYear, 1, 1);
            DateTime yearEnd = new DateTime(currentYear, 12, 31);

            var allEmployees = _db.Employees.ToList();
            var allVacations = _db.Vacations.ToList();

            var result = new List<(string EmployeeName, int DaysUsed)>();

            foreach (var emp in allEmployees)
            {
                int usedDays = allVacations
                    .Where(v => v.EmployeeId == emp.Id)
                    .Sum(v =>
                    {
                        DateTime start = v.DateSince < yearStart ? yearStart : v.DateSince;
                        DateTime end = v.DateUntil > yearEnd ? yearEnd : v.DateUntil;

                        if (end < yearStart || start > yearEnd)
                            return 0;

                        return (end - start).Days + 1;
                    });

                result.Add((emp.Name, usedDays));
            }

            return result;
        }

        // Podpunkt c
        public List<Team> GetTeamsNoVacation2019()
        {
            var result = (from t in _db.Teams
                          join e in _db.Employees on t.Id equals e.TeamId into te
                          where te.All(emp => !_db.Vacations.Any(v => v.EmployeeId == emp.Id &&
                                                                       (v.DateSince.Year == 2019 || v.DateUntil.Year == 2019)))
                          select t)
                         .ToList();

            return result;
        }

        // Zad 3
        public int CountFreeDaysForEmployee(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage)
        {
            int currentYear = DateTime.Now.Year;
            DateTime yearStart = new DateTime(currentYear, 1, 1);
            DateTime yearEnd = new DateTime(currentYear, 12, 31);

            int usedDays = vacations
                .Where(v => v.EmployeeId == employee.Id)
                .Sum(v =>
                {
                    DateTime start = v.DateSince < yearStart ? yearStart : v.DateSince;
                    DateTime end = v.DateUntil > yearEnd ? yearEnd : v.DateUntil;

                    if (end < yearStart || start > yearEnd)
                        return 0;

                    return (end - start).Days + 1;
                });

            int remainingDays = vacationPackage.GrantedDays - usedDays;
            return Math.Max(remainingDays, 0);
        }

        // Zad 4
        public bool IfEmployeeCanRequestVacation(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage)
        {
            int currentYear = DateTime.Now.Year;
            DateTime yearStart = new DateTime(currentYear, 1, 1);
            DateTime yearEnd = new DateTime(currentYear, 12, 31);

            int usedDays = vacations
                .Where(v => v.EmployeeId == employee.Id)
                .Sum(v =>
                {
                    DateTime start = v.DateSince < yearStart ? yearStart : v.DateSince;
                    DateTime end = v.DateUntil > yearEnd ? yearEnd : v.DateUntil;

                    if (end < yearStart || start > yearEnd)
                        return 0;

                    return (end - start).Days + 1;
                });

            int remainingDays = vacationPackage.GrantedDays - usedDays;

            return remainingDays > 0;
        }
    }
}