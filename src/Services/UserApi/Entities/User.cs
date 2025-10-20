namespace UserApi.Entities;

public class User
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public DateTime BirthDate { get; set; }

    public int Gender {  get; set; }

    public string? PhoneNumber { get; set; }

    public string? Department { get; set; }
}
