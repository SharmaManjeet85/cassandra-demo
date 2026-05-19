using Microsoft.AspNetCore.Mvc;
using Photo.Application.DTOs;
using Photo.Application.Interfaces;

namespace Photo.Api.Controllers;

[ApiController]
[Route("api/photos")]
public sealed class PhotosController : ControllerBase
{
    private readonly IPhotoService _service;

    public PhotosController(IPhotoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePhotoRequest request,
        CancellationToken cancellationToken)
    {
        var photoId = await _service.CreateAsync(
            request,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetByPhotoId),
            new { photoId },
            new { photoId });
    }

    [HttpGet("{photoId:guid}")]
    public async Task<IActionResult> GetByPhotoId(
        Guid photoId,
        CancellationToken cancellationToken)
    {
        var photo = await _service.GetByPhotoIdAsync(
            photoId,
            cancellationToken);

        if (photo is null)
        {
            return NotFound();
        }

        return Ok(photo);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(
        string userId,
        CancellationToken cancellationToken)
    {
        var photos = await _service.GetByUserIdAsync(
            userId,
            cancellationToken);

        return Ok(photos);
    }
}