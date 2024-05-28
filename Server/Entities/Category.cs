using System.Text.Json.Serialization;

namespace BlazorApp1.Server.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual List<Task>? Tasks { get; set; }
    }
}
