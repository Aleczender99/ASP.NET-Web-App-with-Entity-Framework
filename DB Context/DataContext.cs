using C__App.DB_Models;
using System.Data.Entity;

namespace C__App.DB_Context
{
    public class DataContext : DbContext
    {
        public DataContext(string connectionString) : base(connectionString) {
        }
        public virtual DbSet<User> Users { get; set;}
    }
}
