namespace ContactGate.Domain.Entities;

// 
// Эта сущность представляет телефонный номер, который связан с пользователем. 
// 
    public class PhoneNumber
    {
        public int Id{get; set;}
        public string Number {get; set;} = string.Empty;

        public int UserId {get; set;}
        public User? User {get; set;}
    }
