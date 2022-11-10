using AutoMapper;
using Digital.Data.Data;
using Digital.Data.Entities;
using Digital.Infrastructure.Interface;
using Digital.Infrastructure.Model;
using Digital.Infrastructure.Model.DocumentModel;
using Digital.Infrastructure.Model.ProcessModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Infrastructure.Service
{
    public class ProcessService : IProcessService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public ProcessService(
            IMapper mapper,
            ApplicationDBContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResultModel> CreateProcess(ProcessCreateModel model)
        {
            var result = new ResultModel();
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var process = _mapper.Map<Process>(model);
                await _context.Processes.AddAsync(process);
                await _context.SaveChangesAsync();

                result.IsSuccess = true;
                result.Code = 200;
                result.ResponseSuccess = _mapper.Map<ProcessModel>(process);

                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                result.IsSuccess = false;
                result.ResponseFailed = e.InnerException != null ? e.InnerException.Message + "\n" + e.StackTrace : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }

        public async Task<int> DeleteProcess(Guid id)
        {
            var res = await _context.Processes.FindAsync(id);
            if (res == null)
                throw new Exception($"Cannot find a process with id {id}");
            var processSteps = await _context.ProcessSteps.Where(x => x.ProcessId == id).ToListAsync();
            _context.ProcessSteps.RemoveRange(processSteps);
            _context.Processes.Remove(res);
            return await _context.SaveChangesAsync();
        }

        public async Task<ResultModel> GetProcessById(Guid id)
        {
            var result = new ResultModel();
            try
            {
                var processes = _context.Processes.Where(x => x.Id == id);

                if (processes == null)
                {
                    result.Code = 400;
                    result.IsSuccess = false;
                    result.ResponseSuccess = $"Any Processes Not Found!";
                    return result;
                }
                
                result.Code = 200;
                result.IsSuccess = true;
                result.ResponseSuccess = await _mapper.ProjectTo<Process>(processes).FirstOrDefaultAsync();

            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.ResponseFailed = e.InnerException != null ? e.InnerException.Message + "\n" + e.StackTrace : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> GetProcesses(ProcessSearchModel searchModel)
        {
            var result = new ResultModel();
            try
            {
                var processes = _context.Processes.Where(x => !x.IsDeleted);

                if (!processes.Any())
                {
                    result.Code = 404;
                    result.IsSuccess = false;
                    result.ResponseSuccess = $"Any Processes Not Found!";
                    return result;
                }

                if(searchModel.CreatedDate != null)
                {
                    processes = processes.Where(x => x.DateCreated == searchModel.CreatedDate);
                }

                result.Code = 200;
                result.IsSuccess = true;
                result.ResponseSuccess = await _mapper.ProjectTo<ProcessViewModel>(processes).ToListAsync();

            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.ResponseFailed = e.InnerException != null ? e.InnerException.Message + "\n" + e.StackTrace : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> UpdateProcess(ProcessUpdateModel model, Guid Id)
        {
            var result = new ResultModel();
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var process = await _context.Documents.FindAsync(Id);
                if (process == null)
                {
                    result.Code = 200;
                    result.IsSuccess = true;
                    result.ResponseSuccess = new ProcessUpdateModel();
                    return result;
                }

                var newProcess = _mapper.Map<Process>(model);
                _context.Processes.Update(newProcess);
                await _context.SaveChangesAsync();
                result.IsSuccess = true;
                result.Code = 200;
                result.IsSuccess = true;
                await transaction.CommitAsync();
                result.ResponseSuccess = _mapper.Map<List<ProcessUpdateModel>>(newProcess);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                result.IsSuccess = false;
                result.ResponseFailed = e.InnerException != null ? e.InnerException.Message + "\n" + e.StackTrace : e.Message + "\n" + e.StackTrace;
            }
            return result;
        }
    }
}
