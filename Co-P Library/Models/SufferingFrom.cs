using System;
using System.Collections.Generic;


namespace Co_P_Library.Models
{
    public partial class SufferingFrom
    {
        public int HealthProblemsNumber { get; set; }
        public string UserId { get; set; } = null!;


        public int Severity { get; set; }

        public string? Care { get; set; }
        public User users { get; set; } = null!;
        public HealthProblem HealthProblems { get; set; } = null!;


    }
}
