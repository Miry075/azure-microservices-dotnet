using Wpm.Managemnt.Api.Entities;

public record CreateBreedDto(string Name)
{

    public Breed ToBreed() => new Breed(0, Name);
}