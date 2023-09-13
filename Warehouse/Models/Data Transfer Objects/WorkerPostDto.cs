namespace Warehouse.Models
{
    public class WorkerPostDto
    {
        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public List<Department> Departments { get; set; } = new();
    }
}
