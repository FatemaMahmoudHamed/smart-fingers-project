using System.Threading.Tasks;

namespace AnamelTask.Repositories.UserDataRepositories.UOW
{
    public interface IUnitOfWork<T>
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
