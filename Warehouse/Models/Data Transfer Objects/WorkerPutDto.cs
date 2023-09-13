namespace Warehouse.Models
{
    public class WorkerPutDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public List<DepartmentIdDto> DepartmentIds { get; set; } = new();
    }
}
