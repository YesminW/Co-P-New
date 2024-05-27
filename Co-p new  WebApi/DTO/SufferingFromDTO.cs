namespace Co_p_new__WebApi.DTO
{
    public class SufferingFromDTO
    {
        public int HealthProblemsNumber { get; set; }
        public string UserId { get; set; } = null!;


        public int Severity { get; set; }

        public string? Care { get; set; }
    }
}
