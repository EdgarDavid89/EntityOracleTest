
using System;
using DataAccess.DTO;

namespace DataAccess.Business
{
    public interface IWorkerB<TId>
    {
        Task<WorkerDto> GetWorkerById(TId id);
        Task<IEnumerable<WorkerDto>> GetWorkers();
        Task<WorkerDto> CreateWorker(WorkerDto worker);
        Task<WorkerDto> UpdateWorker(WorkerDto worker, TId id);
        Task<WorkerDto> DeleteWorker(TId id);
    }
}