namespace WebAPI1.DTO
{
    public class RegisterUserDto
    {
        //DTO (Encryt Column NAme ,take required data only not all field
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
