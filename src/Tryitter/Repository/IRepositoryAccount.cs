using Tryitter.Models;
using Tryitter.Schemas;

namespace Tryitter.Repository
{
    public interface IRepositoryAccount
    {
        User UpdateAccount(UpdateAccount user);
        void DeleteAccount(int id);
        User RegisterAccount(User user);
        bool EmailExist(string email);
        User LoginAccount(Login user);
        User GetAccountById(int id);
        User GetAccountByEmail(string email);
    }
}
