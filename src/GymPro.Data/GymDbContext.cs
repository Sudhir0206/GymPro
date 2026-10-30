using Microsoft.EntityFrameworkCore;

namespace GymPro.Data;

public class GymDbContext(DbContextOptions<GymDbContext> options) : DbContext(options)
{
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Trainer> Trainers => Set<Trainer>();
    public DbSet<Membership> Memberships => Set<Membership>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<Member>().Property(x => x.FullName).HasMaxLength(120).IsRequired();
        modelBuilder.Entity<Member>().Property(x => x.Email).HasMaxLength(160).IsRequired();
        modelBuilder.Entity<Member>().Property(x => x.MobileNumber).HasMaxLength(20).IsRequired();

        modelBuilder.Entity<Trainer>().Property(x => x.TrainerName).HasMaxLength(120).IsRequired();
        modelBuilder.Entity<Membership>().Property(x => x.PlanName).HasMaxLength(80).IsRequired();
        modelBuilder.Entity<Membership>().Property(x => x.Price).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<Payment>().Property(x => x.Amount).HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Member)
            .WithMany()
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
