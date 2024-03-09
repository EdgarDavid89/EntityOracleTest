
using System;
using DataAccess.DTO;

namespace DataAccess.Business
{
    public interface IWorkerB
    {
        Task<WorkerDto> GetWorkerById<TId>(TId id);
        Task<IEnumerable<WorkerDto>> GetWorkers();
        Task<WorkerDto> CreateWorker(WorkerDto worker);
        Task<WorkerDto> UpdateWorker<TId>(WorkerDto worker, TId id);
        Task<WorkerDto> DeleteWorker<TId>(TId id);
    }
}