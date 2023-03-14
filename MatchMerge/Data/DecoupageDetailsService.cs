using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class DecoupageDetailService : IDecoupageDetailService
    {
        private readonly IDapperService _dapperService;

        public DecoupageDetailService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }

        public Task<string> Create(DecoupageDetailModel decoupage)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdDecoupage", decoupage.IdDecoupage, DbType.String, ParameterDirection.Input);
            dbPara.Add("pZone", decoupage.Zone, DbType.String, ParameterDirection.Input);
            dbPara.Add("pDepartement", decoupage.Departement, DbType.String, ParameterDirection.Input);
            dbPara.Add("pSecteur", decoupage.Secteur, DbType.String, ParameterDirection.Input);
            dbPara.Add("pIdTerritoire", decoupage.IdTerritoire, DbType.String, ParameterDirection.InputOutput, 50);
            dbPara.Add("pDescriptionTerritoire", decoupage.DescriptionTerritoire, DbType.String, ParameterDirection.Input);
            dbPara.Add("pLibelleTerritoire", decoupage.LibelleTerritoire, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Decoupage_Detail_Add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pIdDecoupage");
            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE b_decoupage_detail SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE IdTerritoire = '{ReturnValue}' and  IdDecoupage = '{decoupage.IdDecoupage}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<DecoupageDetailModel> GetById(string id)
        {
            var decoupage = Task.FromResult(_dapperService.Get<DecoupageDetailModel>($"SELECT * FROM b_decoupage_detail WHERE IdDecoupage = '{id}'", null, commandType: CommandType.Text));
            return decoupage;
        }
        public Task<DecoupageDetailModel> GetByTerritoryId(string id)
        {
            var decoupage = Task.FromResult(_dapperService.Get<DecoupageDetailModel>($"SELECT * FROM b_decoupage_detail WHERE IdTerritoire = '{id}'", null, commandType: CommandType.Text));
            return decoupage;
        }

        public Task<DecoupageDetailModel> GetById1(string id1, string id2)
        {
            var decoupage = Task.FromResult(_dapperService.Get<DecoupageDetailModel>($"SELECT * FROM b_decoupage_detail WHERE IdDecoupage = '{id1}' and IdTerritoire='{id2}'", null, commandType: CommandType.Text));
            return decoupage;
        }

        public Task<DecoupageDetailModel> GetByName(string LibelleTerritoire, string IdDecoupage)
        {
            var decoupage = Task.FromResult(_dapperService.Get<DecoupageDetailModel>($"SELECT * FROM b_decoupage_detail WHERE LibelleTerritoire = '{LibelleTerritoire}' AND IdDecoupage = '{IdDecoupage}'", null, commandType: CommandType.Text));
            return decoupage;
        }
        public Task<int> Delete(string id1, string id2)
        {
            var deletedecoupage = Task.FromResult(_dapperService.Execute($"DELETE FROM b_decoupage_detail WHERE IdDecoupage = '{id1}' and IdTerritoire='{id2}' ", null, commandType: CommandType.Text));
            return deletedecoupage;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var Countdecoupage = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM b_decoupage_detail WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return Countdecoupage;
        }

        public Task<List<DecoupageDetailModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var decoupage = Task.FromResult(_dapperService.GetAll<DecoupageDetailModel>($" SELECT CONCAT(LibelleTerritoire,'      ',DescriptionTerritoire) as Details,IdDecoupage,Zone,Departement,Secteur,IdTerritoire,DescriptionTerritoire,LibelleTerritoire FROM b_decoupage_detail WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return decoupage;
        }

        public Task<int> Update(DecoupageDetailModel decoupage)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdDecoupage", decoupage.IdDecoupage);
            dbPara.Add("pZone", decoupage.Zone, DbType.String);
            dbPara.Add("pDepartement", decoupage.Departement, DbType.String);
            dbPara.Add("pSecteur", decoupage.Secteur, DbType.String);
            dbPara.Add("pIdTerritoire", decoupage.IdTerritoire, DbType.String);
            dbPara.Add("pDescriptionTerritoire", decoupage.DescriptionTerritoire, DbType.String);
            dbPara.Add("pLibelleTerritoire", decoupage.LibelleTerritoire, DbType.String);
            var updatedecoupage = Task.FromResult(_dapperService.Update<int>("Decoupage_Detail_Update", dbPara, commandType: CommandType.StoredProcedure));
            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE b_decoupage_detail SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE IdDecoupage = '{decoupage.IdDecoupage}' and IdTerritoire = '{decoupage.IdTerritoire}'", null, commandType: CommandType.Text));

            return updatedecoupage;
        }
    }
}
