using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using Tryitter.Models;
using Tryitter.Repository;

namespace Tryitter.Services
{
    public class ServicePost
    {
        private readonly IRepositoryPost _repository;
        private readonly IRepositoryAccount _repositoryAccount;

        public ServicePost(IRepositoryPost repository, IRepositoryAccount repositoryAccount)
        {
            _repository = repository;
            _repositoryAccount = repositoryAccount;
        }

        public Post AddPost(Post post, int id)
        {
            var result = _repositoryAccount.GetAccountById(post.UserId);

            if (result == null) throw new InvalidOperationException("Usuário não existe");
            if (result.Id != id) throw new InvalidOperationException("Operação não permitida");

            return _repository.AddPost(post);
        }

        public void DeletePost(int id, int idToken)
        {
            var result = _repository.GetPostById(id);

            if (result == null) throw new InvalidOperationException("Post não existe");
            if (result.UserId != idToken) throw new InvalidOperationException("Operação não permitida");

            _repository.DeletePost(id);
        }

        public ICollection<Post> GetAllPosts(int userId)
        {
            var user = _repositoryAccount.GetAccountById(userId);

            if (user == null) throw new InvalidOperationException("Usuário não encontrado");

            var posts = _repository.GetAllPosts(userId);
            return posts;
        }

        public Post GetLastPost(int userId)
        {
            var user = _repositoryAccount.GetAccountById(userId);
            if (user == null) throw new InvalidOperationException("Usuário não existente");

            var post = _repository.GetLastPost(userId);

            return post;
        }

        public Post UpdatePost(Post post, int id)
        {
            var user = _repositoryAccount.GetAccountById(post.UserId);

            var postResult = _repository.GetPostById(post.Id);

            if (user == null || postResult == null) throw new InvalidOperationException("Usuário ou post inexistente");

            if(id != post.UserId) throw new InvalidOperationException("Operação não permitida");

            return _repository.UpdatePost(post);
        }
    }
}
