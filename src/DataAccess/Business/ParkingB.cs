using DataAccess.DTO;
using AutoMapper;
using DataAccess.Models;
namespace DataAccess.Business
{
    public class ParkingB<TId> : IParkingB<TId>
    {
        private readonly IRepository<Parking, TId> _parkingRepository;
        private readonly IRepository<Worker, TId> _workerRepository;
        private readonly IMapper _mapper;
        public ParkingB(IRepository<Parking, TId> parkingRepository,
        IRepository<Worker, TId> workerRepository,
          IMapper mapper)
        {
            _parkingRepository = parkingRepository;
            _mapper = mapper;
            _workerRepository = workerRepository;
        }

        public async Task<ParkingDto> CreateParking(WorkerDto worker)
        {
            throw new NotImplementedException();
        }

        public async Task<ParkingDto> DeleteParking(TId id)
        {
            throw new NotImplementedException();
        }

        public async Task<ParkingDto> GetParkingById(TId id)
        {
            var parking = await _parkingRepository.GetByIdAsync(id);
            if(parking != null)
            {
                 var workers = await _workerRepository.GetByFilterAsync(w => w.Parkingid == parking.Id);
                 parking.Workers = workers.ToList(); 
            }
            return _mapper.Map<ParkingDto>(parking);
        }

        public async Task<IEnumerable<ParkingDto>> GetParkings()
        {
            var parking = await _parkingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ParkingDto>>(parking);
        }

        public async Task<ParkingDto> UpdateParking(WorkerDto worker, TId id)
        {
            throw new NotImplementedException();
        }
    }
}