namespace BE.Domain.Entities
{
    public class User : AuditoriaBase
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public Roles Role { get; set; }
    }

    public enum Roles
    {
        ADMINISTRADOR,
        USUARIO
    }
}
