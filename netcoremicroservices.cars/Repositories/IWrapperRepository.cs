namespace NetCoreMicroservices.Cars.Repositories
{
    public interface IWrapperRepository
    {
       
        ICarRepository Car { get; }
        Task<int> SaveAsync();

        int Save();
    }
}
