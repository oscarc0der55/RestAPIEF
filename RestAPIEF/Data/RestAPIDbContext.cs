using Microsoft.EntityFrameworkCore;
using RestAPIEF.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RestAPIEF.Data
{
    public class RestAPIDbContext : DbContext
    {
        public RestAPIDbContext(DbContextOptions<RestAPIDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, FirstName = "Johan", LastName = "Dag", PhoneNumber = "123-456-7890" },
                new Person { Id = 2, FirstName = "Anna", LastName = "Berg", PhoneNumber = "987-654-3210" },
                new Person { Id = 3, FirstName = "Oscar", LastName = "Chas", PhoneNumber = "000-000-0123" }
                );

            modelBuilder.Entity<Interest>().HasData(
                new Interest { Id = 1, Name = "Hiking", Description = "Not the biggest fan of heights", Websitelink = "www.hiking.net", PersonId = 1 },
                new Interest { Id = 2, Name = "Cooking", Description = "Uses a variety of spices", Websitelink = "www.cooking.com", PersonId = 1 },
                new Interest { Id = 3, Name = "Reading", Description = "Favorite genre are mystery novels", Websitelink = "www.reading.com", PersonId = 2 },
                new Interest { Id = 4, Name = "Traveling", Description = "Travel to the alps this summer", Websitelink = "www.traveling.com", PersonId = 2 },
                new Interest { Id = 5, Name = "Gaming", Description = "On the computer", Websitelink = "www.gaming.com", PersonId = 3 },
                new Interest { Id = 6, Name = "Bowling", Description = "On a friday evening with friends", Websitelink = "www.bowling.com", PersonId = 3 }
                );

            modelBuilder.Entity<Link>().HasData(
                new Link { Id = 1, Url ="www.morecooking.com", InterestId = 2 },
                new Link { Id = 2, Url = "www.newcooking.com", InterestId = 2 },
                new Link { Id = 3, Url = "www.morereading.com", InterestId = 3 },
                new Link { Id = 4, Url = "www.morebowling.com", InterestId = 6 },
                new Link { Id = 5, Url = "www.newbowling.com", InterestId = 6 },
                new Link { Id = 6, Url = "www.morehikinh.com", InterestId = 1 }
                );
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }
    }
}
