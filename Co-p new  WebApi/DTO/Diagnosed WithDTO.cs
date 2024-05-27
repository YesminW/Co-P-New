namespace Co_p_new__WebApi.DTO
{
    public class Diagnosed_WithDTO
    {
        public string ChildId { get; set; } = null!;
        public int HealthProblemsNumber { get; set; }

        public int Severity { get; set; }

        public string? Care { get; set; }
    }
}
