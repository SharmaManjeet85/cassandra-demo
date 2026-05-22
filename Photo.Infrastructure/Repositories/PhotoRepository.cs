using Cassandra;
using Photo.Application.Interfaces;
using PhotoEntity = Photo.Domain.Entities.Photo;

namespace Photo.Infrastructure.Repositories;

public sealed class PhotoRepository : IPhotoRepository
{
    private readonly ISession _session;

    private readonly PreparedStatement _insertPhotoById;

    private readonly PreparedStatement _insertPhotoByUser;

    private readonly PreparedStatement _getByPhotoId;

    private readonly PreparedStatement _getByUserId;

    public PhotoRepository(ISession session)
    {
        _session = session;

        _insertPhotoById = _session.Prepare(@"
            INSERT INTO photos_by_id
            (
                photo_id,
                user_id,
                title,
                url,
                uploaded_at
            )
            VALUES (?, ?, ?, ?, ?)");

        _insertPhotoByUser = _session.Prepare(@"
            INSERT INTO photos_by_user
            (
                user_id,
                uploaded_at,
                photo_id,
                title,
                url
            )
            VALUES (?, ?, ?, ?, ?)");

        _getByPhotoId = _session.Prepare(@"
            SELECT *
            FROM photos_by_id
            WHERE photo_id = ?");

        _getByUserId = _session.Prepare(@"
        SELECT *
        FROM photos_by_user
        WHERE user_id = ?");
    }

    public async Task CreateAsync(
        PhotoEntity photo,
        CancellationToken cancellationToken)
    {
        var batch = new BatchStatement();

        batch.Add(
            _insertPhotoById.Bind(
                photo.PhotoId,
                photo.UserId,
                photo.Title,
                photo.Url,
                photo.UploadedAt));

        batch.Add(
            _insertPhotoByUser.Bind(
                photo.UserId,
                photo.UploadedAt,
                photo.PhotoId,
                photo.Title,
                photo.Url));

        await _session.ExecuteAsync(batch)
            .WaitAsync(cancellationToken);
    }

    public async Task<PhotoEntity?> GetByIdAsync(
        Guid photoId,
        CancellationToken cancellationToken)
    {
        var statement =
            _getByPhotoId.Bind(photoId);

        var result = await _session
            .ExecuteAsync(statement)
            .WaitAsync(cancellationToken);

        var row = result.FirstOrDefault();

        if (row is null)
        {
            return null;
        }

        return MapPhoto(row);
    }

    public async Task<IReadOnlyList<PhotoEntity>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken)
    {
        var statement =
            _getByUserId.Bind(userId);

        var result = await _session
            .ExecuteAsync(statement)
            .WaitAsync(cancellationToken);

        return result
            .Select(MapPhoto)
            .ToList();
    }

    private static PhotoEntity MapPhoto(Row row)
    {
        return new PhotoEntity
        {
            PhotoId = row.GetValue<Guid>("photo_id"),
            UserId = row.GetValue<string>("user_id"),
            Title = row.GetValue<string>("title"),
            Url = row.GetValue<string>("url"),
            UploadedAt = row.GetValue<DateTime>(
                "uploaded_at")
        };
    }
}