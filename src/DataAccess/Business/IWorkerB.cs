
using System;
using DataAccess.DTO;

namespace DataAccess.Business
{
    public interface IWorkerB
    {
        Task<WorkerDto> GetWorker(decimal id);
    }
}