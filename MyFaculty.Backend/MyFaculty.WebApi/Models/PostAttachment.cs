namespace MyFaculty.WebApi.Models
{
    public class PostAttachment
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
        public long Length { get; set; }
        public string Extension { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is PostAttachment attachment)
                return Path == attachment.Path;
            return false;
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }
    }
}
