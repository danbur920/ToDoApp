using System.Text.Json.Serialization;

namespace BlazorApp1.Shared.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Task>? Tasks { get; set; }
    }
}
