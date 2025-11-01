using System.Data.Entity;
using Zad2.Models;

public class MyDbContext : DbContext
{
    public MyDbContext() : base("name=MyDbContext")
    {
        Database.SetInitializer(new CreateDatabaseIfNotExists<MyDbContext>());
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<VacationPackage> VacationPackages { get; set; }
    public DbSet<Vacation> Vacations { get; set; }
}
