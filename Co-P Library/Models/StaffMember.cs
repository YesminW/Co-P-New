using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Co_P_Library.Models;


public partial class StaffMember
{
    public string UserId { get; set; } = null!;

    public int KindergartenNumber { get; set; }

    public virtual Kindergarten KindergartenNumberNavigation { get; set; } = null!;

    public virtual ICollection<SufferingFrom> SufferingFroms { get; set; } = new List<SufferingFrom>();

    public virtual User User { get; set; } = null!;
}
