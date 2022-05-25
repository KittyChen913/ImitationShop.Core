namespace ImitationShop.Repository;

public interface IRepository<TModel>
{
    Task<List<TModel>> Query();
    Task<TModel> QueryById(object id);
}
