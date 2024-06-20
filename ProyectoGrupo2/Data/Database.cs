using SQLite;
using ProyectoGrupo2.Models;
using System.Threading.Tasks;

namespace ProyectoGrupo2.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TodoItem>().Wait();
        }

        public Task<List<TodoItem>> GetTodoItemsAsync()
        {
            return _database.Table<TodoItem>().ToListAsync();
        }

        public Task<TodoItem> GetTodoItemAsync(int id)
        {
            return _database.Table<TodoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveTodoItemAsync(TodoItem item)
        {
            if (item.Id != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }

        public Task<int> DeleteTodoItemAsync(TodoItem item)
        {
            return _database.DeleteAsync(item);
        }
    }
}