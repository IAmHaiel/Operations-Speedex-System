namespace OTMS.Common
{
    public class OnLeaveException(string overrideToken, Guid leaveId, string employeeName) : Exception("Your account is currently on leave.")
    {
        public string OverrideToken { get; } = overrideToken;
        public Guid LeaveId { get; } = leaveId;
        public string EmployeeName { get; } = employeeName;
    }
}
