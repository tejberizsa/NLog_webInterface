using System.Collections.Generic;
using System.Threading.Tasks;
using LogiAppMonitor.API.Helpers;
using LogiAppMonitor.API.Models;

namespace LogiAppMonitor.API.Data
{
    public interface IApplicationRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
         Task<PagedList<LogMessage>> GetLogMessages(PageParams pageParams);
         Task<LogMessage> GetLogMessage(int Id);
        //Task GetLogMessages(object userParams1, object userParams2);
    }
}