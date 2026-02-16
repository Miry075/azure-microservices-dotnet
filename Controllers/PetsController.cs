using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wpm.Managemnt.Api.DataAccess;
using Wpm.Managemnt.Api.Dtos;

namespace Wpm.Managemnt.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PetsController(ManagementDbContext managementDbContext, ILogger<PetsController> logger) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Create(CreatePetDto petDto,
           CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(petDto);
        try
        {
            var breed = await managementDbContext.Breeds.FirstOrDefaultAsync(b => b.Id == petDto.BreedId, cancellationToken);
            if (breed == null) return NotFound($"Breed with Id: {petDto.BreedId}");
            var pet = petDto.ToPet();
            await managementDbContext.Pets.AddAsync(pet, cancellationToken);
            await managementDbContext.SaveChangesAsync(cancellationToken);
            return CreatedAtRoute(nameof(GetPetById), new { id = pet.Id }, petDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        var results = await managementDbContext
        .Pets
        .Include(p => p.Breed)
        .AsNoTracking()
        .ToListAsync(cancellationToken);
        return Ok(results);
    }

    [HttpGet("{id}", Name = nameof(GetPetById))]
    public async Task<IActionResult> GetPetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await managementDbContext
        .Pets
        .Include(p => p.Breed)
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        return result != null ? Ok(result) : NotFound();
    }
}
