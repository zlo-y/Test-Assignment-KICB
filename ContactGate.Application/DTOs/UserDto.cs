namespace ContactGate.Application.DTOs;
// 
// DTO создал для того, чтобы не возвращать сущности напрямую, а использовать их в виде объектов передачи данных. Это позволяет контролировать, какие данные будут возвращаться клиенту!
// 
    public record UserDto(int Id , string Name, string Email, DateTime DateOfBirth, List<PhoneNumberDto> PhonesNumber);
    public record CreateUserDto(string Name, string Email, DateTime DateOfBirth);
    public record UpdateUserDto(string Name, string Email, DateTime DateOfBirth);