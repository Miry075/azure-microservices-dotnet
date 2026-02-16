using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wpm.Managemnt.Api.DataAccess;


namespace Wpm.Managemnt.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BreedsController(ManagementDbContext managementDbContext, ILogger<BreedsController> logger) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Create(CreateBreedDto breedDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(breedDto);
        try
        {
            var breed = breedDto.ToBreed();
            await managementDbContext.Breeds.AddAsync(breed, cancellationToken);
            await managementDbContext.SaveChangesAsync(cancellationToken);
            return CreatedAtRoute(nameof(GetBreedById), new { id = breed.Id }, breedDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var results = await managementDbContext
        .Breeds
        .AsNoTracking()
        .ToListAsync(cancellationToken);
        return Ok(results);
    }

    [HttpGet("{id}", Name = nameof(GetBreedById))]
    public async Task<IActionResult> GetBreedById(int id, CancellationToken cancellationToken)
    {
        var result = await managementDbContext
        .Breeds
        .AsNoTracking()
        .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        return result != null ? Ok(result) : NotFound();
    }


}