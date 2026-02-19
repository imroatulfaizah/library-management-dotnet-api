using LibraryManagementAPI.Entities;
using LibraryManagementAPI.Models.Entities;

namespace LibraryManagementAPI.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(LibraryDbContext context)
        {
            if (!context.Authors.Any())
            {
                var authors = new List<Author>();

                for (int i = 1; i <= 50; i++)
                {
                    authors.Add(new Author
                    {
                        FullName = $"Author {i}",
                        BirthDate = DateTime.UtcNow.AddYears(-30).AddDays(i),
                        CreatedAt = DateTime.UtcNow
                    });
                }

                context.Authors.AddRange(authors);
                await context.SaveChangesAsync();
            }

            if (!context.Books.Any())
            {
                var books = new List<Book>();

                for (int i = 1; i <= 500; i++)
                {
                    books.Add(new Book
                    {
                        Title = $"Book Title {i}",
                        ISBN = $"ISBN-{100000 + i}",
                        PublishedYear = 2000 + (i % 25),
                        CopiesTotal = 5 + (i % 10),
                        CopiesAvailable = 5 + (i % 10),
                        AuthorId = (i % 50) + 1,
                        CreatedAt = DateTime.UtcNow
                    });
                }

                context.Books.AddRange(books);
                await context.SaveChangesAsync();
            }

            if (!context.Members.Any())
            {
                var members = new List<Member>();

                for (int i = 1; i <= 200; i++)
                {
                    members.Add(new Member
                    {
                        FullName = $"Member {i}",
                        Email = $"member{i}@mail.com",
                        Phone = $"08{i:0000000000}",
                        CreatedAt = DateTime.UtcNow
                    });
                }

                context.Members.AddRange(members);
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new Users
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = "Admin",
                    CreatedAt = DateTime.UtcNow
                });

                context.Users.Add(new Users
                {
                    Username = "user",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                    Role = "User",
                    CreatedAt = DateTime.UtcNow
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
