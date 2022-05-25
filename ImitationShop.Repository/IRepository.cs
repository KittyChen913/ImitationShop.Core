namespace ImitationShop.Repository;

public interface IRepository<TModel>
{
    Task<List<TModel>> Query();
    Task<TModel> QueryById(object id);
    Task<TModel> QueryByString(string searchValue);
    Task<int> Add(TModel model);
}
