using Digital.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Infrastructure.Model
{
    public class ProcessModel
    {
        public string? Name { get; set; }
        public string? Template { get; set; }
        public string? Status { get; set; }
        public string? CompanyLevel { get; set; }
        public string? ProcessStepId { get; set; }
        public virtual ICollection<ProcessStep>? ProcessStep { get; set; } = new List<ProcessStep>();
    }

    public class ProcessCreateModel
    {
        public string? Name { get; set; }
        public string? Template { get; set; }
        public string? Status { get; set; }
        public string? CompanyLevel { get; set; }
        public string? ProcessStepId { get; set; }
        public virtual ICollection<ProcessStep>? ProcessStep { get; set; } = new List<ProcessStep>();
    }
    public class ProcessUpdateModel
    {
        public string? Name { get; set; }
        public string? Template { get; set; }
        public string? Status { get; set; }
        public string? CompanyLevel { get; set; }
        public string? ProcessStepId { get; set; }
        public virtual ICollection<ProcessStep>? ProcessStep { get; set; } = new List<ProcessStep>();
    }
}
