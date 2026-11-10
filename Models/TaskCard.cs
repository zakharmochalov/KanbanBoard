using System.ComponentModel.DataAnnotations;
namespace KanbanBoard.Models
{
    public class TaskCard
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        public string Title { get; set; } = "";

        [StringLength(100)]
        public string Status { get; set; } = "TODO";

        [StringLength(30)]
        public string Author { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
