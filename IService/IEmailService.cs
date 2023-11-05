using FileExplorer.DTOs;

namespace FileExplorer.IService
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailDTO email);
        Task<bool> SendRegistrationCodeByEmail(string UserId);
    }
}
