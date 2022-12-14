namespace Tryitter.Schemas
{
    public class NewPost
    {
        public string Mensagem { get; set; } = null!;
        public string Imagem { get; set; } = null!;
        public int UserId { get; set; }
    }
}
