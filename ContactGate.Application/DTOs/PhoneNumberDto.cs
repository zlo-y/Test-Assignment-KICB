namespace ContactGate.Application.DTOs;

// 
// DTO для передачи данных о телефонных номерах между слоями приложения.
// 
public record PhoneNumberDto(int Id, string Number, int UserId)
{
    public int Id { get; set; } = Id;
    public string Number { get; set; } = Number;
    public int UserId { get; set; } = UserId;

    public PhoneNumberDto() : this(0, string.Empty, 0) { }
}

public record CreatePhoneNumberDto(string Number, int UserId)
{
    public string Number { get; set; } = Number;
    public int UserId { get; set; } = UserId;

    public CreatePhoneNumberDto() : this(string.Empty, 0) { }
}

public record UpdatePhoneNumberDto(string Number)
{
    public string Number { get; set; } = Number;

    public UpdatePhoneNumberDto() : this(string.Empty) { }
}