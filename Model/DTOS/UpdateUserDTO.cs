namespace FormBackend.Model.DTOS{
    public class UpdateUserDTO{
        public string? Username {get; set;}
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DOB { get; set; }
        public bool IsAdmin {get; set;}
    }
}