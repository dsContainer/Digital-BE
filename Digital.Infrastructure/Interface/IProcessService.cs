using Digital.Infrastructure.Model.DocumentModel;
using Digital.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Infrastructure.Interface
{
    public interface IProcessService
    {
        Task<ResultModel> GetProcesses();
        Task<ResultModel> GetProcessById(Guid id);
        Task<ResultModel> GetProcessByDocumentType(Guid id);
        Task<ResultModel> GetProcessByCreatedDate(Guid id);
        Task<ResultModel> CreateProcess(DocumentTypeCreateModel model);
        Task<int> DisableProcess(Guid id);
        Task<ResultModel> UpdateProcess(DocumentTypeUpdateModel model, Guid Id);
    }
}
