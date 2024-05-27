using System;
using System.Collections.Generic;

namespace Co_P_Library.Models;

public partial class HealthProblem
{
    public int HealthProblemsNumber { get; set; }

    public string HealthProblemName { get; set; } = null!;

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<StaffMember> Users { get; set; } = new List<StaffMember>();

}
