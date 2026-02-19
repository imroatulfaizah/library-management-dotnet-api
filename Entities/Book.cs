using System;
using System.Collections.Generic;

namespace LibraryManagementAPI.Entities;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string ISBN { get; set; } = null!;

    public int? PublishedYear { get; set; }

    public int CopiesTotal { get; set; }

    public int CopiesAvailable { get; set; }

    public int AuthorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
