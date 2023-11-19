namespace GloboTicket.TicketManagement.Domain.Common
{
    public class AuditableEntity
    {
        public string CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}