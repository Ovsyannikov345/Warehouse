namespace Warehouse.Models
{
    public class DepartmentPutDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public List<WorkerIdDto> WorkerIds { get; set; } = new();

        public List<ProductIdDto> ProductIds { get; set; } = new();
    }
}
