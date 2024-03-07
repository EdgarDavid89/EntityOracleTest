using System;
using DataAccess.DTO;
using DataAccess.Models;
using AutoMapper;

namespace DataAccess.Business
{   
    public class WorkerB: IWorkerB
    {
      private readonly IRepository<Worker, decimal> _workerRepository;
      private readonly IMapper _mapper; 
      public WorkerB(IRepository<Worker, decimal> workerRepository,
        IMapper mapper)
      {
        _workerRepository = workerRepository;
        _mapper = mapper;
      }

      public async Task<WorkerDto> GetWorker(decimal id)
      {
        var worker = await _workerRepository.GetByIdAsync(id);
        return _mapper.Map<WorkerDto>(worker);   
      }

    }
}