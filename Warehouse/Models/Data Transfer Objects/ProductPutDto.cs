namespace Warehouse.Models
{
    public class ProductPutDto
    {
        public int Id {  get; set; }

        public string Name { get; set; } = "";

        public int DepartmentId {  get; set; }
    }
}
