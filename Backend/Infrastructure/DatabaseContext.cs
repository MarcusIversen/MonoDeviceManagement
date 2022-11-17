using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
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
            .OnDelete(DeleteBehavior.Cascade);
        
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

        #region Seed data
        
        //User
        User user1 = new User { Id = 1, Email = "Andy@email.com", FirstName = "Andy", LastName = "Lam", Role = "Admin", WorkNumber = "12345678", Hash = "test", Salt = "test", Devices = new List<Device>()};
        User user2 = new User { Id = 2, Email = "Kris@email.com", FirstName = "Kristian", LastName = "Johnson", Role = "Teacher", WorkNumber = "87654321", Hash = "test", Salt = "test", Devices = new List<Device>()};
        modelBuilder.Entity<User>().HasData(user1, user2);

        //Device
        Device device1 = new Device { Id = 1, Amount = 1, DeviceName = "Seed device1", SerialNumber = "1234553", UserId = user1.Id};
        Device device2 = new Device { Id = 2, Amount = 1, DeviceName = "Seed device2", SerialNumber = "1123", UserId = user2.Id};
        modelBuilder.Entity<Device>().HasData(device1, device2);
        
        #endregion

    }
    
    public DbSet<Device> Devices { get; set; }
    public DbSet<User> Users { get; set; }
}