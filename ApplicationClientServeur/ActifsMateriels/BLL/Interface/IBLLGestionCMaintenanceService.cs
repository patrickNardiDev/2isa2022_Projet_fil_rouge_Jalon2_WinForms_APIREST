using Domain.DTO.Gestion;

namespace BLL.Interface;

public interface IBLLGestionCMaintenanceService
{
    public Task<GestionCMaintenanceResponseDto> GetGestionCMaintenanceAsync();
}
