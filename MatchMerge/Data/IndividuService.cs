using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class IndividuService : IIndividuService
    {
        private readonly IDapperService _dapperService;

        public IndividuService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<string> Create(IndividuModel individu)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdIndividu", individu.IdIndividu, DbType.String, ParameterDirection.Output, 50);
            dbPara.Add("pIdUnivers", individu.IdUnivers, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdPays", individu.IdPays, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdDepartement", individu.IdDepartement, DbType.String, ParameterDirection.Input);
            dbPara.Add("pCodeExterneIndividu", individu.CodeExterneIndividu, DbType.String, ParameterDirection.Input);
            dbPara.Add("pCivilite", individu.Civilite, DbType.String, ParameterDirection.Input);
            dbPara.Add("pTitre", individu.Titre, DbType.String, ParameterDirection.Input);
            dbPara.Add("pNomIndividu", individu.NomIndividu, DbType.String, ParameterDirection.Input);
            dbPara.Add("pAdresse", individu.Adresse, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdSpecialite", individu.IdSpecialite, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdSpecialiteSecondaire", individu.IdSpecialiteSecondaire, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdTypeIndividu", individu.IdTypeIndividu, DbType.String, ParameterDirection.Input);
            dbPara.Add("pCodePostal", individu.CodePostal, DbType.String, ParameterDirection.Input);
            dbPara.Add("pTelephoneFixe", individu.TelephoneFixe, DbType.String, ParameterDirection.Input);
            dbPara.Add("pTelephoneMobile", individu.TelephoneMobile, DbType.String, ParameterDirection.Input);
            dbPara.Add("pTelephoneFixe1", individu.TelephoneFixe1, DbType.String, ParameterDirection.Input);
            dbPara.Add("pTelephoneMobile1", individu.TelephoneMobile1, DbType.String, ParameterDirection.Input);
            dbPara.Add("pEmail", individu.Email, DbType.String, ParameterDirection.Input);
            dbPara.Add("pFaceBook", individu.FaceBook, DbType.String, ParameterDirection.Input);
            dbPara.Add("pSiteWeb", individu.SiteWeb, DbType.String, ParameterDirection.Input);
            dbPara.Add("pLinkedIn", individu.LinkedIn, DbType.String, ParameterDirection.Input);
            dbPara.Add("pInstagram", individu.Instagram, DbType.String, ParameterDirection.Input);
            dbPara.Add("pTweeter", individu.Tweeter, DbType.String, ParameterDirection.Input);
            dbPara.Add("pLongitude", individu.Longitude, DbType.String, ParameterDirection.Input);
            dbPara.Add("platitude", individu.latitude, DbType.String, ParameterDirection.Input);
            dbPara.Add("pActive", individu.Active, DbType.Boolean, ParameterDirection.Input);
            dbPara.Add("pVille", individu.Ville, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Individu_Add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pIdIndividu");
            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE u_individu SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE IdIndividu = '{ReturnValue}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<IndividuModel> GetById(string id)
        {
            var individu = Task.FromResult(_dapperService.Get<IndividuModel>($"SELECT * FROM u_individu WHERE IdIndividu = '{id}'", null, commandType: CommandType.Text));
            return individu;
        }
        public Task<IndividuModel> GetByName(string NomIndividu)
        {
            var individu = Task.FromResult(_dapperService.Get<IndividuModel>($"SELECT * FROM u_individu WHERE NomIndividu = '{NomIndividu}'", null, commandType: CommandType.Text));
            return individu;
        }

        public Task<int> Delete(string id)
        {
            var deleteindividu = Task.FromResult(_dapperService.Execute($"DELETE FROM u_individu WHERE IdIndividu = '{id}'", null, commandType: CommandType.Text));
            return deleteindividu;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var Countindividu = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM u_individu WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return Countindividu;
        }

        public Task<List<IndividuModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var individu = Task.FromResult(_dapperService.GetAll<IndividuModel>($"SELECT IdIndividu,IdUnivers,IdPays,CodeExterneIndividu,Civilite,Titre,NomIndividu,Adresse,IdSpecialite,IdSpecialiteSecondaire,IdTypeIndividu,CodePostal,TelephoneFixe,TelephoneMobile,TelephoneFixe1,TelephoneMobile1,Email,FaceBook,SiteWeb,LinkedIn,Instagram,Tweeter,Longitude,latitude,Active,IdDepartement,Ville FROM u_individu WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return individu;
        }

        public Task<List<IndividuModel>> InnerJoinList(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            var datemax = "with datemax as(SELECT  max(DateDebutActivite) as topdate, idIndividu from u_activite group by idIndividu)";
            if (condition != "") condition = " AND (" + condition + ")";
            // var individu = Task.FromResult(_dapperService.GetAll<IndividuModel>($"SELECT * FROM u_individu INNER JOIN u_activite ON u_individu.IdIndividu = u_activite.IdIndividu INNER JOIN u_etablissement ON u_activite.IdEtablissement = u_etablissement.IdEtablissement  WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            var individu = Task.FromResult(_dapperService.GetAll<IndividuModel>($" {datemax} SELECT u_individu.IdIndividu, u_individu.NomIndividu, u_individu.Titre, u_etablissement.Adresse, u_individu.TelephoneMobile, u_etablissement.LibelleEtablissement, u_individu.Active FROM u_individu LEFT JOIN u_activite ON u_individu.IdIndividu = u_activite.IdIndividu LEFT JOIN u_etablissement ON u_activite.IdEtablissement = u_etablissement.IdEtablissement inner join datemax on  datemax.IdIndividu = u_individu.IdIndividu and topdate = DateDebutActivite  WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return individu;
        }

        public Task<List<IndividuModel>> InnerJoinListFiltered(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            var datemax = "with datemax as(SELECT  max(DateDebutActivite) as topdate, idIndividu from u_activite group by idIndividu)";
            if (condition != "") condition = " AND (" + condition + ")";
            // var individu = Task.FromResult(_dapperService.GetAll<IndividuModel>($"SELECT * FROM u_individu INNER JOIN u_activite ON u_individu.IdIndividu = u_activite.IdIndividu INNER JOIN u_etablissement ON u_activite.IdEtablissement = u_etablissement.IdEtablissement  WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            var individu = Task.FromResult(_dapperService.GetAll<IndividuModel>($" {datemax} SELECT {condition}  FROM u_individu LEFT JOIN u_activite ON u_individu.IdIndividu = u_activite.IdIndividu LEFT JOIN u_etablissement ON u_activite.IdEtablissement = u_etablissement.IdEtablissement inner join datemax on  datemax.IdIndividu = u_individu.IdIndividu and topdate = DateDebutActivite  WHERE 1=1 ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return individu;
        }

        //public Task<List<IndividuModel>> listAllInnerJoinSpeciality(string condition = "", string orderBy = "1", string direction = "DESC")
        //{
        //    if (condition != "") condition = " AND (" + condition + ")";
        //    var individu = Task.FromResult(_dapperService.GetAll<IndividuModel>($"SELECT * FROM u_individu inner join b_specialite on u_individu.IdSpecialite = b_specialite.IdSpecialite WHERE 1 = 1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
        //    return individu;
        //}

        public Task<int> Update(IndividuModel individu)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdIndividu", individu.IdIndividu);
            dbPara.Add("pIdUnivers", individu.IdUnivers, DbType.String);
            dbPara.Add("pIdPays", individu.IdPays, DbType.String);
            dbPara.Add("pIdDepartement", individu.IdDepartement, DbType.String);
            dbPara.Add("pCodeExterneIndividu", individu.CodeExterneIndividu, DbType.String);
            dbPara.Add("pCivilite", individu.Civilite, DbType.String);
            dbPara.Add("pTitre", individu.Titre, DbType.String);
            dbPara.Add("pNomIndividu", individu.NomIndividu, DbType.String);
            dbPara.Add("pAdresse", individu.Adresse, DbType.String);
            dbPara.Add("pIdSpecialite", individu.IdSpecialite, DbType.String);
            dbPara.Add("pIdSpecialiteSecondaire", individu.IdSpecialiteSecondaire, DbType.String);
            dbPara.Add("pIdTypeIndividu", individu.IdTypeIndividu, DbType.String);
            dbPara.Add("pCodePostal", individu.CodePostal, DbType.String);
            dbPara.Add("pTelephoneFixe", individu.TelephoneFixe, DbType.String);
            dbPara.Add("pTelephoneMobile", individu.TelephoneMobile, DbType.String);
            dbPara.Add("pTelephoneFixe1", individu.TelephoneFixe1, DbType.String);
            dbPara.Add("pTelephoneMobile1", individu.TelephoneMobile1, DbType.String);
            dbPara.Add("pEmail", individu.Email, DbType.String);
            dbPara.Add("pFaceBook", individu.FaceBook, DbType.String);
            dbPara.Add("pSiteWeb", individu.SiteWeb, DbType.String);
            dbPara.Add("pLinkedIn", individu.LinkedIn, DbType.String);
            dbPara.Add("pInstagram", individu.Instagram, DbType.String);
            dbPara.Add("pTweeter", individu.Tweeter, DbType.String);
            dbPara.Add("pLongitude", individu.Longitude, DbType.String);
            dbPara.Add("platitude", individu.latitude, DbType.String);
            dbPara.Add("pActive", individu.Active, DbType.Boolean);
            dbPara.Add("pVille", individu.Ville, DbType.String);
            var updateindividu = Task.FromResult(_dapperService.Update<int>("Individu_Update", dbPara, commandType: CommandType.StoredProcedure));
            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE u_individu SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE IdIndividu = '{individu.IdIndividu}'", null, commandType: CommandType.Text));
            return updateindividu;
        }

    }
}
