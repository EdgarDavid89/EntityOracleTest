using System;
using AutoMapper;
using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Business
{
    public class ResourceB : IResourceB
    {

        private readonly IRepository<Resource> _resourceRepository;
        private readonly IMapper _mapper;
        public ResourceB(IRepository<Resource> resourceRepository,
          IMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResourceDto>> GetResources()
        {
            var parkings = await _resourceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ResourceDto>>(parkings);
        }

        public async Task<ResourceDto> CreateResource(ResourceDto resource)
        {
            resource.Id = resource.Id > 0 || resource.Id < 0  ? 0: resource.Id;
            var resourceEntity = _mapper.Map<Resource>(resource);
            var newResource = await _resourceRepository.InsertAsync(resourceEntity);
            return _mapper.Map<ResourceDto>(newResource);
        }

        public async Task<ResourceDto> GetResourceById<TId>(TId id)
        {
            var resource = await _resourceRepository.GetByIdAsync(id);
            return _mapper.Map<ResourceDto>(resource);
        }

        public async Task<ResourceDto> UpdateResource<TId>(ResourceDto resource, TId id)
        {
            var resourceUpdate = _mapper.Map<Resource>(resource);
            var updatedResource = await _resourceRepository.UpdateAsync(resourceUpdate, id);
            return _mapper.Map<ResourceDto>(updatedResource);
        }
        
    }
}