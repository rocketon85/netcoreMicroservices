namespace NetCoreMicroservices.Auth.Repositories
{
    public interface IWrapperRepository
    {
       
        IAuthRepository Auth { get; }
        Task<int> SaveAsync();

        int Save();
    }
}
