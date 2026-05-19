namespace Photo.Application.DTOs
{
    public sealed class PhotoResponse
    {
        public Guid PhotoId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; }
    }
}