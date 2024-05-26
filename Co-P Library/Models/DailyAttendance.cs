using System;
using System.Collections.Generic;

namespace Co_P_Library.Models;

public partial class DailyAttendance
{
    public int DailyAttendanceId { get; set; }

    public string ChildId { get; set; } = null!;

    public int MorningPresence { get; set; }
    public DateTime AttendanceDate { get; set; } 

    public int AfternoonPresence { get; set; }

    public virtual Attendance? AfternoonPresenceNavigation { get; set; }

    public virtual Child Child { get; set; } = null!;

    public virtual Attendance? MorningPresenceNavigation { get; set; }
}
