namespace Zad1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? SuperiorId { get; set; }
        public Employee Superior { get; set; }
    }

}