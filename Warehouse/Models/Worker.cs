namespace Warehouse.Models
{
    public class Worker
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public List<Department> Departments { get; set; } = new();
    }
}
