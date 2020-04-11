namespace Azakaw.Domain.Config
{
    public class SecurityConfig
    {
        public string JwtSecret { get; set; }
        public int? ExpiresInHours { get; set; }
    }
}