using alpvisionapp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace alpvisionapp.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }
        DbSet<ImportedFile> ImportedFiles { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
