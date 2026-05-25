using OTMS.Entities.DTOs.EmergencyOverrideRequest;
using OTMS.Entities.DTOs.EmergencyOverrideRequest.Responses;

namespace OTMS.Service.Interfaces
{
    public interface IEmergencyOverrideService
    {
        Task<EmergencyOverrideResponseDTO> RequestOverrideAsync(Guid accountId, CreateEmergencyOverrideRequestDTO request);
        Task<EmergencyOverrideResponseDTO> ApproveOverrideAsync(Guid adminId, ApproveEmergencyOverrideDTO request);
    }
}
