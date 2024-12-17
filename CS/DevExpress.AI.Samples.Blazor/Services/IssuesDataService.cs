using DevExpress.AI.Samples.Blazor.Data;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.AI.Samples.Blazor.Services {
    public class IssuesDataService {
        IDbContextFactory<IssuesContext> DbFactory;

        public IssuesDataService(IDbContextFactory<IssuesContext> DbFactory) {
            this.DbFactory = DbFactory;
        }

        public Task<List<Issue>> GetIssuesAsync(CancellationToken ct = default) {
            return DbFactory.CreateDbContext().Items.ToListAsync();
        }

        public Task<List<Project>> GetProjectsAsync(CancellationToken ct = default) {
            return DbFactory.CreateDbContext().Projects.ToListAsync();
        }

        public Task<List<User>> GetUsersAsync(CancellationToken ct = default) {
            return DbFactory.CreateDbContext().Users.ToListAsync();
        }
    }
}
