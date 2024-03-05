using NarcisKH.Models;
using NarcisKH.Models.S3Handler;

namespace NarcisKH.Class
{
    public class ImageUploadHelper : IStorageService
    {
        public Task<S3ResponseDTO> DeleteFileAsync(string fileUrl)
        {
            throw new NotImplementedException();
        }

        public Task<S3ResponseDTO> UploadFileAsync(S3Object s3Object, AwsCredentials awsCredentials)
        {
            var credentials = new AwsCredentials
            {
                AwsKey = awsCredentials.AwsKey,
                AwsSecretKey = awsCredentials.AwsSecretKey
            };
            return Task.FromResult(new S3ResponseDTO());
        }
    }
}
