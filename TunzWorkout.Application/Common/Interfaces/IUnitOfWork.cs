namespace TunzWorkout.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitChangesAsync();
        Task BeginTransactionAsync();
    }
}
