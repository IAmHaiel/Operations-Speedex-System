using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OTMS.Entities.DTOs.Task;
using OTMS.Entities.DTOs.Task.Responses;
using OTMS.Service.Interfaces;

namespace OTMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class taskController(ITaskService taskService) : ControllerBase
    {

        /// <summary>
        /// Creates a new task and assigns it to an employee. Only authenticated users can create tasks.
        /// </summary>
        [Authorize(Roles = "OperationsAdmin")]
        [HttpPost("create-task")]
        public async Task<ActionResult<TaskResponseDTO>> CreateTask(
            CreateTaskDTO request)
        {
            try
            {
                var result = await taskService.CreateTaskAsync(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
