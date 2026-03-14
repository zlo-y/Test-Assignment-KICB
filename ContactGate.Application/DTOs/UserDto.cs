namespace ContactGate.Application.DTOs;

// 
// Dto для пользователя. Он содержит свойства Id, Name, Email, DateOfBirth и список телефонных номеров. Также есть конструкторы для удобства создания объектов.
// 
public record UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public List<PhoneNumberDto> PhonesNumber { get; set; } = new();

    public UserDto() { }


    public UserDto(int id, string name, string email, DateTime dateOfBirth, List<PhoneNumberDto> phonesNumber)
    {
        Id = id;
        Name = name;
        Email = email;
        DateOfBirth = dateOfBirth;
        PhonesNumber = phonesNumber;
    }
}

public record CreateUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    public CreateUserDto() { }
    
    public CreateUserDto(string name, string email, DateTime dateOfBirth)
    {
        Name = name;
        Email = email;
        DateOfBirth = dateOfBirth;
    }
}


public record UpdateUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.Now;

    public UpdateUserDto() { }
}