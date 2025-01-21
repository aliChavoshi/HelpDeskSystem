using HelpDeskSystem.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskSystem.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{
    //Entities
    public DbSet<Ticket> Ticket => Set<Ticket>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //one to many
        builder.Entity<Ticket>()
            .HasOne(x => x.CreatedBy)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.CreatedById);

        builder.Entity<Ticket>().Property(x => x.Description).HasMaxLength(900);
        builder.Entity<Ticket>().HasQueryFilter(x => !x.IsDeleted);
        //Comments
        builder.Entity<Comment>()
            .HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById);

        builder.Entity<Comment>()
            .HasOne(x => x.Ticket)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.TicketId);
        builder.Entity<Comment>().Property(x => x.Description).HasMaxLength(500);
    }
}