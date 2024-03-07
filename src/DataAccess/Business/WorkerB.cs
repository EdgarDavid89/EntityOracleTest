using System;
using DataAccess.DTO;
using DataAccess.Models;
using AutoMapper;

namespace DataAccess.Business
{   
    public class WorkerB<TId>: IWorkerB<TId>
    {
      private readonly IRepository<Worker, TId> _workerRepository;
      private readonly IMapper _mapper; 
      public WorkerB(IRepository<Worker, TId> workerRepository,
        IMapper mapper)
      {
        _workerRepository = workerRepository;
        _mapper = mapper;
      }

      public async Task<WorkerDto> GetWorkerById(TId id)
      {
        var worker = await _workerRepository.GetByIdAsync(id);
        return _mapper.Map<WorkerDto>(worker);   
      }

      public async Task<IEnumerable<WorkerDto>> GetWorkers()
      {
        var workers = await _workerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<WorkerDto>>(workers);   
      }

      public async Task<WorkerDto> CreateWorker(WorkerDto worker)
      {
        worker.Id = worker.Id > 0 || worker.Id < 0  ? 0: worker.Id;
        var workerEntity = _mapper.Map<Worker>(worker);
        var createdWorker = await _workerRepository.InsertAsync(workerEntity);
        return _mapper.Map<WorkerDto>(createdWorker);   
      }

      public async Task<WorkerDto> UpdateWorker(WorkerDto worker, TId id)
      {
        var workerUpdate = _mapper.Map<Worker>(worker);
        var updatedWorker = await _workerRepository.UpdateAsync(workerUpdate, id); 
        return _mapper.Map<WorkerDto>(updatedWorker);
      }

      public async Task<WorkerDto> DeleteWorker(TId id)
      {
        var deletedWorker = await _workerRepository.DeleteAsync(id);
        return _mapper.Map<WorkerDto>(deletedWorker);  
      }

    }
}