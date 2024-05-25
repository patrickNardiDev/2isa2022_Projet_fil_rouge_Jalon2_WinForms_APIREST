using BLL.Interface;
using DAL;
using Domain.DTO.Gestion;
using Domain.ValueObjects;

namespace BLL.Implementation;

internal class BLLGestionEntrepriseService : IBLLGestionEntrepriseService
{

    private readonly IUOW _dbContext;

    public BLLGestionEntrepriseService(IUOW dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<GestionEntrepriseResponseDto> GetGestionEntrepriseAsync()
    {
        // je construit la réponse
        GestionEntrepriseResponseDto reponseDto = new();

        // je récupère les éléments piur la constitution de du CMaintenanceInfos
        var enumEntreprises = await _dbContext.Entreprise.GetAllAsync();
        //var enumMateriels = await _dbContext.Materiel.GetAllAsync();
        var enumMContrat = await _dbContext.ContratMaintenance.GetAllAsync();

        // je réalise une prè-requête pour compter le nombre de matériel par contrat
        var MatsContrat = from entreprise in enumEntreprises
                          join mcontrat in enumMContrat on entreprise.Id equals mcontrat.IdEntreprise
                          select new
                          {
                              NomEntreprise = entreprise.Nom,
                              IdEntreprise = entreprise.Id,
                              IdMat = mcontrat.Id
                          };
        var a = MatsContrat;

        // je constitu la requête pour la création du enumérable de CMaintenanceInfos
        var GestionCMaintenanceInfos = from mcontrat in enumMContrat
                                       join entreprise in enumEntreprises on mcontrat.IdEntreprise equals entreprise.Id
                                       select new EntrepriseGestion
                                       {

                                           Id = (long)entreprise?.Id,
                                           Nom = entreprise.Nom,
                                           Tel = entreprise.Tel,
                                           Email = entreprise?.Email ?? null,
                                           LastModif = entreprise.LastModif,
                                           Archive = entreprise.Archive,

                                           NbContrat = (from matcontrat in MatsContrat where matcontrat.IdEntreprise == entreprise.Id select matcontrat).Count(),

                                       };
        var b = GestionCMaintenanceInfos.DistinctBy(x => x.Id);
        // je complète la réponse
        reponseDto.EnumEntrepriseInfos = GestionCMaintenanceInfos.DistinctBy(x => x.Id).OrderBy(x => x.Id);

        return reponseDto;
    }
}
