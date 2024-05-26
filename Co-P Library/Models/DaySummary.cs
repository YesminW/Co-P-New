using System;
using System.Collections.Generic;

namespace Co_P_Library.Models;

public partial class DaySummary
{
    public DateTime DaySummaryDate { get; set; }

    public DateTime? DaySummaryHour { get; set; }

    public string SummaryDetails { get; set; } = null!;

    public int CurrentAcademicYear { get; set; }

    public int KindergartenNumber { get; set; }

    public virtual AcademicYear CurrentAcademicYearNavigation { get; set; } = null!;

    public virtual Kindergarten KindergartenNumberNavigation { get; set; } = null!;
}
