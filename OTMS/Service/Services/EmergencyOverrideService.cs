using Microsoft.EntityFrameworkCore;
using OTMS.Data;
using OTMS.Entities.DTOs.EmergencyOverrideRequest;
using OTMS.Entities.DTOs.EmergencyOverrideRequest.Responses;
using OTMS.Entities.Models;
using OTMS.Service.Interfaces;

namespace OTMS.Service.Services
{
    public class EmergencyOverrideService(IHttpContextAccessor httpContextAccessor, OTMSDbContext context) : IEmergencyOverrideService
    {
        public async Task<EmergencyOverrideResponseDTO> ApproveOverrideAsync(Guid adminId, ApproveEmergencyOverrideDTO request)
        {
            var emergencyOverride = await context.EmergencyOverrideRequests
                .FirstOrDefaultAsync(e =>
                    e.EmergencyOverrideId == request.EmergencyOverrideId);

            if (emergencyOverride is null)
            {
                throw new Exception("Emergency Override request not found");
            }

            emergencyOverride.Status = "Approved";
            emergencyOverride.ApprovedById = adminId;
            emergencyOverride.ApprovedAt = DateTime.UtcNow;
            emergencyOverride.OverrideUntil = DateTime.UtcNow.AddDays(1);

            await context.SaveChangesAsync();

            return new EmergencyOverrideResponseDTO
            {
                EmergencyOverrideId = emergencyOverride.EmergencyOverrideId,
                RequestedById = emergencyOverride.RequestedById,
                LeaveId = emergencyOverride.LeaveId,
                Status = emergencyOverride.Status,
                Reason = emergencyOverride.Reason,
                RequestedAt = emergencyOverride.RequestedAt,
                ApprovedAt = emergencyOverride.ApprovedAt,
                OverrideUntil = emergencyOverride.OverrideUntil
            };
        }

        public async Task<EmergencyOverrideResponseDTO> RequestOverrideAsync(Guid accountId, CreateEmergencyOverrideRequestDTO request)
        {
            var leaveRequest = await context.LeaveRequests
                .FirstOrDefaultAsync(lr =>
                    lr.LeaveId == request.LeaveId &&
                    lr.AccountId == accountId &&
                    lr.Approval_Status == "Approved");

            if (leaveRequest is null)
            {
                throw new Exception("You can only request emergency override for approved leave requests.");
            }

            var emergencyOverride = new EmergencyOverrideRequest
            {
                RequestedById = accountId,
                LeaveId = request.LeaveId,
                Reason = request.Reason,
                Status = "Pending"
            };

            context.EmergencyOverrideRequests
                .Add(emergencyOverride);

            await context.SaveChangesAsync();

            return new EmergencyOverrideResponseDTO
            {
                EmergencyOverrideId = emergencyOverride.EmergencyOverrideId,
                RequestedById = emergencyOverride.RequestedById,
                LeaveId = emergencyOverride.LeaveId,
                Status = emergencyOverride.Status,
                Reason = emergencyOverride.Reason,
                RequestedAt = emergencyOverride.RequestedAt,
                ApprovedAt = emergencyOverride.ApprovedAt,
                OverrideUntil = emergencyOverride.OverrideUntil
            };

        }
    }
}
