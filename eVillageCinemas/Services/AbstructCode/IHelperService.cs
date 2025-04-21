using System.Drawing.Imaging;
using ZXing;

namespace eVillageCinemas.Services.AbstructCode
{
    public interface IHelperService
    {
        public Task<string> UploadFileAsync(IFormFile file);

        public Task<string> UploadFileAsync(MemoryStream memoryStream);

        public Task SendEmailAsync(string fromAddress, string toAddress, string subject, string htmlBody);
    }
}
