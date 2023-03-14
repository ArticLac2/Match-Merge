using MatchMerge.Interfaces;
using MatchMerge.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class IndividuViewService : IIndividuViewService
    {
        private readonly IDapperService _dapperService;

        public IndividuViewService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<IndividuViewModel> GetById(string id)
        {
            var individu = Task.FromResult(_dapperService.Get<IndividuViewModel>($"SELECT * FROM View_IndividuFinal WHERE IdIndividu = '{id}'", null, commandType: CommandType.Text));
            return individu;
        }
        public Task<IndividuViewModel> GetBySpecialite(string id)
        {
            var individu = Task.FromResult(_dapperService.Get<IndividuViewModel>($"select LibelleSpecialite,COUNT(LibelleSpecialite) as NombreIndividuUnivers from View_IndividuFinal WHERE LibelleSpecialite ='{id}' group by LibelleSpecialite", null, commandType: CommandType.Text));
            return individu;
        }
        public Task<List<IndividuViewModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var individu = Task.FromResult(_dapperService.GetAll<IndividuViewModel>($"SELECT * FROM View_IndividuFinal WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return individu;
        }



        public Task<List<IndividuViewModel>> GetUpdateBy()
        {
            var individu = Task.FromResult(_dapperService.GetAll<IndividuViewModel>($"SELECT  distinct([UpdatedBy]),[NomUtilisateur] FROM[CannyVey].[dbo].[View_IndividuFinal] where  UpdatedBy != 'null' and UpdatedBy != '4' and UpdatedBy!=''", null, commandType: CommandType.Text));
            return individu;
        }



        public Task<List<IndividuViewModel>> ListAllWithCible(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var individu = Task.FromResult(_dapperService.GetAll<IndividuViewModel>($" Select Max(a_cible.IdCycle) AS IdCycle,a_cible.IdActivite, Max(a_cible.IdEquipe) AS IdEquipe, Max(a_cible.idCanal) AS idCanal, Max(a_cible.IdSegment) AS IdSegment,Max(a_cible.IdDecoupage) AS IdDecoupage, Max(a_cible.IdTerritoire) AS IdTerritoire,  Max(a_cible.IdVisiteur) AS IdVisiteur,Max(a_cible.IdSecteur) AS IdSecteur, Max(a_cible.IdSpecialite) AS IdSpecialite,Max(View_IndividuFinal.IdIndividu) AS IdIndividu,Max(View_IndividuFinal.IdEtablissement) AS IdEtablissement,Max(View_IndividuFinal.NomIndividu) AS NomIndividu,Max(View_IndividuFinal.LibelleEtablissement) AS LibelleEtablissement,Max(View_IndividuFinal.LibelleTerritoire) AS LibelleTerritoire,Max(b_segment.LibelleSegment) AS LibelleSegment,Max(a_cible.Frequence) AS Frequence,count(distinct(a_plan.IdPlan)) as planCount, count(distinct(a_contact_activite.IdContact)) as contactCount, cast(count(distinct(a_plan.IdPlan)) *100 / Max(a_cible.Frequence) as float) as agendaProgress, cast(count(distinct(a_contact_activite.IdContact)) * 100 / Max(a_cible.Frequence) as float) as contactProgress from a_cible left join View_IndividuFinal on a_cible.IdActivite = View_IndividuFinal.IdActivite AND View_IndividuFinal.IdDecoupage = a_cible.IdDecoupage AND View_IndividuFinal.IdTerritoire = a_cible.IdTerritoire AND View_IndividuFinal.IdSpecialite = a_cible.IdSpecialite left join b_segment on b_segment.idSegment = a_cible.IdSegment left join a_plan on a_plan.IdEquipe = a_cible.IdEquipe AND a_plan.IdCycle = a_cible.IdCycle AND a_plan.IdCanal = a_cible.IdCanal AND a_plan.IdActivite = a_cible.IdActivite AND a_plan.IdVisiteur = a_cible.IdVisiteur left join a_contact on a_contact.IdEquipe = a_cible.IdEquipe  AND a_contact.IdCycle = a_cible.IdCycle AND a_contact.IdCanal = a_cible.IdCanal AND a_contact.IdVisiteur = a_cible.IdVisiteur AND a_contact.IdSecteur = a_cible.IdSecteur left join a_contact_activite on a_contact.IdContact = a_contact_activite.IdContact AND a_contact_activite.IdActivite = a_cible.IdActivite AND a_contact_activite.IdDecoupage = a_cible.IdDecoupage AND a_contact_activite.IdTerritoire = a_cible.IdTerritoire AND a_contact_activite.IdSpecialite = a_cible.IdSpecialite AND a_contact_activite.IdSegment = a_cible.IdSegment WHERE 1 =1 {condition}  GROUP BY a_cible.IdActivite ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return individu;
        }

        public Task<List<IndividuViewModel>> listAllGraphIndividuGlobalDepartment(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var individu = Task.FromResult(_dapperService.GetAll<IndividuViewModel>($"SELECT IdDepartement,MAX(LibelleDepartement) AS LibelleDepartement,COUNT(*) AS nombreIndividusParDepartement FROM View_IndividuFinal WHERE 1 = 1 {condition} GROUP BY IdDepartement ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return individu;
        }

        public Task<List<IndividuViewModel>> listAllGraphIndividuGlobalSpecialite(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var individu = Task.FromResult(_dapperService.GetAll<IndividuViewModel>($"SELECT TOP(20) IdSpecialite,MAX(LibelleSpecialite) AS LibelleSpecialite,COUNT(*) AS nombreIndividusParSpecialite FROM View_IndividuFinal WHERE 1 = 1 {condition} GROUP BY IdSpecialite ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return individu;
        }

        public Task<List<IndividuViewModel>> listAllGraphIndividuGlobalTypeIndividu(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var individu = Task.FromResult(_dapperService.GetAll<IndividuViewModel>($"SELECT IdTypeIndividu,MAX(LibelleTypeIndividu) AS LibelleTypeIndividu,COUNT(*) AS nombreIndividusParTypeIndividu FROM View_IndividuFinal WHERE 1 = 1 {condition} GROUP BY IdTypeIndividu ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return individu;
        }

        public Task<List<IndividuViewModel>> listAllGraphIndividuGlobalCivilite(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var individu = Task.FromResult(_dapperService.GetAll<IndividuViewModel>($"SELECT Civilite,COUNT(*) AS nombreIndividusParCivilite FROM View_IndividuFinal WHERE 1 = 1 {condition} GROUP BY Civilite ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return individu;
        }

        public Task<List<IndividuViewModel>> listAllGraphIndividuGlobalCategorieEtablissement(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var individu = Task.FromResult(_dapperService.GetAll<IndividuViewModel>($"SELECT LibelleCategorieEtablissement,cast(COUNT(*) as float) /100 AS nombreIndividusParCategorieEtablissement FROM View_IndividuFinal WHERE 1 = 1 {condition} GROUP BY LibelleCategorieEtablissement ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return individu;
        }

        public Task<List<IndividuViewModel>> listAllGraphIndividuGlobalTypeEtablissement(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var individu = Task.FromResult(_dapperService.GetAll<IndividuViewModel>($"SELECT LibelleTypeEtablissement,COUNT(*) AS nombreIndividusParTypeEtablissement FROM View_IndividuFinal WHERE 1 = 1 {condition}  GROUP BY LibelleTypeEtablissement ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return individu;
        }

    }
}
