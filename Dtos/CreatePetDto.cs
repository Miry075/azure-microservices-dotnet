
using System.ComponentModel.DataAnnotations;
using Wpm.Managemnt.Api.Entities;

namespace Wpm.Managemnt.Api.Dtos;

public class CreatePetDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 1)]
    public string? Name { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "BreedId must be valid")]
    public int BreedId { get; set; }

    public int Age { get; set; }
    public Pet ToPet() => new Pet() { Name = Name, Age = Age, BreedId = BreedId };
}