namespace ImitationShop.Repository;

public interface IStoreRepository : IBaseRepository<Store>
{
    Task<IEnumerable<Store>> GetStoreItemList(int userId);
}
