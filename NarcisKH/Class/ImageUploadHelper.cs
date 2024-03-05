using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using NarcisKH.Models;
using NarcisKH.Models.S3Handler;
using static Amazon.Internal.RegionEndpointProviderV2;

namespace NarcisKH.Class
{
    public class ImageUploadHelper : IStorageService
    {
        public Task<S3ResponseDTO> DeleteFileAsync(string fileUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<S3ResponseDTO> UploadFileAsync(NarcisKH.Models.S3Handler.S3Object s3Object, AwsCredentials awsCredentials)
        {
            //Adding AWS credentials
            var credentials = new AwsCredentials
            {
                AwsKey = awsCredentials.AwsKey,
                AwsSecretKey = awsCredentials.AwsSecretKey
            };
            var config = new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.APSoutheast1
            };
            var response = new S3ResponseDTO();
            try
            {
                //Create upload request
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = s3Object.InputStream,
                    Key = s3Object.Name,
                    BucketName = s3Object.BucketName,
                    CannedACL = S3CannedACL.NoACL
                };
                using (var client = new AmazonS3Client(credentials.AwsKey, credentials.AwsSecretKey, config))
                {
                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                    response.StatusCode = 200;
                    response.Message = "File uploaded successfully";
                }
            }
            catch (AmazonS3Exception e)
            {
                response.StatusCode = (int)e.StatusCode;
                response.Message = e.Message;
            }catch (Exception e)
            {
                response.StatusCode = 500;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
