using Amazon.S3.Model;
using Amazon.S3;
using eVillageCinemas.Services.AbstructCode;
using Amazon.SimpleEmail.Model;
using Amazon.SimpleEmail;
using Amazon;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Windows.Compatibility;

namespace eVillageCinemas.Services.ImplementCode
{
    public class HelperService : IHelperService
    {        
        private readonly string _accessKey = "AKIATTDYIGSFWNFZZDED";
        private readonly string _secretKey = "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";
        private readonly string _bucketName = "evillagecinemasbucket2";
        private readonly RegionEndpoint _bucketRegion = RegionEndpoint.EUCentral1;        

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            AmazonS3Client client = new AmazonS3Client(_accessKey, _secretKey, _bucketRegion);

            var keyName = DateTime.UtcNow.Ticks + file.FileName;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                var request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = keyName,
                    InputStream = memoryStream
                };

                await client.PutObjectAsync(request);
            }

            var itemUrl = $"https://{_bucketName}.s3.{_bucketRegion.SystemName}.amazonaws.com/{keyName}";
            return itemUrl;
        }

        public async Task<string> UploadFileAsync(MemoryStream memoryStream)
        {
            AmazonS3Client client = new AmazonS3Client(_accessKey, _secretKey, _bucketRegion);

            var keyName = DateTime.UtcNow.Ticks + "QRCode";            

            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = keyName,
                InputStream = memoryStream
            };

            await client.PutObjectAsync(request);

            var itemUrl = $"https://{_bucketName}.s3.{_bucketRegion.SystemName}.amazonaws.com/{keyName}";
            return itemUrl;
        }

        public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string htmlBody)
        {
            var _sesClient = new AmazonSimpleEmailServiceClient(_accessKey, _secretKey, _bucketRegion);

            var sendRequest = new SendEmailRequest
            {
                Source = fromAddress,
                Destination = new Destination
                {
                    ToAddresses = new List<string> { toAddress }
                },
                Message = new Message
                {
                    Subject = new Content(subject),
                    Body = new Body
                    {
                        Html = new Content(htmlBody)
                    }
                }
            };

            await _sesClient.SendEmailAsync(sendRequest);
        }
    }
}
