
using System;
using NUnit.Framework;
using System.Collections.Generic;
using Zad2.Models;
using Zad2.Services;


namespace Zad2.Tests
{
    [TestFixture]
    public class VacationServiceTests
    {
        private VacationService _service;
        private Employee _employee;
        private VacationPackage _vacationPackage;
        private List<Vacation> _vacations;

        [SetUp]
        public void Setup()
        {
            // Uzupelnienie danych
            _employee = new Employee { Id = 1, Name = "Jan Kowalski" };
            _vacationPackage = new VacationPackage { GrantedDays = 26 };

            _vacations = new List<Vacation>
            {
                new Vacation
                {
                    EmployeeId = 1,
                    DateSince = new DateTime(DateTime.Now.Year, 1, 10),
                    DateUntil = new DateTime(DateTime.Now.Year, 1, 15)
                }
            };

            _service = new VacationService(null);
        }

        // Test - Czy pracownik moze wnioskowac o urlop - kiedy nie wykorzystal calego urlopu
        [Test]
        public void Employee_Can_Request_Vacation()
        {
            bool result = _service.IfEmployeeCanRequestVacation(_employee, _vacations, _vacationPackage);
            Assert.That(result, Is.True);
        }

        // Test - Czy pracownik moze wnioskowac o urlop - kiedy wykorzystal caly urlop
        [Test]
        public void Employee_Cant_Request_Vacation()
        {
            var fullVacations = new List<Vacation>
            {
                new Vacation
                {
                    EmployeeId = 1,
                    DateSince = new DateTime(DateTime.Now.Year, 1, 1),
                    DateUntil = new DateTime(DateTime.Now.Year, 12, 31)
                }
            };

            bool result = _service.IfEmployeeCanRequestVacation(_employee, fullVacations, _vacationPackage);
            Assert.That(result, Is.False);
        }
    }
}
