using System.ComponentModel.DataAnnotations;
namespace KanbanBoard.Models
{
    public class TaskCard
    {
        public int Id { get; set; }

        // название задачи
        [StringLength(100)]
        public string Title { get; set; } = "";

        // статус задачи: todo, in_progress, done
        [StringLength(100)]
        public string Status { get; set; } = "todo";

        // автор задачи
        [StringLength(30)]
        public string Author { get; set; } = "";

        // исполнитель задачи
        [StringLength(30)]
        public string? Assignee { get; set; }  

        // время создания задачи
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
