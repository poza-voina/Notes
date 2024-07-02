using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Notes.Core.Entities;

namespace Notes.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public virtual DbSet<Note> Notes => Set<Note>();
    public virtual DbSet<Reminder> Reminders => Set<Reminder>();
    public virtual DbSet<Tag> Tags => Set<Tag>();
    
    public ApplicationDbContext() {}
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>().HasKey(n => n.Id);
        modelBuilder.Entity<Reminder>().HasKey(r => r.Id);
        modelBuilder.Entity<Tag>().HasKey(t => t.Id);
        modelBuilder.Entity<Tag>().HasIndex(t => t.Title).IsUnique();

        modelBuilder.Entity<Note>().HasMany(e => e.Tags).WithMany(e => e.Notes).UsingEntity(j => j.ToTable("NotesTags"));
        modelBuilder.Entity<Reminder>().HasMany(e => e.Tags).WithMany(e => e.Reminders)
            .UsingEntity(j => j.ToTable("RemindersTags"));
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies();
    }
}