using Domain.Entities;
using GestionMaterielIntExt.ModelsOfView.Consultation.Interface;

namespace GestionMaterielIntExt.Controller.Consultation.Interface;
public interface IControllerConsultation
{
    public Task<bool> OpenViewConsultation();

    public Task<IMViewConsultation> GetMaterielsInfos();

    public Task<IMViewConsultation> GetMaterielsInfos(Categorie categorie);

    public void CloseViewConsultation();
}
