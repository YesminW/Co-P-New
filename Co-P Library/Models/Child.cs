﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Co_P_Library.Models;

public partial class Child
{
    public string ChildId { get; set; } = null!;

    public string ChildFirstName { get; set; } = null!;

    public string ChildSurname { get; set; } = null!;

    public DateTime ChildBirthDate { get; set; }

    public string? ChildGender { get; set; }

    public string Parent1 { get; set; } = null!;

    public string Parent2 { get; set; } = null!;

    public virtual ICollection<DailyAttendance> DailyAttendances { get; set; } = new List<DailyAttendance>();

    public virtual ICollection<Duty> DutyChild1Navigations { get; set; } = new List<Duty>();

    public virtual ICollection<Duty> DutyChild2Navigations { get; set; } = new List<Duty>();

    public virtual Parent Parent1Navigation { get; set; } = null!;

    public virtual Parent Parent2Navigation { get; set; } = null!;

    public virtual ICollection<RegisterdTo> RegisterdTos { get; set; } = new List<RegisterdTo>();

    public virtual ICollection<HealthProblem> HealthProblemsNumbers { get; set; } = new List<HealthProblem>();
}
