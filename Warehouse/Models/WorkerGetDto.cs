namespace Warehouse.Models
{
    public class WorkerGetDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public List<DepartmentGetDto> Departments { get; set; } = new();
    }
}
