using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogiAppMonitor.API.Helpers;
using LogiAppMonitor.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LogiAppMonitor.API.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DataContext _context;
        public ApplicationRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<LogMessage> GetLogMessage(int Id)
        {
            var message = await _context.LogMessages.FirstOrDefaultAsync(m => m.Id == Id);
            return message;
        }

        public async Task<PagedList<LogMessage>> GetLogMessages(PageParams pageParams)
        {
            var messages = _context.LogMessages.OrderByDescending(m => m.CreatedAt);
            return await PagedList<LogMessage>.CreateAsync(messages, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}