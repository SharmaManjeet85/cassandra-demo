namespace Photo.Domain.Entities
{
    public sealed class Photo
    {
        public Guid PhotoId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public DateTime UploadedAt { get; set; }
    }
}