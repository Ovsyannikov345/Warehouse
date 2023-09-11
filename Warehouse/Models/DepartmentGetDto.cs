namespace Warehouse.Models
{
    public class DepartmentGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public List<ProductGetDto> Products { get; set; } = new();

        public List<WorkerGetDto> Workers { get; set; } = new();
    }
}
