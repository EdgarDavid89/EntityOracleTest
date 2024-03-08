
using System;
using DataAccess.DTO;

namespace DataAccess.Business
{
    public interface IParkingB<TId>
    {
        Task<ParkingDto> GetParkingById(TId id);
        Task<IEnumerable<ParkingDto>> GetParkings();
        Task<ParkingDto> CreateParking(WorkerDto worker);
        Task<ParkingDto> UpdateParking(WorkerDto worker, TId id);
        Task<ParkingDto> DeleteParking(TId id);
    }
}