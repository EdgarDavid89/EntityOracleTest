using System;
using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Business
{   
    public class WorkerB
    {
      private readonly IRepository<Worker, decimal> _workerRepository; 
      public WorkerB(IRepository<Worker, decimal> workerRepository)
      {
        _workerRepository = workerRepository;
      }

      public async Task<WorkerDto> GetWorker(decimal id)
      {
        var worker = await _workerRepository.GetByIdAsync(id);
        return new WorkerDto
        {
          Id = worker.Id,
          Name = worker.Name,
          Age = worker.Age,
          Rfc = worker.Rfc
        };
      }



    }
}