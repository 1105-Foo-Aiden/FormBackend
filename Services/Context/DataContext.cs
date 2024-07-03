using FormBackend.Model;
using Microsoft.EntityFrameworkCore;
namespace FormBackend.Services.Context{
    public class DataContext: DbContext
    {
        public DbSet<UserModel> UserInfo { get; set; }
        public DbSet<FormModel> FormInfo { get; set; }
        public DataContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
        }
    }
}