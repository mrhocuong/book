using Books.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.EntityModels;

namespace Books.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<AuthorEntity> AuthorEntities { get; set; }
        public DbSet<BookEntity> BookEntities { get; set; }
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Author
            builder.Entity<AuthorEntity>()
                .HasKey(x => x.Id);
            
            builder.Entity<AuthorEntity>()
                .Property(x => x.Id).ValueGeneratedOnAdd();
            
            //Book
            builder.Entity<BookEntity>()
                .HasKey(x => x.Id);
            builder.Entity<BookEntity>()
                .Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Entity<BookEntity>()
                .HasOne(x => x.AuthorEntity).WithMany(x => x.BookEntities).HasForeignKey(x => x.AuthorId);
            
            base.OnModelCreating(builder);

        }
    }
}