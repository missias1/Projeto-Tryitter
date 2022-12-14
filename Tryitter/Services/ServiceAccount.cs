using Tryitter.Models;
using Tryitter.Repository;
using Tryitter.Autentication;
using Tryitter.Schemas;

namespace Tryitter.Services
{
    public class ServiceAccount
    {
        private readonly IRepositoryAccount _repository;

        public ServiceAccount(IRepositoryAccount repository)
        {
            _repository = repository;
        }

        public User UpdateAccount(UpdateAccount user, int idToken)
        {
            var result = _repository.GetAccountById(user.Id);

            if (result == null) throw new InvalidOperationException("Usuário não existe");
            if (result.Id != idToken) throw new InvalidOperationException("Operação não permitida");

            var student = _repository.UpdateAccount(user);
            return student;
        }

        public void DeleteAccount(int id, int idToken)
        {
            var result = _repository.GetAccountById(id);
            if (result == null) throw new InvalidOperationException("Usuário não existe"); 

            if(result.Id != idToken) throw new InvalidOperationException("Operação não permitida");

            _repository.DeleteAccount(id);
        }

        public User RegisterAccount(User user)
        {
            var result = _repository.GetAccountByEmail(user.Email);
            if (result != null) throw new InvalidOperationException("Email já cadastrado");

            var userCreated = _repository.RegisterAccount(user);

            return userCreated;
        }

        public LoginResult LoginAccount(Login user)
        {
            var studentRegistered = _repository.EmailExist(user.email);

            if (!studentRegistered) throw new InvalidOperationException("Usuário não cadastrado");

            var student = _repository.LoginAccount(user);

            if(student == null) throw new InvalidOperationException("Email ou senha incorretos!");

            var token = new TokenGenerator().Generate(student.Nome, user.email, student.Id);

            var result = new LoginResult
            {
                Id = student.Id,
                Token = token,
            };

            return result;
        }

        public User GetAccount(int id)
        {
            var result = _repository.GetAccountById(id);
            if (result == null) throw new InvalidOperationException("Conta não existente!");
            return result;
        }
    }
}
