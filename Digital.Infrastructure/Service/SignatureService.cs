using AutoMapper;
using Digital.Data.Data;
using Digital.Data.Entities;
using Digital.Infrastructure.Interface;
using Digital.Infrastructure.Model;
using Digital.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Infrastructure.Service
{
    public class SignatureService : ISignatureService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper; 

        public SignatureService(
            IMapper mapper,
            ApplicationDBContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultModel> CreateSignatureByUserId(Guid userId)
        {
            var result = new ResultModel();
            try
            {
                var userToCreate =  _context.Users.FirstOrDefault(x => x.Id == userId);
                if (userToCreate != null)
                {
                    string message = SignatureUtils.createCertificate(userToCreate.Username);
                    var signature = new Signature
                    {
                        Id =  Guid.NewGuid(),
                        FromDate = DateTime.Now,
                        ToDate = DateTime.Now.AddYears(1),
                        IsDelete = false,
                        UserId = userId
                    };
                   await _context.Signatures.AddAsync(signature);
                    await _context.SaveChangesAsync();
                    result.IsSuccess = true;
                    result.Code = 200;
                    result.ResponseSuccess = message;
                }
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Code = 400;
                result.ResponseFailed = e.InnerException != null ? e.InnerException.Message + "\n" + e.StackTrace : e.Message + "\n" + e.StackTrace;
            }
            return result;  
        }

        public  async Task<ResultModel> GetListSignature()
        {
            var result = new ResultModel();

            try
            {
                var listSignature = await _context.Signatures.ToListAsync();
                if (listSignature != null)
                {
                    result.IsSuccess = true;
                    result.Code = 200;
                    result.ResponseSuccess = listSignature;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Code = 200;
                    result.ResponseSuccess = "Signature not found";
                }
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Code = 400;
                result.ResponseFailed = e.InnerException != null ? e.InnerException.Message + "\n" + e.StackTrace : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> SearchBySignatureId(Guid sigId)
        {

            var result = new ResultModel();
            try
            {
                var signature =  _context.Signatures.FirstOrDefault(x => x.Id == sigId);
                if (signature != null)
                {
                    result.IsSuccess = true;
                    result.Code = 200;
                    result.ResponseSuccess = signature;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Code = 200;
                    result.ResponseSuccess = "Not Found";
                }
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Code = 400;
                result.ResponseFailed = e.InnerException != null ? e.InnerException.Message + "\n" + e.StackTrace : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }


        public async Task<ResultModel> SearchRangeDate(string fromDate, string toDate)
        {

            var result = new ResultModel();
            try
            {
                DateTime fromDateToSearch = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                DateTime toDateToSearch = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                int dateCompare = fromDateToSearch.CompareTo(toDateToSearch); // >=0 => true
                if (dateCompare >= 0)
                {
                    result.IsSuccess = false;
                    result.Code = 400;
                    result.ResponseSuccess = "Date not valid";
                    return result;
                }

                var listSignature = await _context.Signatures.Where(x => x.ToDate >= fromDateToSearch&&x.ToDate <= toDateToSearch)
                                                        .ToListAsync();
                if (listSignature != null)
                {
                    result.IsSuccess = true;
                    result.Code = 200;
                    result.ResponseSuccess = listSignature;
                }
                else
                {
                    result.IsSuccess = true;
                    result.Code = 200;
                    result.ResponseSuccess = "not found";
                }
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.Code = 400;
                result.ResponseFailed = e.InnerException != null ? e.InnerException.Message + "\n" + e.StackTrace : e.Message + "\n" + e.StackTrace;
            }
            return result;
        }
    }
}
