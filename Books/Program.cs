using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Books.Data;
using Books.EntityModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Books
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedData(host);
            host.Run();
        }

        private static void SeedData(IHost host)
        {
            using var scope = host.Services.CreateScope();
            
            var service = scope.ServiceProvider;
            //apply migration
            var dbContext = service.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
            var data = SeedDataHelper.GetSampleData();
            var authors = data.Select(x => x.Author).Distinct().ToList();
            foreach (var author in authors)
            {
                var split = author.Split(" ");
                var authorEnity = new AuthorEntity
                {
                    FirstName = split[0],
                    LastName = split.Length > 1 ? split[1] : "",
                };
                dbContext.Set<AuthorEntity>().Add(authorEnity);
            }

            dbContext.SaveChanges();

            var authorEntity = dbContext.AuthorEntities.Select(x => new
            {
                x.Id, 
                FullName = $"{x.FirstName} {x.LastName}"
            }).ToList();
            
            for (var index = 0; index < data.Count; index++)
            {
                var book = data[index];
                var author = authorEntity.FirstOrDefault(x => x.FullName == book.Author);
                if (author == null)
                {
                    continue;
                }

                var bookEntity = new BookEntity
                {
                    Title = book.Title,
                    PublishedOn = DateTime.UtcNow.AddDays(index * -1),
                    AuthorId = author.Id,
                };
                dbContext.Set<BookEntity>().Add(bookEntity);
            }
            dbContext.SaveChanges();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}