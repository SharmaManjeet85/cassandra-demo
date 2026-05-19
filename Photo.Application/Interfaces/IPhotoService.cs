using Photo.Application.DTOs;

namespace Photo.Application.Interfaces;

public interface IPhotoService
{
    Task<Guid> CreateAsync(
        CreatePhotoRequest request,
        CancellationToken cancellationToken);

    Task<PhotoResponse?> GetByPhotoIdAsync(
        Guid photoId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<PhotoResponse>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken);
}