using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Setup DB

        //Device model builder
        //Auto generate id
        modelBuilder.Entity<Device>()
            .Property(d => d.Id)
            .ValueGeneratedOnAdd();

        //Make foreign key 
        modelBuilder.Entity<Device>()
            .HasOne(d => d.User)
            .WithMany(u => u.Devices)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        //User model builder
        //Auto generate id
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        //Make Email unique 
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        //Make Serial numbers unique
        modelBuilder.Entity<Device>()
            .HasIndex(d => d.SerialNumber)
            .IsUnique();

        #endregion
    }

    public DbSet<Device> Devices { get; set; }
    public DbSet<User> Users { get; set; }
}