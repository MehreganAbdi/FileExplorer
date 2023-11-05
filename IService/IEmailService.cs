using FileExplorer.DTOs;

namespace FileExplorer.IService
{
    public interface IEmailService
    {
        Task<bool> SendFileByEmail(EmailDTO email,string path);
        
    }
}
