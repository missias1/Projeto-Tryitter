namespace Tryitter.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public string Modulo { get; set; } = null!;
        public string Status { get; set; } = null!;
        public ICollection<Post>? Posts { get; set; }
    }
}
