namespace ImitationShop.Repository;

public interface IRepository<TModel>
{
    Task<IEnumerable<TModel>> Query();
    Task<TModel> QueryById(object id);
    Task<TModel> QueryByString(string searchValue);
    Task<int> Add(TModel model);
}
