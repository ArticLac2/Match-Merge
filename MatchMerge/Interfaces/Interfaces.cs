using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MatchMerge.Models;

namespace MatchMerge.Interfaces
{

    public interface IDapperService : IDisposable
    {

        DbConnection GetConnection();

        T Get<T>(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure);

        List<T> GetAll<T>(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure);

        int Execute(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure);

        T Insert<T>(string sp, ref DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure);

        T Update<T>(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure);
    }

    public interface IIndividuViewService
    {
        Task<IndividuViewModel> GetById(string id);

        Task<List<IndividuViewModel>> ListAll(string condition, string orderBy, string direction);
        Task<List<IndividuViewModel>> GetUpdateBy();

        //Task<List<IndividuViewModel>> ListAllGroupBySpeciality(string condition, string orderBy, string direction);
        Task<IndividuViewModel> GetBySpecialite(string id);
        Task<List<IndividuViewModel>> ListAllWithCible(string condition, string orderBy, string direction);

        Task<List<IndividuViewModel>> listAllGraphIndividuGlobalDepartment(string condition, string orderBy,
            string direction);

        Task<List<IndividuViewModel>> listAllGraphIndividuGlobalSpecialite(string condition, string orderBy,
            string direction);

        Task<List<IndividuViewModel>> listAllGraphIndividuGlobalTypeIndividu(string condition, string orderBy,
            string direction);

        Task<List<IndividuViewModel>> listAllGraphIndividuGlobalCivilite(string condition, string orderBy,
            string direction);

        Task<List<IndividuViewModel>> listAllGraphIndividuGlobalCategorieEtablissement(string condition, string orderBy,
            string direction);

        Task<List<IndividuViewModel>> listAllGraphIndividuGlobalTypeEtablissement(string condition, string orderBy,
            string direction);
    }


    public interface IIndividuService
    {
        Task<string> Create(IndividuModel individu);
        Task<int> Delete(string idIndividu);
        Task<int> Count(string condition);
        Task<int> Update(IndividuModel individu);
        Task<IndividuModel> GetById(string idIndividu);
        Task<IndividuModel> GetByName(string nomIndividu);
        Task<List<IndividuModel>> ListAll(string condition, string orderBy, string direction);
        Task<List<IndividuModel>> InnerJoinList(string condition, string orderBy, string direction);
        Task<List<IndividuModel>> InnerJoinListFiltered(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }
    public interface IActiviteService
    {
        Task<string> Create(ActiviteModel activite);
        Task<int> Delete(string idActivite);
        Task<int> Count(string condition);
        Task<int> Update(ActiviteModel activite);
        Task<ActiviteModel> GetById(string idActivite);
        Task<ActiviteModel> GetById1(string idActivite, string idIndividu);
        Task<List<ActiviteModel>> GetByIdIndividu(string idIndividu);
        Task<ActiviteModel> GetById2(string idIndividu, string idEtablissement);
        Task<List<ActiviteModel>> ListAll(string condition, string orderBy, string direction);
        Task<List<ActiviteModel>> InnerJoinList(string condition, string orderBy, string direction);
        Task<List<ActiviteModel>> InnerJoinList1(string condition, string orderBy, string direction);
        Task<List<ActiviteModel>> InnerJoinListProfiling(string condition, string orderBy, string direction);
        Task<List<ActiviteModel>> InnerJoinListProfilingForIndividu(string condition, string orderBy, string direction);

        Task<List<ActiviteModel>> InnerJoinListProfilingForEtablissement(string condition, string orderBy,
            string direction);

        Task<List<ActiviteModel>> InnerJoinListProfilingForEtablissement_1(string condition, string orderBy,
            string direction);

        Task<List<ActiviteModel>> InnerJoinListProfilingForActivite(string condition, string orderBy, string direction);

        Task<List<ActiviteModel>> InnerJoinindividu_etablissement_specialité(string condition, string orderBy,
            string direction);

        Task<List<ActiviteModel>> InnerJoinindividu_etablissement_specialité_Decoupage(string condition, string orderBy,
            string direction);

        Task<List<ActiviteModel>> InnerJoinindividu_etablissement_specialité_Decoupage_Territory(string condition,
            string orderBy, string direction);

        public void GetUserId(string id);
    }
    public interface IEtablissementService
    {
        Task<string> Create(EtablissementModel etablissement);
        Task<int> Delete(string idEtablissement);
        Task<int> Count(string condition);
        Task<int> Update(EtablissementModel etablissement);
        Task<EtablissementModel> GetById(string idEtablissement);
        Task<EtablissementModel> GetByIlibelle(string LibelleEtablissement);
        Task<List<EtablissementModel>> ListAll(string condition, string orderBy, string direction);
        Task<List<EtablissementModel>> ListAllJoinSpeciality(string condition, string orderBy, string direction);
        Task<List<EtablissementModel>> ListAllJoinDecoupage(string condition, string orderBy, string direction);

        Task<List<EtablissementModel>>
            ListAllJoinDecoupageTerritory(string condition, string orderBy, string direction);

        public void GetUserId(string id);
    }
    public interface ITypeIndividuService
    {
        Task<string> Create(TypeIndividuModel typeIndividu);
        Task<int> Delete(string IdTypeIndividu);
        Task<int> Count(string condition);
        Task<int> Update(TypeIndividuModel typeIndividu);
        Task<TypeIndividuModel> GetById(string IdTypeIndividu);
        Task<TypeIndividuModel> GetByLibelle(string IdTypeIndividu);
        Task<List<TypeIndividuModel>> ListAll(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }


    //public interface ITitreService
    //{
    //    Task<string> Create(TitreModel titre);
    //    Task<int> Delete(string idTitre);
    //    Task<int> Count(string condition);
    //    Task<int> Update(TitreModel titre);
    //    Task<TitreModel> GetById(string idTitre);
    //    Task<List<TitreModel>> ListAll(string condition, string orderBy, string direction);
    //    public void GetUserId(string id);
    //}
    public interface ICategorieEtablissementService
    {
        Task<string> Create(CategorieEtablissementModel categorieEtablissement);
        Task<int> Delete(string idCategorieEtablissement);
        Task<int> Count(string condition);
        Task<int> Update(CategorieEtablissementModel idCategorieEtablissement);
        Task<CategorieEtablissementModel> GetById(string idEtablissement);
        Task<CategorieEtablissementModel> GetByLibelle(string idEtablissement);
        Task<List<CategorieEtablissementModel>> ListAll(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }
    public interface ITypeEtablissementService
    {
        Task<string> Create(TypeEtablissementModel typeEtablissement);
        Task<int> Delete(string idTypeEtablissement);
        Task<int> Count(string condition);
        Task<int> Update(TypeEtablissementModel typeEtablissement);
        Task<TypeEtablissementModel> GetById(string idTypeEtablissement);
        Task<TypeEtablissementModel> GetByLibelle(string idTypeEtablissement);
        Task<List<TypeEtablissementModel>> ListAll(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }
    public interface IDepartementService
    {
        Task<string> Create(DepartementModel departement);
        Task<int> Delete(string idDepartement);
        Task<int> Count(string condition);
        Task<int> Update(DepartementModel departement);
        Task<DepartementModel> GetById(string idDepartement);
        Task<DepartementModel> GetByLibelle(string idDepartement);
        Task<List<DepartementModel>> ListAll(string condition, string orderBy, string direction);
        Task<List<DepartementModel>> InnerJoinList(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }
    public interface ISpecialiteService
    {
        Task<string> Create(SpecialiteModel specialite);
        Task<int> Delete(string IdSpecialite);
        Task<int> Count(string condition);
        Task<int> Update(SpecialiteModel specialite);
        Task<SpecialiteModel> GetById(string IdSpecialite);
        Task<SpecialiteModel> GetByLibelle(string IdSpecialite);
        Task<List<SpecialiteModel>> ListAll(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }
    public interface IPaysService
    {
        Task<string> Create(PaysModel pays);
        Task<int> Delete(string idPays);
        Task<int> Count(string condition);
        Task<int> Update(PaysModel pays);
        Task<PaysModel> GetById(string idPays);
        Task<List<PaysModel>> ListAll(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }
    public interface IUniversService
    {
        Task<string> Create(UniverModel univers);
        Task<int> Delete(string idUnivers);
        Task<int> Count(string condition);
        Task<int> Update(UniverModel univers);
        Task<UniverModel> GetById(string idUnivers);
        Task<List<UniverModel>> ListAll(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }

    public interface IDecoupageService
    {
        Task<string> Create(DecoupageModel decoupage);
        Task<int> Delete(string idDecoupage);
        Task<int> Count(string condition);
        Task<int> Update(DecoupageModel decoupage);
        Task<DecoupageModel> GetById(string idDecoupage);
        Task<DecoupageModel> GetById1(string idDecoupage, string idPays);
        Task<DecoupageModel> GetByName(string idDecoupage);
        Task<List<DecoupageModel>> ListAll(string condition, string orderBy, string direction);
        Task<List<DecoupageModel>> InnerJoinList(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }
    public interface IDecoupageDetailService
    {
        Task<string> Create(DecoupageDetailModel decoupageDetail);
        Task<int> Delete(string idDecoupage, string IdTerritoire);
        Task<int> Count(string condition);
        Task<int> Update(DecoupageDetailModel decoupageDetail);
        Task<DecoupageDetailModel> GetById(string idDecoupage);
        Task<DecoupageDetailModel> GetByTerritoryId(string idDecoupage);
        Task<DecoupageDetailModel> GetById1(string idDecoupage, string IdTerritoire);
        Task<DecoupageDetailModel> GetByName(string LibelleTerritoire, string IdDecoupage);
        Task<List<DecoupageDetailModel>> ListAll(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }
    public interface IUtilisateurService
    {
        Task<UtilisateurModel> GetById(string idUtilisateur);
        Task<List<UtilisateurModel>> ListAll(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }
    public interface IEtablissementTerritoireService
    {
        Task<string> Create(EtablissementTerritoireModel decoupageDetail);
        Task<int> Delete(string IdEtablissement, string IdDecoupage);
        Task<int> Update(EtablissementTerritoireModel decoupageDetail);
        Task<List<EtablissementTerritoireModel>> ListAll(string condition, string orderBy, string direction);
        Task<List<EtablissementTerritoireModel>> ListAllEtablissementTerritoireView(string condition, string orderBy, string direction);
        public void GetUserId(string id);
    }
}

