namespace FormBackend.Model.DTOS{
    public class UpdateUserDTO{
        public int id {get; set;}
        public string? Username {get; set;}
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? DOB { get; set; }
        public bool IsAdmin {get; set;}
        public UpdateUserDTO() { }
    }
}