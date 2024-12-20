namespace MultiTenantCRM.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
        public Tenant Tenant { get; set; }
    }
}
