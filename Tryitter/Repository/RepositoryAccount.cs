using Tryitter.Models;
using Tryitter.Context;
using Tryitter.Schemas;

namespace Tryitter.Repository
{
    public class RepositoryAccount: IRepositoryAccount
    {
        private readonly IMyContext _context;

        public RepositoryAccount(IMyContext context)
        {
            _context = context;
        }
        public User UpdateAccount(UpdateAccount user)
        {
            var result = _context.Users.Where(e => e.Id == user.Id).FirstOrDefault();
            
            if (result != null)
            {
                result.Modulo = user.Modulo;
                result.Status = user.Status;
            }

            _context.SaveChanges();

            return result;
        }

        public void DeleteAccount(int id)
        {
            var user = _context.Users.Where(e => e.Id == id).FirstOrDefault();

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User RegisterAccount(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            var result = _context.Users.Where(e => e.Email == user.Email).First();
            return result;
        }

        public bool EmailExist(string email)
        {
            var result = _context.Users.Where(e => e.Email == email);
            if (result != null) return true;

            return false;
        }

        public User LoginAccount(Login user)
        {
            var result = _context.Users.Where(e => e.Email == user.email && e.Senha == user.senha).FirstOrDefault();
            return result;
        }

        public User GetAccountById(int id)
        {
            var user = _context.Users.Where(e => e.Id == id).FirstOrDefault();
            return user;
        }

        public User GetAccountByEmail(string email)
        {
            var user = _context.Users.Where(e => e.Email == email).FirstOrDefault();
            return user;
        }
    }
}
