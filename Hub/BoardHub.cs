using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using KanbanBoard.Data;
using KanbanBoard.Models;

namespace KanbanBoard.Hubs
{
    public class BoardHub : Hub
    {
        private static readonly string[] Statuses = new[] { "TODO", "In Progress", "Done" };
        private static int _online; // количество вкладок с доской
        private readonly AppDbContext _context;
        public BoardHub(AppDbContext context)
        {
            _context = context;
        }
        // подключение и отключение
        public override async Task OnConnectedAsync()
        {
            // новому клиенту отправляем всю доску из БД
            var cards = await _context.TaskCards.OrderBy(c => c.Id).ToListAsync();
            await Clients.Caller.SendAsync("Board", cards);              // только ему

            var online = Interlocked.Increment(ref _online);
            await Clients.All.SendAsync("Online", online);              // всем
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var online = Interlocked.Decrement(ref _online);
            await Clients.All.SendAsync("Online", online);              // всем
            await base.OnDisconnectedAsync(exception);
        }
        // методы клиента

        // добавить карточку
        public async Task AddCard(string title, string author)
        {
            title = (title ?? "").Trim();
            if (title.Length == 0 || title.Length > 100) return;

            var card = new TaskCard { Title = title, Author = CleanName(author), Status = "TODO",  CreatedAt = DateTime.Now};
            _context.TaskCards.Add(card);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("CardAdded", card);             // всем
            await Clients.Others.SendAsync("Notify", $"{card.Author}: добавлена задача «{card.Title}»");  // всем, кроме автора
        }

        // переместить карточку в колонку
        public async Task MoveCard(int id, string status, string author)
        {
            if (!Statuses.Contains(status)) return;

            var card = await _context.TaskCards.FindAsync(id);
            if (card == null || card.Status == status) return;
            
            card.Status = status;
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("CardMoved", id, status);
            await Clients.Others.SendAsync("Notify", $"{CleanName(author)}: задача «{card.Title}» перемещена");
        }
        // удалить карточку
        public async Task DeleteCard(int id, string author)
        {
            var card = await _context.TaskCards.FindAsync(id);
            if (card == null) return;

            _context.TaskCards.Remove(card);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("CardDeleted", id);
            await Clients.Others.SendAsync("Notify", $"{CleanName(author)}: задача «{card.Title}» удалена");
        }
        private static string CleanName(string name)
        {
            name = (name ?? "").Trim();
            if (name.Length == 0) return "Аноним";
            if (name.Length > 30) name = name.Substring(0, 30);
            return name;
        }
    }
}
