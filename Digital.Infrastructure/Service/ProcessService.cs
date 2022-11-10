using Digital.Infrastructure.Interface;
using Digital.Infrastructure.Model;
using Digital.Infrastructure.Model.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Infrastructure.Service
{
    public class ProcessService : IProcessService
    {
        public Task<ResultModel> CreateProcess(DocumentTypeCreateModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DisableProcess(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel> GetProcessByCreatedDate(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel> GetProcessByDocumentType(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel> GetProcessById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel> GetProcesses()
        {
            throw new NotImplementedException();
        }

        public Task<ResultModel> UpdateProcess(DocumentTypeUpdateModel model, Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
