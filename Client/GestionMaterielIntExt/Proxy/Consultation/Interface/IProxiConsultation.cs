using Domain.DTO.Consultation;
using GestionMaterielIntExt.ObjTransf.Consultation;

namespace GestionMaterielIntExt.Proxy.Consultation.Interface
{
    internal interface IProxiConsultation
    {

        public Task<TrConsultationResponse> GetConsultationMateriels2();
        //public Task<ConsultationResponseDto> GetConsultationMateriels(long categorie);
        public Task<TrConsultationResponse> GetConsultationMateriels(long categorie);

    }
}
