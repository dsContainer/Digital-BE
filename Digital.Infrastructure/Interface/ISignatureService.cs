using System;
using Digital.Infrastructure.Model;
using Digital.Infrastructure.Model.SignatureModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Infrastructure.Interface
{
    public interface ISignatureService
    {
        Task<ResultModel> GetListSignature();
        Task<ResultModel> CreateSignatureByUserId(Guid userId);
        Task<ResultModel> SearchBySignatureId(Guid sigId);
        Task<ResultModel> SearchRangeDate(string fromDate, string toDate);
    }
}
