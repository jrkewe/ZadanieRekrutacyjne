using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

public class Team
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
