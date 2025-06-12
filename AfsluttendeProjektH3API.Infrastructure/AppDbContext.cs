using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AfsluttendeProjektH3API.Infrastructure
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Book> Books { get; set; }
		public DbSet<Artist> Artists { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Cover> Covers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			

			modelBuilder.Entity<Artist>(a => 
			{
				a.HasKey(a => a.Id);
				a.Property(a => a.FirstName).HasMaxLength(50);
				a.Property(a => a.LastName).HasMaxLength(50);
				a.Property(a => a.Nationality).HasMaxLength(30);
			});

			modelBuilder.Entity<ArtistCover>().HasKey(ac => new { ac.ArtistId, ac.CoverId });

			modelBuilder.Entity<ArtistCover>()
			.HasOne(ba => ba.Artist)
			.WithMany(b => b.ArtistCovers)
			.HasForeignKey(ba => ba.ArtistId);

			modelBuilder.Entity<ArtistCover>()
				.HasOne(ba => ba.Cover)
				.WithMany(a => a.ArtistCover)
				.HasForeignKey(ba => ba.CoverId);

			modelBuilder.Entity<Author>(a =>
			{
				a.HasKey(a => a.Id);
				a.Property(a => a.FirstName).HasMaxLength(50);
				a.Property(a => a.LastName).HasMaxLength(50);
				a.Property(a => a.Nationality).HasMaxLength(30);

			});

			modelBuilder.Entity<Book>(b =>
			{
				b.HasKey(e => e.Id);
				b.Property(e => e.BasePrice).IsRequired();
				b.Property(e => e.PublishedDate).IsRequired();
				b.Property(e => e.Genre).HasMaxLength(30);
				b.Property(e => e.Title)
					  .HasMaxLength(50);
			});

            modelBuilder.Entity<Cover>(c =>
			{
				c.HasKey(e => e.Id);
				c.Property(e => e.Title).HasMaxLength(50);
			});


			base.OnModelCreating(modelBuilder);
		}
	}
}
