using OTMS.Entities.DTOs.LeaveRequest;
using OTMS.Entities.DTOs.LeaveRequest.Responses;

namespace OTMS.Service.Interfaces
{
    public interface ILeaveRequest
    {
        Task<LeaveRequestResponseDTO> CreateLeaveRequestAsync(CreateLeaveRequestDTO request);
        Task<List<LeaveRequestResponseDTO>> GetAllLeaveRequestsAsync();
    }
}
