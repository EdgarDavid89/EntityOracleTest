using DataAccess.DTO;
using AutoMapper;
using DataAccess.Models;
namespace DataAccess.Business
{
    public class ParkingB : IParkingB
    {
        private readonly IRepository<Parking> _parkingRepository;
        private readonly IRepository<Worker> _workerRepository;
        private readonly IMapper _mapper;
        public ParkingB(IRepository<Parking> parkingRepository,
        IRepository<Worker> workerRepository,
          IMapper mapper)
        {
            _parkingRepository = parkingRepository;
            _mapper = mapper;
            _workerRepository = workerRepository;
        }

        public async Task<IEnumerable<ParkingDto>> GetParkings()
        {
            var parkings = await _parkingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ParkingDto>>(parkings);
        }

        public async Task<ParkingDto> CreateParking(ParkingDto parking)
        {
            parking.Id = parking.Id > 0 || parking.Id < 0  ? 0: parking.Id;
            var parkingEntity = _mapper.Map<Parking>(parking);
            var newParking = await _parkingRepository.InsertAsync(parkingEntity);
            return _mapper.Map<ParkingDto>(newParking);
        }

        public async Task<ParkingDto> DeleteParking<TId>(TId id)
        {
            var deletedParking = await _parkingRepository.DeleteAsync(id);
            return _mapper.Map<ParkingDto>(deletedParking);
        }

        public async Task<ParkingDto> GetParkingById<TId>(TId id)
        {
            var parking = await _parkingRepository.GetByIdAsync(id);
            return _mapper.Map<ParkingDto>(parking);
        }

        public async Task<ParkingDto> UpdateParking<TId>(ParkingDto parking, TId id)
        {
            var parkingUpdate = _mapper.Map<Parking>(parking);
            var updatedParking = await _parkingRepository.UpdateAsync(parkingUpdate, id);
            return _mapper.Map<ParkingDto>(updatedParking);
        }

        public async Task<IEnumerable<WorkerDto>> GetWorkersByParking<TId>(TId id)
        {
            var workers = await _workerRepository.GetByFilterAsync(w => w.Parkingid == Convert.ToDecimal(id));
            return _mapper.Map<IEnumerable<WorkerDto>>(workers); 
        }


       
    }
}