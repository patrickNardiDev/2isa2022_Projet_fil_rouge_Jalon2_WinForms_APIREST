using BLL.Interface;
using DAL;
using Domain.DTO.Gestion;
using Domain.Entities;
using Domain.Exeption;
using Domain.ValueObjects;


namespace BLL.Implementation;

internal class BLLGestionMaterielService : IBLLGestionMaterielService
{
    private readonly IUOW _dbContext;

    public BLLGestionMaterielService(IUOW dbContext)
    {
        _dbContext = dbContext;
    }

    #region Get ------------------------------------------------------
    public async Task<GestionMaterielResponseDto> GetGestionMaterielsAsync()
    {
        return await FuncGetAllGestionMateriels();
    }


    public async Task<GestionMaterielResponseDto> GetGestionMaterielsByCatAsync(long idcategorie)
    {
        //long testId = 0;
        if (idcategorie <= 0) //TODO, voir avec test
        {
            throw new NotFoundEntitiesExeption();
        }
        else
        {
            var myMaterielGestion = await FuncGetAllGestionMateriels();
            var listCatTriee = myMaterielGestion.IenumMaterielCategories.Where(x => x.IdCategorie == idcategorie).ToList();
            var query = from matGestion in myMaterielGestion.EnumMaterielsGestion
                        join matcat in listCatTriee
                            on matGestion.IdMat equals matcat.IdMateriel
                        select new MaterielGestion
                        (
                            matGestion.IdMat,
                            matGestion.NomMat,
                            matGestion.DateMiseEnService,
                            matGestion.DateFinGarantie,
                            matGestion.IdUser,
                            matGestion.NomUser,
                            matGestion?.IdMContrat ?? null,
                            matGestion?.NomContrat ?? null,
                            matGestion.Archive,
                            matGestion.LastModif
                        );

            var listeTriee = query;
            //var listeTriee = myMaterielGestion.EnumMaterielsGestion.Where(x => x.IdCat == idcategorie).ToList();
            myMaterielGestion.EnumMaterielsGestion = listeTriee;
            return myMaterielGestion;
        }
    }

    public async Task<GestionMaterielResponseDto> GetGestionMaterielsByMatAsync(long idMateriel)
    {
        if (idMateriel <= 0) //TODO, voir avec test
        {
            throw new NotFoundEntitiesExeption();
        }
        else
        {
            var myMaterielGestion = await FuncGetAllGestionMateriels();
            //var myMaterielGestion = await _dbContext.Materiel.GetByIdAsync(idMateriel);

            var listeTriee = myMaterielGestion.EnumMaterielsGestion.Where(x => x.IdMat == idMateriel).ToList();
            myMaterielGestion.EnumMaterielsGestion = listeTriee;
            return myMaterielGestion;
        }
    }

    public async Task<Materiel> GetMaterielByIdAsync(long idMateriel)
    {
        if (idMateriel <= 0) //TODO, voir avec test
        {
            throw new NotFoundEntitiesExeption();
        }
        else
        {
            var materiel = await _dbContext.Materiel.GetByIdAsync(idMateriel);
            return materiel;
        }
    }


    private async Task<GestionMaterielResponseDto> FuncGetAllGestionMateriels()
    {
        GestionMaterielResponseDto reponseDto = new();

        var enumMaterielCategorie = await _dbContext.MaterielCategorie.GetAllAsync(null); // test d paramétrage => (null)
        var enumEntreprises = await _dbContext.Entreprise.GetAllAsync();
        var enumMateriels = await _dbContext.Materiel.GetAllAsync();
        var enumCategories = await _dbContext.Categorie.GetAllAsync();
        var enumMContrat = await _dbContext.ContratMaintenance.GetAllAsync();
        var enumUsers = await _dbContext.User.GetAllAsync();

        var GestionMaterielsInfos = from materiel in enumMateriels
                                    join mContrat in enumMContrat on materiel.IdMContrat equals mContrat.Id
                                        into a
                                    from subCMaintenance in a.DefaultIfEmpty()
                                    join user in enumUsers on materiel.IdUser equals user.id
                                        into b
                                    from subUser in b.DefaultIfEmpty()
                                    select new MaterielGestion()
                                    {
                                        IdMat = materiel.Id,
                                        NomMat = materiel.Nom,
                                        DateMiseEnService = materiel.DateMiseEnService,
                                        DateFinGarantie = materiel.DateFinGarantie,
                                        IdUser = subUser?.id ?? null,
                                        NomUser = subUser is not null ? subUser.firstname[0] + ". " + subUser.name : null,
                                        IdMContrat = subCMaintenance?.Id ?? null,
                                        NomContrat = subCMaintenance?.Nom ?? null,
                                        Archive = materiel.Archive,
                                        LastModif = materiel.LastModif,
                                    };
        reponseDto.EnumMaterielsGestion = GestionMaterielsInfos;
        reponseDto.EnumCategories = enumCategories;
        reponseDto.IenumMaterielCategories = enumMaterielCategorie;
        reponseDto.EnumCMaintenance = enumMContrat;

        var myListUserTransit = new List<UserTransit>();
        foreach (var user in enumUsers)
        {
            var myUseuShort = new UserTransit() // 8 TestU => correspondance entre UserTransit supposé et réalisé
            {
                Id = user.id,
                NameFormatted = user.firstname[0] + ". " + user.name,
                Email = user.email,
                Role = (Role.RoleEnum)user.idSIRole,
                AccessToken = string.Empty,
                IsConnected = true,
            };
            myListUserTransit.Add(myUseuShort);
        }
        reponseDto.EnumUsersTransit = myListUserTransit;

        return reponseDto;
    }
    #endregion

    #region Post ------------------------------------------------------
    public async Task<GestionMaterielResponseDto> PostGestionMaterielsAddAsync(GestionMatrielRequestDto gestionMatrielRequestDto)
    {
        if (gestionMatrielRequestDto.CRUDtype != CRUDtype.MyType.Add)
        {
            throw new InsertEntityException(new Materiel());
        }
        if (gestionMatrielRequestDto.NewNomMat == string.Empty || gestionMatrielRequestDto.NewNomMat is null)
        {
            GestionMaterielResponseDto resultError = await FuncGetAllGestionMateriels();
            resultError.ErrorMessage = "Vous devez nommer le matériel";
            resultError.IdMat = gestionMatrielRequestDto?.OldIdMat ?? -1;
            return resultError;
        }
        else if (gestionMatrielRequestDto.NewListIdCategories.Count() < 1)
        {
            GestionMaterielResponseDto resultError = await FuncGetAllGestionMateriels();
            resultError.ErrorMessage = "Le matériel doit avoir au moins une catégorie";
            resultError.IdMat = gestionMatrielRequestDto?.OldIdMat ?? -1;
            return resultError;
        }
        else
        {
            // je transforme le Dto de requête en Matériel et liste de catégorie
            Materiel newMateriel = new()
            {
                Nom = gestionMatrielRequestDto.NewNomMat,
                DateMiseEnService = gestionMatrielRequestDto.NewDMService,
                DateFinGarantie = gestionMatrielRequestDto.NewDFGarantie,
                IdUser = gestionMatrielRequestDto.NewIdUser,
                IdMContrat = gestionMatrielRequestDto.NewIdCMaintenance,
                //Archive = false,
                //LastModif = gestionMatrielRequestDto.LastModif,
            };

            // je commence la transaction
            _dbContext.BeginTransaction();

            // j'ajoute le matériel en récupérant l'id du nouveau matériel
            var newMat = await _dbContext.Materiel.AddAsync(newMateriel);

            // j'ajoute les catégorie au matériel
            foreach (var idCat in gestionMatrielRequestDto.NewListIdCategories)
            {
                var newMatCat = new MaterielCategorie()
                {
                    IdCategorie = idCat,
                    IdMateriel = newMat.Id,
                };
                await _dbContext.MaterielCategorie.AddAsync(newMatCat);
            }
            // je ferme la transaction
            _dbContext.Commit();

            // je récupère les catégorie du nouveau matériel - si récupération de la liste listIdMatCat, inutile
            List<MaterielCategorie> listMatCat = (await _dbContext.MaterielCategorie.GetAllAsync(newMat.Id)).ToList();

            // je fait la liste des catégorie en fonction de la liste
            List<Categorie> listCat = new();
            foreach (var matCat in listMatCat)
            {
                var enumCats = await _dbContext.Categorie.GetByIdCatAsync(matCat.IdCategorie);
                listCat.Add(enumCats);
            }

            // je crée le résultat
            GestionMaterielResponseDto result = await FuncGetAllGestionMateriels();
            result.IdMat = newMat.Id;
            result.CatOfIdMat = listCat;
            return result;
        }
    }

    #endregion

    #region Put ------------------------------------------------------
    public async Task<GestionMaterielResponseDto> PutGestionMaterielsUpdateAsync(GestionMatrielRequestDto gestionMatrielRequestDto)
    {
        if (gestionMatrielRequestDto.CRUDtype != CRUDtype.MyType.Modify)
        {
            throw new UpdateEntityException(new Materiel());
        }
        if (gestionMatrielRequestDto.NewNomMat == string.Empty || gestionMatrielRequestDto.NewNomMat is null)
        {
            GestionMaterielResponseDto resultError = await FuncGetAllGestionMateriels();
            resultError.ErrorMessage = "Vous devez nommer le matériel";
            resultError.IdMat = gestionMatrielRequestDto?.OldIdMat ?? -1;
            return resultError;
        }
        else if (gestionMatrielRequestDto.NewListIdCategories.Count() < 1)
        {
            GestionMaterielResponseDto resultError = await FuncGetAllGestionMateriels();
            resultError.ErrorMessage = "Le matériel doit avoir au moins une catégorie";
            resultError.IdMat = gestionMatrielRequestDto?.OldIdMat ?? -1;
            return resultError;
        }
        else
        {

            // je transforme le Dto de requête en Matériel et liste de catégorie
            Materiel MaterielModif = new()
            {
                Id = gestionMatrielRequestDto?.OldIdMat ?? -1,
                Nom = gestionMatrielRequestDto.NewNomMat,
                DateMiseEnService = gestionMatrielRequestDto.NewDMService,
                DateFinGarantie = gestionMatrielRequestDto.NewDFGarantie,
                IdUser = gestionMatrielRequestDto.NewIdUser,
                IdMContrat = gestionMatrielRequestDto.NewIdCMaintenance,
                Archive = gestionMatrielRequestDto.NewArchive,
                LastModif = gestionMatrielRequestDto.LastModif,
            };

            // je récupère la date de la dernière modification
            var lastModifInDatabase = (await _dbContext.Materiel.GetByIdAsync(MaterielModif.Id)).LastModif;

            // je vérifi qu'entre ma demande et maintenant personne n'a modifié ce même matériel
            if (MaterielModif.LastModif < lastModifInDatabase)
            {
                //return new GestionMaterielResponseDto(new SysException($"Le matériel dont l'ID est {MaterielModif.Id} fût modifié avant votre action."));
                return new GestionMaterielResponseDto()
                {
                    IdMat = MaterielModif.Id,
                    ErrorMessage = $"Le matériel dont l'ID est {MaterielModif.Id}, fût modifié avant votre action.",
                };
            }
            else
            {
                // je compare l'ancien avec le nouveau si différents alors le flux continue, comparaison mat et catégorie
                Materiel oldMat = await _dbContext.Materiel.GetByIdAsync(MaterielModif.Id);
                List<long> listOldMatCat = (await _dbContext.MaterielCategorie.GetAllAsync(MaterielModif.Id)).Select(x => x.IdCategorie).ToList();

                // je compare les listes de catégories old vs new
                List<long> idCatToAdd = new();
                List<long> idCatToDelete = new();
                foreach (var newIdCat in gestionMatrielRequestDto.NewListIdCategories)
                {
                    if (!listOldMatCat.Contains(newIdCat))
                        idCatToAdd.Add(newIdCat);
                }
                foreach (var oldIdCat in listOldMatCat)
                {
                    if (!gestionMatrielRequestDto.NewListIdCategories.Contains(oldIdCat))
                        idCatToDelete.Add(oldIdCat);
                }

                if (oldMat.CompareTo(MaterielModif) == 1 && idCatToAdd.Count() == 0 && idCatToDelete.Count() == 0)
                {
                    GestionMaterielResponseDto resultError = await FuncGetAllGestionMateriels();
                    resultError.ErrorMessage = $"Le matériel dont l'ID est {MaterielModif.Id}, ne comporte aucune modification.";
                    resultError.IdMat = gestionMatrielRequestDto?.OldIdMat ?? -1;
                    return resultError;
                    //return new GestionMaterielResponseDto(new SysException($"Vous n'apportez aucunes modification au matériel"));
                }
                else
                {
                    // je commence la transaction
                    _dbContext.BeginTransaction();
                    MaterielModif.LastModif = DateTime.Now;
                    // j'ajoute le matériel en récupérant l'id du nouveau matériel
                    var upDateMat = await _dbContext.Materiel.UpdateAsync(MaterielModif);

                    // j'ajoute les nouvelles catégories
                    foreach (var idCat in idCatToAdd)
                    {
                        var upDtaMatCat = new MaterielCategorie()
                        {
                            IdCategorie = idCat,
                            IdMateriel = upDateMat.Id,
                        };
                        var idMatCat = await _dbContext.MaterielCategorie.AddAsync(upDtaMatCat);
                        //listIdMatCat.Add(idMatCat);
                    }
                    // je supprime les catégorie non appliquées au matériel
                    foreach (var idCat in idCatToDelete)
                    {
                        var resultDelete = await _dbContext.MaterielCategorie.DeleteAsync(idCat, upDateMat.Id);
                    }
                    // je ferme la transaction
                    _dbContext.Commit();

                    // je récupère les catégorie du nouveau matériel - si récupération de la liste listIdMatCat, inutile
                    List<MaterielCategorie> listMatCat = (await _dbContext.MaterielCategorie.GetAllAsync(upDateMat.Id)).ToList();

                    // je fait la liste des catégorie en fonction de listMatCat
                    List<Categorie> listCat = new();
                    foreach (var matCat in listMatCat)
                    {
                        var cat = await _dbContext.Categorie.GetByIdCatAsync(matCat.IdCategorie);
                        listCat.Add(cat);
                    }

                    // je crée le résultat
                    GestionMaterielResponseDto result = await FuncGetAllGestionMateriels();
                    result.IdMat = upDateMat.Id;
                    result.CatOfIdMat = listCat;
                    return result;
                }
            }
        }
    }

    public async Task<GestionMaterielResponseDto> PutGestionMaterielsArchiveAsync(GestionMatrielRequestDto gestionMatrielRequestDto)
    {

        // je transforme le Dto de requête en Matériel et liste de catégorie
        Materiel MaterielModif = new()
        {
            Id = gestionMatrielRequestDto?.OldIdMat ?? -1,
            Nom = gestionMatrielRequestDto.NewNomMat,
            DateMiseEnService = gestionMatrielRequestDto.NewDMService,
            DateFinGarantie = gestionMatrielRequestDto.NewDFGarantie,
            IdUser = gestionMatrielRequestDto.NewIdUser,
            IdMContrat = gestionMatrielRequestDto.NewIdCMaintenance,
            Archive = gestionMatrielRequestDto.NewArchive,
            LastModif = gestionMatrielRequestDto.LastModif,
        };

        // je récupère la date de la dernière modification
        var MaterielDatabase = await _dbContext.Materiel.GetByIdAsync(MaterielModif.Id);
        //var lastModifInDatabase = (await _dbContext.Materiel.GetByIdAsync(MaterielModif.Id)).LastModif;

        // je vérifi qu'entre ma demande et maintenant personne n'a modifié ce même matériel
        if (MaterielModif.LastModif < MaterielDatabase.LastModif)
        {
            //return new GestionMaterielResponseDto(new SysException($"Le matériel dont l'ID est {MaterielModif.Id} fût modifié avant votre action."));
            return new GestionMaterielResponseDto()
            {
                IdMat = MaterielModif.Id,
                ErrorMessage = $"Le matériel dont l'ID est {MaterielModif.Id} fût modifié avant votre action.",
            };
        }
        else if (MaterielDatabase.Archive)
        {
            return new GestionMaterielResponseDto()
            {
                ErrorMessage = $"Le matériel dont l'ID est {MaterielModif.Id} fût archivé avant votre action.",
            };
        }
        else
        {
            // je force la valeur à updater
            MaterielModif.Archive = true;
            // j'ajoute le matériel en récupérant l'id du nouveau matériel
            var upDateMat = await _dbContext.Materiel.UpdateArchiveAsync(MaterielModif);

            // je crée le résultat
            GestionMaterielResponseDto result = await FuncGetAllGestionMateriels();
            result.IdMat = upDateMat.Id;
            return result;

        }

    }

    #endregion


    #region Delete ------------------------------------------------------
    public async Task<GestionMaterielResponseDto> DeleteGestionMaterielsAsync(long idMat)
    {
        //_dbContext.Rollback();

        var MaterielDatabase = await _dbContext.Materiel.GetByIdAsync(idMat);
        if (idMat < 0)
        {
            return new GestionMaterielResponseDto()
            {
                IdMat = idMat,
                ErrorMessage = $"L'identifiant matériel ne peut pas être négatif.",
            };
        }
        // je vérifi qu'entre ma demande et maintenant personne n'a modifié ce même matériel
        else if (!MaterielDatabase.Archive)
        {
            //return new GestionMaterielResponseDto(new SysException($"Le matériel dont l'ID est {MaterielModif.Id} fût modifié avant votre action."));
            return new GestionMaterielResponseDto()
            {
                IdMat = idMat,
                ErrorMessage = $"Le matériel dont l'identifiant est {idMat}, doit être archivé avant d'être détruit.",
            };
        }

        else
        {
            List<long> listOldIdCat = (await _dbContext.MaterielCategorie.GetAllAsync(idMat)).Select(x => x.IdCategorie).ToList();

            var newMatDetruit = new MaterielDetruit
            {
                OldNumMarquage = MaterielDatabase.Id,
                DateDestruction = DateTime.Now,
                DateMiseEnService = MaterielDatabase.DateMiseEnService,
                IdMContrat = MaterielDatabase?.IdMContrat ?? null,
                ListCategories = string.Join(",", listOldIdCat),
                Nom = MaterielDatabase.Nom,
            };


            _dbContext.BeginTransaction();
            // je supprime le matériel de la table D_MATERIEL_CATEGORIE
            // je supprime les catégories appliquées au matériel
            foreach (var idCat in listOldIdCat)
            {
                var resultDelete = await _dbContext.MaterielCategorie.DeleteAsync(idCat, MaterielDatabase.Id);
            }

            // je supprime le matériel de la table D_MATERIEL
            var matDeleted = await _dbContext.Materiel.Delete(idMat);
            // j'ajoute le matériel à supprimer dans la table D_MATERIEL_DETRUIT
            var matAddDeleted = await _dbContext.MaterielDetruit.AddAsync(newMatDetruit);

            _dbContext.Commit();

            // je fait la liste des catégorie en fonction de la liste
            List<Categorie> listCat = new();
            foreach (var idCat in listOldIdCat)
            {
                var cat = await _dbContext.Categorie.GetByIdCatAsync(idCat);
                listCat.Add(cat);
            }

            // je crée le résultat
            GestionMaterielResponseDto result = await FuncGetAllGestionMateriels();
            result.IdMat = idMat;
            result.CatOfIdMat = listCat;
            return result;

        }

    }
    public async Task<MaterielDetruit> GetMatDeletedAsync(long idMat)
    {
        var matDeleted = await _dbContext.MaterielDetruit.GetByIdAsync(idMat);
        return matDeleted;
    }

    public async Task<bool> DeleteMatDeletedGestionMaterielsAsync(long idmatdeleted)
    {
        var nbmatDeleted = await _dbContext.MaterielDetruit.DeleteAsync(idmatdeleted);
        return nbmatDeleted > 0 ? true : false;
    }

    #endregion
}
