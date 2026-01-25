using RestAPIEF.Data;
using Microsoft.EntityFrameworkCore;
using RestAPIEF.Models;

namespace RestAPIEF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<RestAPIDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            //Get all persons
            app.MapGet("/persons", async (RestAPIDbContext context) =>
            {
                var persons = await context.Persons.ToListAsync();

                return Results.Ok(persons);
            });

            //Gets all the specific interests from a specific person
            app.MapGet("/persons/{id}", async (RestAPIDbContext context, int id) =>
            {
                var personint = await context.Persons.Include(p => p.interests).FirstOrDefaultAsync(p => p.Id == id);

                return Results.Ok(personint);
            });


            //Gets all the interestlinks connected to a person
            app.MapGet("/persons/{id}/interests/", async (RestAPIDbContext context, int id) =>
            {
                var person = await context.Persons.FindAsync(id);
                if (person == null)
                {
                    return Results.NotFound();
                }
                var links = await context.Interests.Where(i => i.PersonId == id && i.Websitelink != null)
                      .Select(i => i.Websitelink).ToListAsync();

                if (!links.Any()) return Results.NotFound();
                return Results.Ok(links);
            });

            // Create a new Interest and attach it to an existing Person
            app.MapPost("/persons/{personId}/interests", async (RestAPIDbContext context, int personId, Interest incoming) =>
            {
                var person = await context.Persons.FindAsync(personId);
                if (person == null)
                {
                    return Results.NotFound(new { error = "Person not found", personId });
                }

                // validate incoming as needed
                incoming.PersonId = personId;
                context.Interests.Add(incoming);
                await context.SaveChangesAsync();

                return Results.Created($"/interests/{incoming.Id}", incoming);
            });

            //Adds a new interest
            app.MapPost("/interests/", async (RestAPIDbContext context, Interest interest) =>
            {
                context.Interests.Add(interest);
                await context.SaveChangesAsync();

                return interest;

            });

            // Adds a link for a specifik interest
            app.MapPost("/interests/{id}/links", async (RestAPIDbContext context, int id, Link incoming) =>
            {
                var interest = await context.Interests.FirstOrDefaultAsync(i => i.Id == id);
                if (interest == null)
                {
                    return Results.NotFound();
                }
                var link = new Link { Url = incoming.Url, InterestId = id };
                context.Links.Add(link);
                await context.SaveChangesAsync();


                return Results.Created($"/interests/{id}/links/{link.Id}", link);
            });

            //Get all people, their interests and extra links 
            app.MapGet("/interests", async (RestAPIDbContext context) =>
            {
                var persons = await context.Persons.Include(p => p.interests).ThenInclude(i => i.Links).ToListAsync();
                if (persons == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(persons);
            });

            app.Run();
        }
    }
}
