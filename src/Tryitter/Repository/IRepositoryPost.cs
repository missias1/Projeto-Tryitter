using Tryitter.Models;

namespace Tryitter.Repository
{
    public interface IRepositoryPost
    {
        Post AddPost(Post post);
        Post UpdatePost(Post post);
        void DeletePost(int id);
        ICollection<Post> GetAllPosts(int userId);
        Post GetLastPost(int userId);
        Post GetPostById(int id);

    }
}
