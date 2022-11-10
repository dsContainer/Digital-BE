using Digital.Infrastructure.Interface;
using Digital.Infrastructure.Model.ProcessModel;
using Microsoft.AspNetCore.Mvc;


namespace Digital_BE.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessService _service;

        public ProcessController(IProcessService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a new process
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProcess(ProcessCreateModel model)
        {
            var result = await _service.CreateProcess(model);

            if (result.IsSuccess && result.Code == 200) return Ok(result.ResponseSuccess);
            return BadRequest(result);
        }

        /// <summary>
        /// get a process by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProcessById(Guid Id)
        {
            if (Id != null)
            {
                var result = await _service.GetProcessById(Id);
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// get all processes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProcesses([FromQuery] ProcessSearchModel searchModel)
        {
            var result = await _service.GetProcesses(searchModel);
            if (result.Code == 200)
                return Ok(result);
            else if (result.Code == 404)
                return NotFound(result);
            return BadRequest(result);
        }

        /// <summary>
        /// delete process
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _service.DeleteProcess(Id);
            if (result > 0)
            {
                return Ok(result);
            }
            return NotFound();
        }


        /// <summary>
        /// update process
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(ProcessUpdateModel model, Guid Id)
        {
            var result = await _service.UpdateProcess(model, Id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
