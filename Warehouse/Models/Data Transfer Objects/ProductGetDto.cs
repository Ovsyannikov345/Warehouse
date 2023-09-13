namespace Warehouse.Models
{
    public class ProductGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public DepartmentGetDto? Department { get; set; }
    }
}
