using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zad2.Models
{
    public class Vacation
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateSince { get; set; }
        public DateTime DateUntil { get; set; }
        public int NumberOfHours { get; set; }
        public bool IsPartialVacation { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
