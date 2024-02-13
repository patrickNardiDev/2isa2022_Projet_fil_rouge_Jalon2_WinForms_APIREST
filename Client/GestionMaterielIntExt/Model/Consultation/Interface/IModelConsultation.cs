using Domain.DTO.Consultation;
using GestionMaterielIntExt.ObjTransf.Consultation;

namespace GestionMaterielIntExt.Model.Consultation.Interface
{
    public interface IModelConsultation
    {
        public Task<TrConsultationResponse> GetConsultationMateriels();
        public Task<TrConsultationResponse> GetConsultationMateriels(long categorie);
    }
}
