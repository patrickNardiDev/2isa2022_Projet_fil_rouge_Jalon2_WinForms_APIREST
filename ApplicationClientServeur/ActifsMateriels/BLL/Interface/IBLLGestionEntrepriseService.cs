using Domain.DTO.Gestion;

namespace BLL.Interface;

public interface IBLLGestionEntrepriseService
{
    public Task<GestionEntrepriseResponseDto> GetGestionEntrepriseAsync();

}
