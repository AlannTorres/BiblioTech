namespace LibrarySystem_2.Domain
{
    public class User : BaseEntity
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
    }
}
