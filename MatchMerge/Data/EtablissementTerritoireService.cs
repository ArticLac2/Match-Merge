using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MatchMerge.Data.Universe.Activities
{
    public class EtablissementTerritoireService : IEtablissementTerritoireService
    {
        private readonly IDapperService _dapperService;

        public EtablissementTerritoireService(IDapperService dapperService)
        {
            _dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<string> Create(EtablissementTerritoireModel etablissementTerritoire)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdEtablissement", etablissementTerritoire.IdEtablissement, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdDecoupage", etablissementTerritoire.IdDecoupage, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdTerritoire", etablissementTerritoire.IdTerritoire, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("EtablissementTerritoire_Add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pIdEtablissement");
            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE u_etablissement_territoire SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE IdEtablissement = '{etablissementTerritoire.IdEtablissement}' and IdDecoupage = '{etablissementTerritoire.IdDecoupage}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<int> Delete(string IdEtablissement,string IdDecoupage)
        {
            var etablissementTerritoire = Task.FromResult(_dapperService.Execute($"DELETE FROM u_etablissement_territoire WHERE IdEtablissement = '{IdEtablissement}' AND IdDecoupage = '{IdDecoupage}'", null,CommandType.Text));
            return etablissementTerritoire;
        }

        public Task<List<EtablissementTerritoireModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var etablissementTerritoire = Task.FromResult(_dapperService.GetAll<EtablissementTerritoireModel>($"SELECT IdEtablissement,IdDecoupage,IdTerritoire FROM u_etablissement_territoire WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return etablissementTerritoire;
        }

        public Task<List<EtablissementTerritoireModel>> ListAllEtablissementTerritoireView(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var etablissementTerritoire = Task.FromResult(_dapperService.GetAll<EtablissementTerritoireModel>($"SELECT IdEtablissement, LibelleEtablissement, IdDecoupage, LibelleDecoupage, IdTerritoire, LibelleTerritoire,DescriptionTerritoire FROM View_Etablissement_Territoire WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return etablissementTerritoire;
        }

        public Task<int> Update(EtablissementTerritoireModel etablissementTerritoire)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdEtablissement", etablissementTerritoire.IdEtablissement);
            dbPara.Add("pIdDecoupage", etablissementTerritoire.IdDecoupage, DbType.String);
            dbPara.Add("pIdTerritoire", etablissementTerritoire.IdTerritoire, DbType.String);
            var updateetablissementTerritoire = Task.FromResult(_dapperService.Update<int>("EtablissementTerritoire_Update", dbPara, commandType: CommandType.StoredProcedure));
            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE u_etablissement_territoire SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE IdEtablissement = '{etablissementTerritoire.IdEtablissement}' and IdDecoupage = '{etablissementTerritoire.IdDecoupage}'", null, commandType: CommandType.Text));

            return updateetablissementTerritoire;
        }


    }
}

