using AfsluttendeProjektH3API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AfsluttendeProjektH3API.Infrastructure
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ArtistCover> ArtistCovers { get; set; }
		public DbSet<Artist> Artists { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Book> Books { get; set; }
		public DbSet<Cover> Covers { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<User> Users { get; set; }
        

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Artist>(a => 
			{
				a.HasKey(a => a.Id);
				a.Property(a => a.FirstName).HasMaxLength(50);
				a.Property(a => a.LastName).HasMaxLength(50);
			});
            modelBuilder.Entity<Artist>(a =>
            {
				a.OwnsOne(a => a.Address);
				a.OwnsOne(a => a.SocialLinks);
				a.OwnsOne(a => a.ContactInfo);
                a.HasOne(a => a.Nationality).WithMany().HasForeignKey(a => a.NationalityId);
                a.HasMany(a => a.Awards).WithOne(a => a.Artist).HasForeignKey(a => a.ArtistId).OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<ArtistCover>().HasKey(ac => new { ac.ArtistId, ac.CoverId });

            modelBuilder.Entity<ArtistCover>()
			.ToTable("ArtistCover");

            modelBuilder.Entity<ArtistCover>()
			.HasOne(ba => ba.Artist)
			.WithMany(b => b.ArtistCovers)
			.HasForeignKey(ba => ba.ArtistId)
			.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ArtistCover>()
				.HasOne(ba => ba.Cover)
				.WithMany(a => a.ArtistCovers)
				.HasForeignKey(ba => ba.CoverId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Author>(a =>
			{
				a.HasKey(a => a.Id);
				a.Property(a => a.FirstName).HasMaxLength(50);
				a.Property(a => a.LastName).HasMaxLength(50);

			});
            modelBuilder.Entity<Author>(a =>
            {
                a.OwnsOne(a => a.Address);
                a.OwnsOne(a => a.ContactInfo);
				a.OwnsOne(a => a.Biography);
                a.HasOne(a => a.Nationality).WithMany().HasForeignKey(a => a.NationalityId);
                a.HasOne(a => a.Publisher).WithMany().HasForeignKey(a => a.PublisherId);
				a.HasOne(a => a.Education).WithMany().HasForeignKey(a => a.EducationId);
            });
            modelBuilder.Entity<Award>(a =>
            {
                a.HasKey(a => a.Id);
                a.HasOne(a => a.Artist)
                 .WithMany(artist => artist.Awards)
                 .HasForeignKey(a => a.ArtistId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Award>(a =>
            {
                a.Property(a => a.Name).HasMaxLength(100).IsRequired();
                a.Property(a => a.Description).HasMaxLength(500);
            });
 
            modelBuilder.Entity<Book>(b =>
			{
				b.Property(e => e.BasePrice).IsRequired();
				b.Property(e => e.PublishedDate).IsRequired();
				b.Property(e => e.Title).HasMaxLength(50);
			});
            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(e => e.Id);
                b.HasOne(b => b.Genre).WithMany().HasForeignKey(b => b.GenreId);
				b.HasMany(b => b.Editions).WithOne(e => e.Book).HasForeignKey(e => e.BookId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Cover>(c =>
			{
				c.HasKey(e => e.Id);
			});

            modelBuilder.Entity<Cover>(c =>
            {
                c.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Edition>(b =>
            {
                b.Property(e => e.Format).HasMaxLength(40);
            });
            modelBuilder.Entity<Education>(b =>
            {
                b.Property(e => e.Degree).HasMaxLength(60);
                b.Property(e => e.Institution).HasMaxLength(100);
            });

            modelBuilder.Entity<Genre>(c =>
            {
                c.HasKey(e => e.Id);
                c.Property(e => e.Name).HasMaxLength(50);
            });
            modelBuilder.Entity<Nationality>(b =>
            {
                b.HasKey(n => n.Id);
            });
            modelBuilder.Entity<Nationality>(b =>
            {
                b.Property(e => e.CountryName).HasMaxLength(60);
                b.Property(e => e.CountryCode).HasMaxLength(15);
            });
            modelBuilder.Entity<Publisher>(b =>
            {
                b.HasKey(n => n.Id);
            });
            modelBuilder.Entity<Publisher>(b =>
            {
                b.Property(e => e.Name).HasMaxLength(60);
                b.Property(e => e.ContactEmail).HasMaxLength(256);
            });

            modelBuilder.Entity<User>(u =>
			{
				u.Property(e => e.Username).HasMaxLength(20);
				u.Property(e => e.PasswordHash).HasMaxLength(2000);
				u.Property(e => e.Role).HasMaxLength(30);
			});
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(e => e.Id);
                u.OwnsOne(u => u.UserProfile);
            });

            modelBuilder.Entity<User>(u =>
            {
                u.OwnsOne(u => u.UserProfile);
            });


            base.OnModelCreating(modelBuilder);
		}
	}
}
