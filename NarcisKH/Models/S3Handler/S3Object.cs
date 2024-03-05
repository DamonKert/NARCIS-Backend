namespace NarcisKH.Models.S3Handler
{
    public class S3Object
    {
        public string Name { get; set; }
        public MemoryStream InputStream { get; set; } = null;
        public string BucketName { get; set; } = null!;

    }
}
