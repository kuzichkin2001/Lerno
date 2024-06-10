using Lerno.DataAccess.DbContexts;

namespace Lerno.DataAccess.Repos
{
    public class BaseRepository
    {
        protected readonly UnitOfWork _unitOfWork;

        protected BaseRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
