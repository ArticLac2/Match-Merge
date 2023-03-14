using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class EtablissementService : IEtablissementService
    {
        private readonly IDapperService _dapperService;
        public string IdUtilisateur = "";

        public EtablissementService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<string> Create(EtablissementModel etablissement)
        {
            var userId = IdUtilisateur;
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdEtablissement", etablissement.IdEtablissement, DbType.String, ParameterDirection.InputOutput, 50);
            dbPara.Add("pIdUnivers", etablissement.IdUnivers, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdPays", etablissement.IdPays, DbType.String, ParameterDirection.Input);
            dbPara.Add("pCodeExterneEtablissement", etablissement.CodeExterneEtablissement, DbType.String, ParameterDirection.Input);
            dbPara.Add("pLibelleEtablissement", etablissement.LibelleEtablissement, DbType.String, ParameterDirection.Input);
            dbPara.Add("pAdresse", etablissement.Adresse, DbType.String, ParameterDirection.Input);
            dbPara.Add("pCodePostal", etablissement.CodePostal, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdCategorieEtablissement", etablissement.IdCategorieEtablissement, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdTypeEtablissement", etablissement.IdTypeEtablissement, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdSpecialite", etablissement.IdSpecialite, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdEtablissmentParent", etablissement.IdEtablissmentParent, DbType.String, ParameterDirection.Input);
            dbPara.Add("pTelephoneFixe", etablissement.TelephoneFixe, DbType.String, ParameterDirection.Input);
            dbPara.Add("pTelephoneMobile", etablissement.TelephoneMobile, DbType.String, ParameterDirection.Input);
            dbPara.Add("pTelephoneFixe1", etablissement.TelephoneFixe1, DbType.String, ParameterDirection.Input);
            dbPara.Add("pTelephoneMobile1", etablissement.TelephoneMobile1, DbType.String, ParameterDirection.Input);
            dbPara.Add("pEmail", etablissement.Email, DbType.String, ParameterDirection.Input);
            dbPara.Add("pFaceBook", etablissement.FaceBook, DbType.String, ParameterDirection.Input);
            dbPara.Add("pSiteWeb", etablissement.SiteWeb, DbType.String, ParameterDirection.Input);
            dbPara.Add("pLinkedIn", etablissement.LinkedIn, DbType.String, ParameterDirection.Input);
            dbPara.Add("pInstagram", etablissement.Instagram, DbType.String, ParameterDirection.Input);
            dbPara.Add("pTweeter", etablissement.Tweeter, DbType.String, ParameterDirection.Input);
            dbPara.Add("pLongitude", etablissement.Longitude, DbType.String, ParameterDirection.Input);
            dbPara.Add("platitude", etablissement.latitude, DbType.String, ParameterDirection.Input);
            dbPara.Add("pActive", etablissement.Active, DbType.Boolean, ParameterDirection.Input);
            dbPara.Add("pIdDepartement", etablissement.IdDepartement, DbType.String, ParameterDirection.Input);
            dbPara.Add("pVille", etablissement.Ville, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Etablissement_Add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pIdEtablissement");

            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE u_etablissement SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE IdEtablissement = '{ReturnValue}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<EtablissementModel> GetById(string id)
        {
            var etablissement = Task.FromResult(_dapperService.Get<EtablissementModel>($"SELECT * FROM u_etablissement WHERE IdEtablissement = '{id}'", null, commandType: CommandType.Text));
            return etablissement;
        }

        public Task<EtablissementModel> GetByIlibelle(string LibelleEtablissement)
        {
            var etablissement = Task.FromResult(_dapperService.Get<EtablissementModel>($"SELECT * FROM u_etablissement WHERE LibelleEtablissement = '{LibelleEtablissement}'", null, commandType: CommandType.Text));
            return etablissement;
        }

        public Task<int> Delete(string id)
        {
            var deleteetablissement = Task.FromResult(_dapperService.Execute($"DELETE FROM u_etablissement WHERE IdEtablissement = '{id}'", null, commandType: CommandType.Text));
            return deleteetablissement;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var Countetablissement = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM u_etablissement WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return Countetablissement;
        }

        public Task<List<EtablissementModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var etablissement = Task.FromResult(_dapperService.GetAll<EtablissementModel>($"SELECT IdEtablissement,NomUtilisateur,IdUnivers,IdPays,CodeExterneEtablissement,LibelleEtablissement,Adresse,CodePostal,IdCategorieEtablissement,LibelleCategorieEtablissement,IdTypeEtablissement,LibelleTypeEtablissement,IdSpecialite,LibelleSpecialite,IdEtablissmentParent,TelephoneFixe,TelephoneFixe1,TelephoneMobile,TelephoneMobile1,ListeDesTelephones,Email,FaceBook,SiteWeb,LinkedIn,Instagram,Tweeter,Longitude,Latitude,Active,IdDepartement,ville,AdresseComplete,DateMiseAJour FROM View_Etablissement WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return etablissement;
        }

        public Task<List<EtablissementModel>> ListAllJoinSpeciality(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var etablissement = Task.FromResult(_dapperService.GetAll<EtablissementModel>($"SELECT * from u_etablissement_territoire Left join u_etablissement on(u_etablissement.IdEtablissement = u_etablissement_territoire.IdEtablissement)  left join b_specialite on (u_etablissement.IdSpecialite = b_specialite.IdSpecialite) WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return etablissement;
        }

        public Task<List<EtablissementModel>> ListAllJoinDecoupage(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var etablissement = Task.FromResult(_dapperService.GetAll<EtablissementModel>($"SELECT * from u_etablissement_territoire Left join u_etablissement on(u_etablissement.IdEtablissement = u_etablissement_territoire.IdEtablissement) left join b_decoupage on(u_etablissement_territoire.IdDecoupage = b_decoupage.IdDecoupage) left join b_specialite on (u_etablissement.IdSpecialite = b_specialite.IdSpecialite) left join b_decoupage_detail on u_etablissement_territoire.IdDecoupage= b_decoupage_detail.IdDecoupage and u_etablissement_territoire.IdTerritoire = b_decoupage_detail.IdTerritoire WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return etablissement;
        }

        public Task<List<EtablissementModel>> ListAllJoinDecoupageTerritory(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var etablissement = Task.FromResult(_dapperService.GetAll<EtablissementModel>($"select * from  u_etablissement  left join b_specialite on u_etablissement.IdSpecialite = b_specialite.IdSpecialite left join u_etablissement_territoire on u_etablissement.IdEtablissement = u_etablissement_territoire.IdEtablissement left join b_decoupage on b_decoupage.IdDecoupage = u_etablissement_territoire.IdDecoupage left join b_decoupage_detail on b_decoupage.IdDecoupage = b_decoupage_detail.IdDecoupage and u_etablissement_territoire.IdTerritoire = b_decoupage_detail.IdTerritoire WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return etablissement;
        }


        public Task<int> Update(EtablissementModel etablissement)
        {
            var userId = IdUtilisateur;
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdEtablissement", etablissement.IdEtablissement);
            dbPara.Add("pIdUnivers", etablissement.IdUnivers, DbType.String);
            dbPara.Add("pIdPays", etablissement.IdPays, DbType.String);
            dbPara.Add("pCodeExterneEtablissement", etablissement.CodeExterneEtablissement, DbType.String);
            dbPara.Add("pLibelleEtablissement", etablissement.LibelleEtablissement, DbType.String);
            dbPara.Add("pAdresse", etablissement.Adresse, DbType.String);
            dbPara.Add("pCodePostal", etablissement.CodePostal, DbType.String);
            dbPara.Add("pIdCategorieEtablissement", etablissement.IdCategorieEtablissement, DbType.String);
            dbPara.Add("pIdTypeEtablissement", etablissement.IdTypeEtablissement, DbType.String);
            dbPara.Add("pIdSpecialite", etablissement.IdSpecialite, DbType.String);
            dbPara.Add("pIdEtablissmentParent", etablissement.IdEtablissmentParent, DbType.String);
            dbPara.Add("pTelephoneFixe", etablissement.TelephoneFixe, DbType.String);
            dbPara.Add("pTelephoneMobile", etablissement.TelephoneMobile, DbType.String);
            dbPara.Add("pTelephoneFixe1", etablissement.TelephoneFixe1, DbType.String);
            dbPara.Add("pTelephoneMobile1", etablissement.TelephoneMobile1, DbType.String);
            dbPara.Add("pEmail", etablissement.Email, DbType.String);
            dbPara.Add("pFaceBook", etablissement.FaceBook, DbType.String);
            dbPara.Add("pSiteWeb", etablissement.SiteWeb, DbType.String);
            dbPara.Add("pLinkedIn", etablissement.LinkedIn, DbType.String);
            dbPara.Add("pInstagram", etablissement.Instagram, DbType.String);
            dbPara.Add("pTweeter", etablissement.Tweeter, DbType.String);
            dbPara.Add("pLongitude", etablissement.Longitude, DbType.String);
            dbPara.Add("platitude", etablissement.latitude, DbType.String);
            dbPara.Add("pActive", etablissement.Active, DbType.Boolean);
            dbPara.Add("pIdDepartement", etablissement.IdDepartement, DbType.String);
            dbPara.Add("pVille", etablissement.Ville, DbType.String);
            var updateetablissement = Task.FromResult(_dapperService.Update<int>("Etablissement_Update", dbPara, commandType: CommandType.StoredProcedure));

            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE u_etablissement SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE IdEtablissement = '{etablissement.IdEtablissement}'", null, commandType: CommandType.Text));


            return updateetablissement;
        }
    }
}
