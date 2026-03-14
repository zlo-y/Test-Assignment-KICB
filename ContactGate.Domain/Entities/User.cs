using System.ComponentModel.DataAnnotations;

namespace ContactGate.Domain.Entities;

// 
// Эта сущность пользователя, который может иметь несколько телефонных номеров.
// 
    public class User
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;

        public DateTime DateOfBirth {get; set;} 

        public List<PhoneNumber> PhonesNumber {get; set;} = new ();


    }
