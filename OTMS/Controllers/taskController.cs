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
        /// Creates a new task and assigns it to an employee. Only OperationsAdmin users can create tasks.
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

        /// <summary>
        /// Updates an existing task's details. Only authenticated users can update tasks, and only if they have the "OperationsAdmin" role.
        /// </summary>
        [Authorize(Roles = "OperationsAdmin")]
        [HttpPut("update-task/{taskId}")]
        public async Task<ActionResult<TaskResponseDTO>> UpdateTask(Guid taskId, UpdateTaskDTO request)
        {
            try
            {
                var result = await taskService.UpdateTaskAsync(
                    taskId,
                    request);

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

        /// <summary>
        /// Reopens a completed or closed task, changing its status back to "Pending". Only authenticated users with the "OperationsAdmin" role can reopen tasks.
        /// </summary>
        [Authorize(Roles = "OperationsAdmin")]
        [HttpPatch("{taskId}/reopen")]
        public async Task<ActionResult<TaskResponseDTO>> ReopenTask(Guid taskId)
        {
            try
            {
                var result = await taskService.ReopenTaskAsync(taskId);

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
