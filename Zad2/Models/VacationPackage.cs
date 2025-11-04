using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class VacationPackage
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int GrantedDays { get; set; }
    public int Year { get; set; }
}
