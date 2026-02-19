using System;
using System.Collections.Generic;

namespace LibraryManagementAPI.Entities;

public partial class Member
{
    public int MemberId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
