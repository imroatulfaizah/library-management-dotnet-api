using System;
using System.Collections.Generic;

namespace LibraryManagementAPI.Entities;

public partial class Author
{
    public int AuthorId { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
