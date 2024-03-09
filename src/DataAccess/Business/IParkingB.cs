
using System;
using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Business
{
    public interface IParkingB
    {
        Task<ParkingDto> GetParkingById<TId>(TId id);
        Task<IEnumerable<ParkingDto>> GetParkings();
        Task<ParkingDto> CreateParking(ParkingDto parking);
        Task<ParkingDto> UpdateParking<TId>(ParkingDto parking, TId id);
        Task<ParkingDto> DeleteParking<TId>(TId id);
        Task<IEnumerable<WorkerDto>> GetWorkersByParking<TId>(TId id);
    }
}