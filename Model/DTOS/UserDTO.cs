namespace FormBackend.Model.DTOS{
    public class UserDTO{
        public int ID {get; set;}
        public string Email {get; set;}
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? DOB {get; set;}
        public bool IsAdmin {get; set;}
        public UserDTO () { }
    }
}