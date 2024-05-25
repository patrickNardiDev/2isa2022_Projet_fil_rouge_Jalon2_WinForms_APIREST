using BLL.Interface;
using DAL;
using Domain.DTO.Gestion;
using Domain.ValueObjects;

namespace BLL.Implementation;

internal class BLLGestionCMaintenanceService : IBLLGestionCMaintenanceService
{
    private readonly IUOW _dbContext;

    public BLLGestionCMaintenanceService(IUOW dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GestionCMaintenanceResponseDto> GetGestionCMaintenanceAsync()
    {
        // je construit la réponse
        GestionCMaintenanceResponseDto reponseDto = new();

        // je récupère les éléments piur la constitution de du CMaintenanceInfos
        var enumEntreprises = await _dbContext.Entreprise.GetAllAsync();
        var enumMateriels = await _dbContext.Materiel.GetAllAsync();
        var enumMContrat = await _dbContext.ContratMaintenance.GetAllAsync();

        // je réalise une prè-requête pour compter le nombre de matériel par contrat
        var MatsContrat = from materiel in enumMateriels
                          join mcontrat in enumMContrat on materiel.IdMContrat equals mcontrat.Id
                          select new
                          {
                              mcontrat.Nom,
                              materiel.IdMContrat,
                              IdMat = materiel.Id
                          };
        var a = MatsContrat;

        // je constitu la requête pour la création du enumérable de CMaintenanceInfos
        var GestionCMaintenanceInfos = from mcontrat in enumMContrat
                                       join entreprise in enumEntreprises on mcontrat.IdEntreprise equals entreprise.Id
                                       select new CMaintenanceGestion
                                       {

                                           Id = mcontrat.Id,
                                           Nom = mcontrat.Nom,
                                           Info = mcontrat.Info,
                                           NbMat = (from matcontrat in MatsContrat where matcontrat.IdMContrat == mcontrat.Id select matcontrat).Count(),
                                           DateDebut = mcontrat.DateDebut,
                                           DateFin = mcontrat.DateFin,
                                           DateDerniereIntervention = mcontrat?.DateDerniereIntervention ?? null,
                                           DateProchaineIntervention = mcontrat?.DateProfaineIntervention ?? null,
                                           IdEntreprise = mcontrat.IdEntreprise,
                                           NomEntreprise = entreprise.Nom,
                                           Archive = mcontrat.Archive,
                                           LastModif = mcontrat.LastModif,
                                       };
        // je complète la réponse
        reponseDto.EnumCMaintenanceInfos = GestionCMaintenanceInfos.OrderBy(x => x.Id);

        return reponseDto;
    }
}
