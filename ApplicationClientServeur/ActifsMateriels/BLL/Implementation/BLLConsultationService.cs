using BLL.Interface;
using DAL;
using Domain.DTO.Consultation;

namespace BLL.Implementation;

internal class BLLConsultationService : IBLLConsultationService
{
    private readonly IUOW _dbContext;

    public BLLConsultationService(IUOW dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retourne un ConsultationResponseDto suivant l'id d'une catégorie
    /// </summary>
    /// <param name="categorie">long</param>
    /// <returns>Iénumérables de Categorie et MaterielsInfos, ou null</returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ConsultationResponseDto> GetConsultationMaterielsAsync(long categorie)
    {
        // 1- j'appel la fonction GetMaterielsInfos(categorie) du _dbContext en lui passant en argument la catégorie pour récupérer un IEnumerable<MaterielsInfos>
        // 2- je vérifie si la catégorie n'est pas nule
        //  2.1 si non nule alors :
        //      2.1.1 je hash le password fornit
        //      2.1.2 je vérifie la correspondance entre les hash deu password fournit et celui reçu par la fonction GetByEmail du _dbContext
        //          2.1.2.1 si vrai alors :
        //               2.1.2.1.1 je retourne le user
        //          2.1.2.2 si non
        //               2.1.2.2.1 je lève une exeption personnelle

        var enumMaterielsOnfos = await _dbContext.MaterielsInfos.GetByCategorie(categorie); ;
        var enumCategories = await _dbContext.Categorie.GetAllAsync();
        var enumMatCat = await _dbContext.MaterielCategorie.GetAllAsync(null);

        ConsultationResponseDto reponse = new();
        reponse.IenumMaterielsInfos = enumMaterielsOnfos;
        reponse.IenumCategories = enumCategories;
        reponse.IenummaterielCategories = enumMatCat;
        return reponse;
    }

    /// <summary>
    /// Retourne un ConsultationResponseDto
    /// </summary>
    /// <returns>Iénumérables de Categorie et MaterielsInfos, ou null</returns>
    public async Task<ConsultationResponseDto> GetConsultationMaterielsAsync()
    {
        // 1- j'appelle le IUOW pour accéder au repository IMaterielsInfosRepository
        // et récupéré une IEnumérable de MaterielsInfos
        // 2- jappelle le IOW pour accéder au repository ICategorieRepository
        // et je récupère un IEnumérable de categorie
        // 3- suivant les réponses deux réponses :
        //  3.1 si non nules alors:
        //      3.1.1 je crée un Dto de réponse consultation
        //      3.1.2 je change l'attribut IenumMaterielsInfos avec la réponse de 1
        //      3.1.3 je change l'attribut IenumCategories avec la réponse de 2
        //      3.1.3 je retourne le Dto
        //
        var enumMaterielsOnfos = await _dbContext.MaterielsInfos.GetAllAsync();
        var enumCategories = await _dbContext.Categorie.GetAllAsync();
        var enumMatCat = await _dbContext.MaterielCategorie.GetAllAsync(null);


        ConsultationResponseDto reponse = new();
        reponse.IenumMaterielsInfos = enumMaterielsOnfos;
        reponse.IenumCategories = enumCategories;
        reponse.IenummaterielCategories = enumMatCat;
        return reponse;
    }
}
