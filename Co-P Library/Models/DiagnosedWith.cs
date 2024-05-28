using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Co_P_Library.Models;

public partial class DiagnosedWith
{
    public string ChildId { get; set; } = null!;

    public int HealthProblemsNumber { get; set; }

    public int Severity { get; set; }

    public string Care { get; set; } = null!;

    public virtual Child Child { get; set; } = null!;

    public virtual HealthProblem HealthProblemsNumberNavigation { get; set; } = null!;
}
