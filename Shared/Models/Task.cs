namespace BlazorApp1.Shared.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime Deadline { get; set; }
        public int CategoryId { get; set; }
        public int PersonId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Person Person { get; set; }
    }
}
