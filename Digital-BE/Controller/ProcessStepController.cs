using Digital.Infrastructure.Interface;
using Digital.Infrastructure.Model.ProcessModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Digital_BE.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessStepController : ControllerBase
    {
        private readonly IProcessStepService _service;

        public ProcessStepController(IProcessStepService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a new process step
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> AssignProcessStep(ProcessStepCreateModel model, [Required][FromQuery] Guid ProcessId)
        //{
        //    var result = await _service.AssignProcessStep(model, ProcessId);

        //    if (result.IsSuccess && result.Code == 200) 
        //        return Ok(result);
        //    return BadRequest(result);
        //}

        /// <summary>
        /// get a process step by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProcessStepById(Guid Id)
        {
            if (Id != null)
            {
                var result = await _service.GetProcessStepById(Id);
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// delete process step
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _service.DeleteProcessStep(Id);
            if (result > 0)
            {
                return Ok(result);
            }
            return NotFound();
        }


        /// <summary>
        /// update process step
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(ProcessStepUpdateModel model)
        {
            var result = await _service.UpdateProcessStep(model);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
