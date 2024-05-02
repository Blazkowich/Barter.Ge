using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barter.Ge.DAL.Context.Entities
{
    public class ExchangeEntity
    {
        public Guid Id { get; set; }

        [ForeignKey("InitiatorId")]
        public Guid InitiatorId { get; set; }

        [ForeignKey("ReceiverId")]
        public Guid ReceiverId { get; set; }

        [ForeignKey("ItemOfferedId")]
        public Guid ItemOfferedId { get; set; }

        [ForeignKey("ItemRequestedId")]
        public Guid ItemRequestedId { get; set; }

        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime ExchangedAt { get; set; }
    }
}
