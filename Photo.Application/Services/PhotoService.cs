using Photo.Application.DTOs;
using Photo.Application.Interfaces;
using PhotoEntity = Photo.Domain.Entities.Photo;
using Microsoft.Extensions.Logging;
namespace Photo.Application.Services;

public sealed class PhotoService : IPhotoService
{
    private readonly IPhotoRepository _repository;
    private readonly ILogger<PhotoService> _logger;

    public PhotoService(IPhotoRepository repository, ILogger<PhotoService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Guid> CreateAsync(
        CreatePhotoRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating photo for user {UserId}", request.UserId);

        var photo = new PhotoEntity
        {
            PhotoId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Url = request.Url,
            UploadedAt = DateTime.UtcNow
        };


        await _repository.CreateAsync(
            photo,
            cancellationToken);

        _logger.LogInformation(
    "Photo created successfully with id {PhotoId}",
    photo.PhotoId);
        return photo.PhotoId;
    }

    public async Task<PhotoResponse?> GetByPhotoIdAsync(
        Guid photoId,
        CancellationToken cancellationToken)
    {
        var photo = await _repository.GetByIdAsync(
            photoId,
            cancellationToken);

        if (photo is null)
        {
            return null;
        }

        return Map(photo);
    }

    public async Task<IReadOnlyList<PhotoResponse>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken)
    {
        var photos = await _repository.GetByUserIdAsync(
            userId,
            cancellationToken);

        return photos
            .Select(Map)
            .ToList();
    }

    private static PhotoResponse Map(PhotoEntity photo)
    {
        return new PhotoResponse
        {
            PhotoId = photo.PhotoId,
            UserId = photo.UserId,
            Title = photo.Title,
            Url = photo.Url,
            UploadedAt = photo.UploadedAt
        };
    }
}