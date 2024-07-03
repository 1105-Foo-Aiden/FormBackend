namespace FormBackend.Model.DTOS{
    public class CreateAccountDTO{
        public int ID {get; set;}
        public string? Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public CreateAccountDTO(){ }
    }
}