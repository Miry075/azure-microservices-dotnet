namespace Wpm.Managemnt.Api.Dtos;

public class ApiVersionDto
{
    public string Version { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime BuildDate { get; set; }
    public string Status { get; set; } = "Running";
}
