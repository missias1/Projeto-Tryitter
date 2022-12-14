using System.ComponentModel.DataAnnotations;

namespace Tryitter.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(300)]
        public string Mensagem { get; set; } = null!;
        public string Imagem { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool Editado { get; set; }
    }
}
