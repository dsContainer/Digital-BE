using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Data.Entities
{
    public  class Process : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Template { get; set; }
        public string? Status { get; set; }
        public string? CompanyLevel { get; set; }
        [ForeignKey("ProcessStepId")]
        public string? ProcessStepId { get; set; }
        public virtual ICollection<ProcessStep>? ProcessStep { get; set; } = new List<ProcessStep>();
        [ForeignKey("BatchId")]
        public string? BatchId { get; set; }
        public virtual Batch? Batch { get; set; }

    }
}
