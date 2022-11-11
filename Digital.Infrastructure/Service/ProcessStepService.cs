using AutoMapper;
using Digital.Data.Data;
using Digital.Data.Entities;
using Digital.Infrastructure.Interface;
using Digital.Infrastructure.Model;
using Digital.Infrastructure.Model.ProcessModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Infrastructure.Service
{
    public class ProcessStepService : IProcessStepService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        public ProcessStepService(
            IMapper mapper,
            ApplicationDBContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultModel> AssignProcessStep(ProcessStepCreateModel model, Guid ProcesssId)
        {
            var result = new ResultModel();
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var processStep = _mapper.Map<ProcessStep>(model);
                await _context.ProcessSteps.AddAsync(processStep);
                var process = await _context.Processes.FindAsync(ProcesssId);
                if (process != null)
                {
                    process.ProcessStep!.Add(processStep);
                    await _context.SaveChangesAsync();

                    result.IsSuccess = true;
                    result.Code = 200;
                    result.ResponseSuccess = processStep;

                    await transaction.CommitAsync();
                }
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                result.IsSuccess = false;
                result.ResponseFailed = e.InnerException != null ? e.InnerException.Message + "\n" + e.StackTrace : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }

        public async Task<int> DeleteProcessStep(Guid id)
        {
            var processSteps = await _context.ProcessSteps.FindAsync(id);
            if (processSteps == null)
                throw new Exception($"Cannot find a process step with id {id}");
            _context.ProcessSteps.Remove(processSteps);
            return await _context.SaveChangesAsync();
        }

        public async Task<ResultModel> GetProcessStepById(Guid id)
        {
            var result = new ResultModel();
            try
            {
                var processStep = await _context.Processes.FindAsync(id);

                if (processStep == null)
                {
                    result.Code = 400;
                    result.IsSuccess = false;
                    result.ResponseSuccess = $"Any Process Steps Not Found!";
                    return result;
                }

                result.Code = 200;
                result.IsSuccess = true;
                result.ResponseSuccess = processStep;

            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.ResponseFailed = e.InnerException != null ? e.InnerException.Message + "\n" + e.StackTrace : e.Message + "\n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> UpdateProcessStep(ProcessStepUpdateModel model, Guid Id)
        {
            var result = new ResultModel();
            var transaction = _context.Database.BeginTransaction();
            try
            {
                var process = await _context.ProcessSteps.FindAsync(Id);
                if (process == null)
                {
                    result.Code = 200;
                    result.IsSuccess = true;
                    result.ResponseSuccess = new ProcessStepUpdateModel();
                    return result;
                }

                var newProcess = _mapper.Map<ProcessStep>(model);
                _context.ProcessSteps.Update(newProcess);
                await _context.SaveChangesAsync();
                result.IsSuccess = true;
                result.Code = 200;
                result.IsSuccess = true;
                await transaction.CommitAsync();
                result.ResponseSuccess = _mapper.Map<List<ProcessStepUpdateModel>>(newProcess);
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
