﻿using Co_P_Library.Models;

namespace Co_p_new__WebApi.DTO
{
    public class ChildHealthProblemDTO
    {
        public string ChildName { get; set; } = string.Empty;
        public string HealthProblemName { get; set; } = string.Empty;
        public int Severity { get; set; }
        public string Care { get; set; } = null!;
        public string KindergartenName { get; set; } = string.Empty;
        public int CurrentAcademicYear { get; set; }
        public virtual AcademicYear CurrentAcademicYearNavigation { get; set; } = null!;

        public virtual Kindergarten KindergartenNumberNavigation { get; set; } = null!;
        public virtual DiagnosedWith DiagnosedWith { get; set; } = null!;


    }
}
