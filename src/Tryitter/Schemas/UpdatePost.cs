namespace Tryitter.Schemas
{
    public class UpdatePost
    {
        public string Mensagem { get; set; }
        public string Imagem { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
