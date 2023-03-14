using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class ActiviteService : IActiviteService
    {
        private readonly IDapperService _dapperService;

        public ActiviteService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<string> Create(ActiviteModel activites)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdActivite", activites.IdActivite, DbType.String, ParameterDirection.InputOutput, 50);
            dbPara.Add("pIdIndividu", activites.IdIndividu, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdEtablissement", activites.IdEtablissement, DbType.String, ParameterDirection.Input);
            dbPara.Add("pActivitePrincipale", activites.ActivitePrincipale, DbType.Boolean, ParameterDirection.Input);
            dbPara.Add("pDateDebutActivite", activites.DateDebutActivite, DbType.Date, ParameterDirection.Input);
            dbPara.Add("pDateFinActivite", activites.DateFinActivite, DbType.Date, ParameterDirection.Input);
            dbPara.Add("pActive", activites.Active, DbType.Boolean, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Activite_Add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pIdActivite");
            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE u_activite SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE IdActivite = '{ReturnValue}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<ActiviteModel> GetById(string id)
        {
            var activites = Task.FromResult(_dapperService.Get<ActiviteModel>($"SELECT * FROM u_activite LEFT JOIN View_IndividuFinal ON View_IndividuFinal.IdActivite = u_activite.IdActivite  WHERE u_activite.IdActivite = '{id}'", null, commandType: CommandType.Text));
            return activites;
        }
        public Task<ActiviteModel> GetById1(string id1, string id2)
        {
            var activites = Task.FromResult(_dapperService.Get<ActiviteModel>($"SELECT * FROM u_activite WHERE IdActivite = '{id1}' and IdIndividu = '{id2}' ", null, commandType: CommandType.Text));
            return activites;
        }
        public Task<List<ActiviteModel>> GetByIdIndividu(string id2)
        {
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"SELECT * FROM u_activite WHERE IdIndividu = '{id2}' ", null, commandType: CommandType.Text));
            return activites;
        }
        public Task<ActiviteModel> GetById2(string idIndividu, string idEtablissement)
        {
            var activites = Task.FromResult(_dapperService.Get<ActiviteModel>($"SELECT * FROM u_activite WHERE IdIndividu = '{idIndividu}' and IdEtablissement= '{idEtablissement}' ", null, commandType: CommandType.Text));
            return activites;
        }

        public Task<int> Delete(string id)
        {
            var deleteactivites = Task.FromResult(_dapperService.Execute($"DELETE FROM u_activite WHERE IdActivite = '{id}'", null, commandType: CommandType.Text));
            return deleteactivites;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var Countactivites = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM u_activite WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return Countactivites;
        }

        public Task<List<ActiviteModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"SELECT IdActivite,IdIndividu,IdEtablissement,ActivitePrincipale,DateDebutActivite,DateFinActivite,Active FROM u_activite WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return activites;
        }

        public Task<List<ActiviteModel>> InnerJoinList(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"SELECT * FROM u_activite LEFT JOIN View_IndividuFinal ON u_activite.IdActivite = View_IndividuFinal.IdActivite  WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return activites;
        }

        public Task<List<ActiviteModel>> InnerJoinList1(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"select distinct View_IndividuFinal.IdActivite,View_IndividuFinal.IdIndividu,View_IndividuFinal.IdEtablissement,IdSpecialite,AdresseComplete,ListeDesTelephones,LibelleEtablissement,NomIndividu,LibelleSpecialite from View_IndividuFinal left join s_activite_segment   on View_IndividuFinal.IdActivite=s_activite_segment.IdActivite  where 1=1{ condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return activites;
        }

        public Task<List<ActiviteModel>> InnerJoinListProfiling(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"SELECT * FROM p_critere_profil LEFT JOIN p_individu_profil ON p_critere_profil.IdCritere = p_individu_profil.IdCritere LEFT JOIN p_etablissement_profil ON p_critere_profil.IdCritere = p_etablissement_profil.IdCritere LEFT JOIN p_activite_profil ON p_critere_profil.IdCritere = p_activite_profil.IdCritere LEFT JOIN s_activite_segment ON (s_activite_segment.IdActivite = p_activite_profil.IdActivite OR s_activite_segment.IdEtablissement = p_etablissement_profil.IdEtablissement OR s_activite_segment.IdIndividu = p_individu_profil.IdIndividu) LEFT JOIN u_activite ON (u_activite.IdActivite = p_activite_profil.IdActivite OR u_activite.IdEtablissement = p_etablissement_profil.IdEtablissement OR u_activite.IdIndividu = p_individu_profil.IdIndividu) LEFT JOIN u_etablissement ON u_activite.IdEtablissement = u_etablissement.IdEtablissement  LEFT JOIN u_etablissement_territoire ON u_etablissement_territoire.IdEtablissement = u_etablissement.IdEtablissement LEFT JOIN b_decoupage ON u_etablissement_territoire.IdDecoupage = b_decoupage.IdDecoupage LEFT JOIN b_decoupage_detail ON u_etablissement_territoire.IdDecoupage = b_decoupage_detail.IdDecoupage and u_etablissement_territoire.IdTerritoire = b_decoupage_detail.IdTerritoire LEFT JOIN u_individu ON u_individu.IdIndividu = u_activite.IdIndividu LEFT JOIN b_segment ON b_segment.idSegment = s_activite_segment.IdSegment LEFT JOIN o_equipe ON o_equipe.IdEquipe = s_activite_segment.IdEquipe WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return activites;
        }

        public Task<List<ActiviteModel>> InnerJoinListProfilingForIndividu(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"SELECT View_IndividuFinal.IdIndividu ,MAX(View_IndividuFinal.IdEtablissement) AS IdEtablissement,MAX(View_IndividuFinal.IdActivite) AS IdActivite,MAX(View_IndividuFinal.NomIndividu) AS NomIndividu,Max(View_IndividuFinal.LibelleEtablissement) AS LibelleEtablissement,Max(View_IndividuFinal.LibelleTerritoire) AS LibelleTerritoire,	Max(DateDebutActivite) AS DateDebutActivite,MAX(LibelleSegment) AS LibelleSegment,Max(LibellePanel) AS LibellePanel  ,Max(DateSegment) AS DateSegment  FROM View_IndividuFinal JOIN View_SegmentationAssistant_Individu ON View_SegmentationAssistant_Individu.IdIndividu = View_IndividuFinal.IdIndividu AND View_SegmentationAssistant_Individu.IdEtablissement = View_IndividuFinal.IdEtablissement AND View_SegmentationAssistant_Individu.IdActivite = View_IndividuFinal.IdActivite WHERE 1=1 {condition} GROUP BY View_IndividuFinal.IdIndividu ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return activites;
        }

        public Task<List<ActiviteModel>> InnerJoinListProfilingForEtablissement(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"SELECT View_IndividuFinal.IdEtablissement ,Max(View_IndividuFinal.LibelleEtablissement) AS LibelleEtablissement,Max(View_IndividuFinal.LibelleTerritoire) AS LibelleTerritoire,MAX(LibelleSegment) AS LibelleSegment,Max(LibellePanel) AS LibellePanel,Max(DateSegment) AS DateSegment  FROM View_IndividuFinal JOIN View_SegmentationAssistant_Etablissement ON View_SegmentationAssistant_Etablissement.IdIndividu = View_IndividuFinal.IdIndividu AND View_SegmentationAssistant_Etablissement.IdEtablissement = View_IndividuFinal.IdEtablissement AND View_SegmentationAssistant_Etablissement.IdActivite = View_IndividuFinal.IdActivite WHERE 1 =1 {condition} GROUP BY View_IndividuFinal.IdEtablissement ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return activites;
        }

        public Task<List<ActiviteModel>> InnerJoinListProfilingForEtablissement_1(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"SELECT IdEtablissement,Max(LibelleEtablissement) AS LibelleEtablissement,Max(LibelleTerritoire) AS LibelleTerritoire,Max(LibelleSegment) AS LibelleSegment ,Max(LibellePanel) AS LibellePanel,Max(IdCritereEtablissement) AS IdCritereEtablissement,Max(ValeurCritereEtablissement) AS ValeurCritereEtablissement,	Max(DateSegment) AS DateSegment FROM View_SegmentationAssistantEtablissement_1  WHERE 1 =1 {condition}  GROUP BY IdEtablissement ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return activites;
        }

        public Task<List<ActiviteModel>> InnerJoinListProfilingForActivite(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"SELECT View_IndividuFinal.IdIndividu ,MAX(View_IndividuFinal.IdEtablissement) AS IdEtablissement,MAX(View_IndividuFinal.IdActivite) AS IdActivite,MAX(View_IndividuFinal.NomIndividu) AS NomIndividu,Max(View_IndividuFinal.LibelleEtablissement) AS LibelleEtablissement,Max(View_IndividuFinal.LibelleTerritoire) AS LibelleTerritoire,	Max(DateDebutActivite) AS DateDebutActivite,MAX(LibelleSegment) AS LibelleSegment,Max(LibellePanel) AS LibellePanel ,Max(DateSegment) AS DateSegment  FROM View_IndividuFinal JOIN View_SegmentationAssistant_Activite ON View_SegmentationAssistant_Activite.IdIndividu = View_IndividuFinal.IdIndividu AND View_SegmentationAssistant_Activite.IdEtablissement = View_IndividuFinal.IdEtablissement AND View_SegmentationAssistant_Activite.IdActivite = View_IndividuFinal.IdActivite WHERE 1=1 {condition} GROUP BY View_IndividuFinal.IdIndividu ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return activites;
        }

        public Task<List<ActiviteModel>> InnerJoinindividu_etablissement_specialité(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"select * from u_activite left join u_individu on u_activite.IdIndividu = u_individu.IdIndividu left join u_etablissement on u_activite.IdEtablissement = u_etablissement.IdEtablissement left join b_specialite on(u_individu.IdSpecialite = b_specialite.IdSpecialite) WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return activites;
        }
        public Task<List<ActiviteModel>> InnerJoinindividu_etablissement_specialité_Decoupage(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"select * from u_activite left join u_individu on u_activite.IdIndividu = u_individu.IdIndividu left join u_etablissement on u_activite.IdEtablissement = u_etablissement.IdEtablissement left join b_specialite on u_individu.IdSpecialite = b_specialite.IdSpecialite left join u_etablissement_territoire on u_activite.IdEtablissement = u_etablissement_territoire.IdEtablissement left join b_decoupage on u_etablissement_territoire.IdDecoupage = b_decoupage.IdDecoupage left join b_decoupage_detail on u_etablissement_territoire.IdDecoupage= b_decoupage_detail.IdDecoupage and u_etablissement_territoire.IdTerritoire = b_decoupage_detail.IdTerritoire WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return activites;
        }
        public Task<List<ActiviteModel>> InnerJoinindividu_etablissement_specialité_Decoupage_Territory(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var activites = Task.FromResult(_dapperService.GetAll<ActiviteModel>($"select * from u_activite left join u_individu on u_activite.IdIndividu = u_individu.IdIndividu left join u_etablissement on u_activite.IdEtablissement = u_etablissement.IdEtablissement left join b_specialite on u_individu.IdSpecialite = b_specialite.IdSpecialite left join u_etablissement_territoire on u_activite.IdEtablissement = u_etablissement_territoire.IdEtablissement left join b_decoupage on b_decoupage.IdDecoupage = u_etablissement_territoire.IdDecoupage left join b_decoupage_detail on b_decoupage.IdDecoupage = b_decoupage_detail.IdDecoupage and u_etablissement_territoire.IdTerritoire = b_decoupage_detail.IdTerritoire  WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return activites;
        }

        public Task<int> Update(ActiviteModel activites)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdActivite", activites.IdActivite);
            dbPara.Add("pIdIndividu", activites.IdIndividu, DbType.String);
            dbPara.Add("pIdEtablissement", activites.IdEtablissement, DbType.String);
            dbPara.Add("pActivitePrincipale", activites.ActivitePrincipale, DbType.Boolean);
            dbPara.Add("pDateDebutActivite", activites.DateDebutActivite, DbType.Date);
            dbPara.Add("pDateFinActivite", activites.DateFinActivite, DbType.Date);
            dbPara.Add("pActive", activites.Active, DbType.Boolean);
            var updateactivites = Task.FromResult(_dapperService.Update<int>("Activite_Update", dbPara, commandType: CommandType.StoredProcedure));
            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE u_activite SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE IdActivite = '{activites.IdActivite}'", null, commandType: CommandType.Text));

            return updateactivites;
        }


    }
}
