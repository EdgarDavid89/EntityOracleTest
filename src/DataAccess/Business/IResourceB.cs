using System;
using DataAccess.DTO;

namespace DataAccess.Business
{
    public interface IResourceB
    {
        Task<ResourceDto> GetResourceById<TId>(TId id);
        Task<IEnumerable<ResourceDto>> GetResources();
        Task<ResourceDto> CreateResource(ResourceDto worker);
        Task<ResourceDto> UpdateResource<TId>(ResourceDto worker, TId id);
    }
}