namespace NarcisKH.Models.S3Handler
{
    public interface IStorageService
    {
        Task<S3ResponseDTO> UploadFileAsync(S3Object s3Object, AwsCredentials awsCredentials);
        Task<S3ResponseDTO> DeleteFileAsync(string fileName, string bucketName, AwsCredentials awsCredentials);
    }
}
