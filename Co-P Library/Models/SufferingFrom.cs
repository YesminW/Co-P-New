using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Co_P_Library.Models;


public partial class SufferingFrom
{
    public string UserId { get; set; } = null!;

    public int HealthProblemsNumber { get; set; }

    public string Care { get; set; } = null!;

    public int Severity { get; set; }

    public virtual HealthProblem HealthProblemsNumberNavigation { get; set; } = null!;

    public virtual StaffMember User { get; set; } = null!;
}
