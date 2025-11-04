using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;         
using System.ComponentModel.DataAnnotations.Schema;
using Zad2.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    [ForeignKey("Team")]
    public int TeamId { get; set; }
    public Team Team { get; set; }

    [ForeignKey("VacationPackage")]
    public int VacationPackageId { get; set; }
    public VacationPackage VacationPackage { get; set; }
}
