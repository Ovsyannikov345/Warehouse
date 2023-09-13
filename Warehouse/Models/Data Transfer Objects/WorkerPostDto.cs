namespace Warehouse.Models
{
    public class WorkerPostDto
    {
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public List<DepartmentIdDto> DepartmentIds { get; set; } = new();
    }
}
