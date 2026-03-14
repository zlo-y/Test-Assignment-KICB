namespace ContactGate.Application.DTOs;

// 
// Dto для телефонного номера. Он содержит свойства Id, Number и UserId.
// 
public record PhoneNumberDto(int Id, string Number, int UserId);
public record CreatePhoneNumberDto(string Number, int UserId);
public record UpdatePhoneNumberDto(string Number);