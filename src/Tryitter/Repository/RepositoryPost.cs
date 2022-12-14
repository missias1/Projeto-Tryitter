using Tryitter.Models;
using Tryitter.Context;

namespace Tryitter.Repository
{
    public class RepositoryPost: IRepositoryPost
    {
        private readonly IMyContext _context;

        public RepositoryPost(IMyContext context)
        {
            _context = context;
        }

        public Post AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            var result = _context.Posts.Where(e => e.Mensagem == post.Mensagem && e.UserId == post.UserId).FirstOrDefault();
            return result;
        }

        public Post UpdatePost(Post post)
        {
            var result = _context.Posts.Where(e => e.Id == post.Id).FirstOrDefault();

            result.Mensagem = post.Mensagem;
            result.Imagem = post.Imagem;
            result.Editado = post.Editado;
            result.Date = post.Date;

            _context.SaveChanges();

            var resultUpdate = _context.Posts.Where(e => e.Id == post.Id).FirstOrDefault();
            return resultUpdate;
        }

        public void DeletePost(int id)
        {
            var post = _context.Posts.Where(e => e.Id == id).First();
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public ICollection<Post> GetAllPosts(int userId)
        {
            var posts = _context.Posts.Where(e=> e.UserId== userId).ToList();
            return posts;
        }

        public Post GetLastPost(int userId)
        {
            var posts = _context.Posts.Where(e => e.UserId == userId).ToList();
            var lastIndex = posts.Count -1;
            return posts[lastIndex];
        }

        public Post GetPostById(int id)
        {
            var post = _context.Posts.Where(e=> e.Id == id).FirstOrDefault();
            return post;
        }
    }
}
