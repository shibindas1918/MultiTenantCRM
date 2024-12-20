namespace MultiTenantCRM.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        public string? PasswordHash { get; set; }

        public Tenant Tenant { get; set; }
    }
}
