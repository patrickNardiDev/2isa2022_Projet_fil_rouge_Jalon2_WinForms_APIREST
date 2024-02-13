using Domain.DTO.Consultation;
using GestionMaterielIntExt.Model.Consultation.Interface;
using GestionMaterielIntExt.ObjTransf.Consultation;
using GestionMaterielIntExt.Proxy.Consultation.Interface;

namespace GestionMaterielIntExt.Model.Consultation.Implementation;

internal class ModelConsultation : IModelConsultation
{
    //ID
    private readonly IProxiConsultation _pconsult;

    public ModelConsultation(IProxiConsultation pconsult)
    {
        _pconsult = pconsult;
    }

    #region GetConsultationMateriels
    public async Task<TrConsultationResponse> GetConsultationMateriels()
    {// 1- j'appelle le la fonction GetConsultationMateriels() du modèle de consultation
        // 2- je renvoie le Dto de consultaion
        return await _pconsult.GetConsultationMateriels2();

    }
    #endregion

    #region GetMaterielsInfos filtre par catégorie
    //public async Task<ConsultationResponseDto> GetConsultationMateriels(long categorie)
    //{
    //    // 1- j'appelle le la fonction GetConsultationMateriels(categorie) du modèle de consultation
    //    // 2- je renvoie le Dto de consultaion
    //    return await _pconsult.GetConsultationMateriels(categorie);
    //}

    public async Task<TrConsultationResponse> GetConsultationMateriels(long categorie)
    {
        // 1- j'appelle le la fonction GetConsultationMateriels(categorie) du modèle de consultation
        // 2- je renvoie le Dto de consultaion
        return await _pconsult.GetConsultationMateriels(categorie);
    }


    #endregion
}
