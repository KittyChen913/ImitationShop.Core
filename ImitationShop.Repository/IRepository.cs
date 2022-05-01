namespace ImitationShop.Repository
{
    public interface IRepository<TModel>
    {
        List<TModel> Query();
    }
}