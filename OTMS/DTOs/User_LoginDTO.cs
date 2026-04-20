namespace OTMS.Models;
public class User_LoginDTO
{
    public int UserId { get; set; }                  
    public string PasswordHash { get; set; } = string.Empty;
}