﻿using RentACarNow.Common.Models;

namespace RentACarNow.Common.Infrastructure.Services.Interfaces
{
    public interface IHttpService
    {
        //Task<TResult> GetAsync<TResult, TParam>(string path, TParam param) where TParam : class;

        Task<TResult> GetByIdAsync<TResult>(string url, Guid id);
        Task<TResult> GetAllAsync<TResult>(string path, PaginationParameter paginationParam, OrderingParameter orderingParam);

        Task<TResult> PostAsync<TResult, TParam>(string path, TParam param) where TParam : class;
        Task<TResult> PutAsync<TResult, TParam>(string path, TParam param) where TParam : class;
        Task<TResult> DeleteByIdAsync<TResult>(string path, Guid id);

    }
}
