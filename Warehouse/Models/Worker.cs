namespace Warehouse.Models
{
    public class Worker
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public List<Department> Departments { get; set; } = new();
    }
}
