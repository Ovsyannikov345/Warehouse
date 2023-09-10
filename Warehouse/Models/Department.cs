namespace Warehouse.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public List<Worker> Workers { get; set; } = new();
    }
}
