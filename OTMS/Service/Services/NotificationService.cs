using Microsoft.EntityFrameworkCore;
using OTMS.Common.Constraints;
using OTMS.Data;
using OTMS.Entities.Models;
using OTMS.Service.Interfaces;

namespace OTMS.Service.Services
{
    public class NotificationService(OTMSDbContext context) : INotificationService
    {
        public async System.Threading.Tasks.Task CreateDeadlineNotificationAsync(Entities.Models.Task task)
        {
            var notification = new Notification
            {
                NotificationId = Guid.NewGuid(),
                EmployeeId = task.AssignedTo,
                TaskId = task.TaskId,
                NotificationType =
                    NotificationTypes.TaskDeadlineApproaching,
                Message =
                    $"Task '{task.TaskTitle}' is nearing its deadline.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task CreateTaskAssignedNotificationAsync(Entities.Models.Task task)
        {
            var notification = new Notification
            {
                NotificationId = Guid.NewGuid(),
                EmployeeId = task.AssignedTo,
                TaskId = task.TaskId,
                NotificationType =
                    NotificationTypes.TaskAssigned,
                Message =
                    $"You were assigned a new task: '{task.TaskTitle}'.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task CheckTaskDeadlinesAsync()
        {
            var currentDate = DateTime.UtcNow;

            var upcomingTasks = await context.Tasks
                .Where(t =>
                    t.TaskStatus != "Completed"
                    &&
                    t.DueAt.HasValue
                    &&
                    t.DueAt.Value.Date <=
                        currentDate.AddDays(1).Date
                    &&
                    t.DueAt.Value.Date >= currentDate.Date)
                .ToListAsync();

            foreach (var task in upcomingTasks)
            {
                bool notificationExists =
                    await context.Notifications.AnyAsync(n =>
                        n.TaskId == task.TaskId
                        &&
                        n.NotificationType ==
                            NotificationTypes
                                .TaskDeadlineApproaching);
                if (!notificationExists)
                {
                    await CreateDeadlineNotificationAsync(task);
                }
            }
        }
    }
}
