using PhotoEntity = Photo.Domain.Entities.Photo;

namespace Photo.Application.Interfaces
{
    public interface IPhotoRepository
    {
        Task CreateAsync(PhotoEntity photo, CancellationToken cancellationToken);

        Task<PhotoEntity?> GetByIdAsync(Guid photoId, CancellationToken cancellationToken);

        Task<IReadOnlyList<PhotoEntity>> GetByUserIdAsync(string userId, CancellationToken cancellationToken);    
    }
}