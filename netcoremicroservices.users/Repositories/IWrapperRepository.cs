namespace NetCoreMicroservices.Users.Repositories
{
    public interface IWrapperRepository
    {
       
        IUserRepository User { get; }
        Task<int> SaveAsync();

        int Save();
    }
}
