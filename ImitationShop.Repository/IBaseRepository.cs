﻿namespace ImitationShop.Repository;

public interface IBaseRepository<TModel> where TModel : class
{
    Task<IEnumerable<TModel>> Query();
    Task<TModel> QueryById(object primaryKeyId);
    Task<TModel> Add(TModel model);
}