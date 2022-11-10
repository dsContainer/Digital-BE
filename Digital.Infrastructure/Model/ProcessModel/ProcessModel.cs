using Digital.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Infrastructure.Model.ProcessModel
{
    public class ProcessModel
    {
        public string? Name { get; set; }
        public Guid? TemplateId { get; set; }
        public string? Status { get; set; } = "Active";
        public string? CompanyLevel { get; set; }
        public string? ProcessStepId { get; set; }
        public virtual ICollection<ProcessStepModel>? ProcessStep { get; set; } = new List<ProcessStepModel>();
    }

    public class ProcessCreateModel : ProcessModel
    {
    }
    public class ProcessUpdateModel : ProcessModel
    {
    }

    public class ProcessViewModel : ProcessModel
    {
        public Guid Id { get; set; }
    }

    public class ProcessSearchModel
    {
        public DateTime? CreatedDate { get; set; }
    }
}
