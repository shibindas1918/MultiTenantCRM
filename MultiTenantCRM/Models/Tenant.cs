namespace MultiTenantCRM.Models
{
    public class Tenant
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Contact> Customers { get; set; } = new List<Contact>();

    }
}

