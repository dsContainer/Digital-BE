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
                result.ResponseSuccess = process;

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
                var processes = await _context.Processes.
                    Include(e => e.ProcessStep). 
                    Where(x => x.Id == id).
                    FirstOrDefaultAsync();

                if (processes == null)
                {
                    result.Code = 400;
                    result.IsSuccess = false;
                    result.ResponseSuccess = $"Any Processes Not Found!";
                    return result;
                }
                
                result.Code = 200;
                result.IsSuccess = true;
                result.ResponseSuccess = processes;

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
                var processes = await _context.Processes.Include(e => e.ProcessStep).ToListAsync();

                if (!processes.Any())
                {
                    result.Code = 404;
                    result.IsSuccess = false;
                    result.ResponseSuccess = $"Any Processes Not Found!";
                    return result;
                }

                if(searchModel.CreatedDate != null)
                {
                    processes = await _context.Processes.
                        Include(e => e.ProcessStep).
                        Where(x => x.DateCreated == searchModel.CreatedDate).
                        ToListAsync();
                }

                result.Code = 200;
                result.IsSuccess = true;
                result.ResponseSuccess = processes;

            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.ResponseFailed = e.InnerException != null ? e.InnerException.Message + "\n" + e.StackTrace : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> UpdateProcess(ProcessUpdateModel model)
        {
            var result = new ResultModel();
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var process = await _context.Processes.FindAsync(model.Id);
                if (process == null)
                {
                    result.Code = 200;
                    result.IsSuccess = true;
                    result.ResponseSuccess = new ProcessUpdateModel();
                    return result;
                }

                process.Name = model.Name;
                process.CompanyLevel = model.CompanyLevel;
                process.Status = model.Status;
                var list = model.ProcessStep;
                if (list != null) 
                {
                    foreach (var item in list)
                    {
                        var processStep = await _context.ProcessSteps.FindAsync(item.Id);
                        if (processStep != null) 
                        {
                            processStep.OrderIndex = item.OrderIndex;
                            processStep.UserId = item.UserId;
                            processStep.XPoint = item.XPoint;
                            processStep.YPoint = item.YPoint;
                            processStep.XPointPercent = item.XPointPercent;
                            processStep.YPointPercent = item.YPointPercent;
                            processStep.Width = item.Width;
                            processStep.Height = item.Height;
                            processStep.PageSign = item.PageSign;
                            _context.ProcessSteps.Update(processStep);
                        }
                    }
                
                }
                _context.Processes.Update(process);
                await _context.SaveChangesAsync();
                result.IsSuccess = true;
                result.Code = 200;
                result.IsSuccess = true;
                await transaction.CommitAsync();
                result.ResponseSuccess = process;
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
