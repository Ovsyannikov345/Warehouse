using Newtonsoft.Json;

namespace Warehouse.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public List<Product> Products { get; set; } = new();

        public List<Worker> Workers { get; set; } = new();
    }
}
