using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Co_P_Library.Models;


public partial class HealthProblem
{
    public int HealthProblemsNumber { get; set; }

    public string HealthProblemName { get; set; } = null!;

    public virtual ICollection<DiagnosedWith> DiagnosedWiths { get; set; } = new List<DiagnosedWith>();

    public virtual ICollection<SufferingFrom> SufferingFroms { get; set; } = new List<SufferingFrom>();
}
