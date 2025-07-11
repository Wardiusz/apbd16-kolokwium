using Kolokwium_APBD.DTOs;

namespace Kolokwium_APBD.Services;

public interface IDbService
{
    Task<NurseryBatchesDto> GetNurseryBatchesAsync(int nurseryId);
    Task<int> AddBatchAsync(NewBatchDto dto);
}