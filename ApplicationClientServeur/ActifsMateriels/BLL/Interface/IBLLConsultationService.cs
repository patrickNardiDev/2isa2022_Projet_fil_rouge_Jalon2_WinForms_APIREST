using Domain.DTO.Consultation;

namespace BLL.Interface;

public interface IBLLConsultationService
{
    public Task<ConsultationResponseDto> GetConsultationMaterielsAsync();
    public Task<ConsultationResponseDto> GetConsultationMaterielsAsync(long categorie);

}
