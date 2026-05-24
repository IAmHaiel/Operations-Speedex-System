using Microsoft.EntityFrameworkCore;
using OTMS.Data;
using OTMS.Entities.DTOs.Task;
using OTMS.Entities.DTOs.Task.Responses;
using OTMS.Service.Interfaces;
using System.Security.Claims;

namespace OTMS.Service.Services
{
    public class TaskService(OTMSDbContext context, IHttpContextAccessor httpContextAccessor) : ITaskService
    {
        public async Task<TaskResponseDTO> CreateTaskAsync(CreateTaskDTO request)
        {
            // Get Logged In User
            var accountIdClaim = httpContextAccessor
                .HttpContext?
                .User
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (string.IsNullOrEmpty(accountIdClaim))
            {
                throw new UnauthorizedAccessException("Invalid user session.");
            }

            var creatorId = Guid.Parse(accountIdClaim);

            // Check Assigned Employee
            var assignedAccount = await context.Accounts
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.AccountId == request.AssignedTo);

            if (assignedAccount == null)
            {
                throw new Exception("Assigned employee not found.");
            }

            // Get Creator
            var creatorAccount = await context.Accounts
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.AccountId == creatorId);

            if (creatorAccount == null)
            {
                throw new Exception("Creator account not found.");
            }

            // Create Task
            var task = new OTMS.Entities.Models.Task
            {
                TaskId = Guid.NewGuid(),
                CreatedBy = creatorId,
                AssignedTo = request.AssignedTo,

                TaskTitle = request.TaskTitle,
                TaskDescription = request.TaskDescription,
                Priority = request.Priority,
                DueAt = request.DueAt,

                TaskStatus = "Pending",

                CreatedAt = DateTime.UtcNow
            };

            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            return new TaskResponseDTO
            {
                TaskId = task.TaskId,
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDescription,
                Priority = task.Priority,
                DueAt = task.DueAt,
                TaskStatus = task.TaskStatus,

                AssignedEmployee = assignedAccount.Employee.EmployeeName,
                CreatedByEmployee = creatorAccount.Employee.EmployeeName,

                CreatedAt = task.CreatedAt
            };
        }

        public async Task<TaskResponseDTO> UpdateTaskAsync(Guid taskId, UpdateTaskDTO request)
        {
            var task = await context.Tasks
                .Include(t => t.Assignee)
                    .ThenInclude(a => a.Employee)
                .Include(t => t.Creator)
                    .ThenInclude(a => a.Employee)
                .FirstOrDefaultAsync(t => t.TaskId == taskId);

            if (task == null)
            {
                throw new Exception("Task not found.");
            }

            // Update Fields
            task.TaskTitle = request.TaskTitle;
            task.TaskDescription = request.TaskDescription;
            task.Priority = request.Priority;
            task.DueAt = request.DueAt;
            task.AssignedTo = request.AssignedTo;
            task.TaskRemarks = request.TaskRemarks;

            task.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();

            // Reload assignee if changed
            var assignedAccount = await context.Accounts
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.AccountId == request.AssignedTo);

            return new TaskResponseDTO
            {
                TaskId = task.TaskId,
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDescription,
                Priority = task.Priority,
                DueAt = task.DueAt,
                TaskStatus = task.TaskStatus,

                AssignedEmployee = assignedAccount?.Employee.EmployeeName ?? "",

                CreatedByEmployee = task.Creator.Employee.EmployeeName,

                CreatedAt = task.CreatedAt
            };
        }

        public async Task<TaskResponseDTO> ReopenTaskAsync(Guid taskId)
        {
            var task = await context.Tasks
                .Include(t => t.Assignee)
                    .ThenInclude(a => a.Employee)
                .Include(t => t.Creator)
                    .ThenInclude(a => a.Employee)
                .FirstOrDefaultAsync(t => t.TaskId == taskId);

            if (task == null)
            {
                throw new Exception("Task not found.");
            }

            if (task.TaskStatus != "Completed")
            {
                throw new Exception("Only completed tasks can be reopened.");
            }

            // Reopen Task
            task.TaskStatus = "In Progress";

            task.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();

            return new TaskResponseDTO
            {
                TaskId = task.TaskId,
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDescription,
                Priority = task.Priority,
                DueAt = task.DueAt,
                TaskStatus = task.TaskStatus,

                AssignedEmployee = task.Assignee.Employee.EmployeeName,

                CreatedByEmployee = task.Creator.Employee.EmployeeName,

                CreatedAt = task.CreatedAt
            };
        }
    }
}
