using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class DecoupageService : IDecoupageService
    {
        private readonly IDapperService _dapperService;

        public DecoupageService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<string> Create(DecoupageModel decoupage)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdDecoupage", decoupage.IdDecoupage, DbType.String, ParameterDirection.InputOutput, 50);
            dbPara.Add("pLibelleDecoupage", decoupage.LibelleDecoupage, DbType.String, ParameterDirection.Input);
            dbPara.Add("pidPays", decoupage.idPays, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Decoupage_add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pIdDecoupage");
            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE b_decoupage SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE IdDecoupage = '{ReturnValue}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<DecoupageModel> GetById(string id)
        {
            var decoupage = Task.FromResult(_dapperService.Get<DecoupageModel>($"SELECT * FROM b_decoupage WHERE IdDecoupage = '{id}'", null, commandType: CommandType.Text));
            return decoupage;
        }


        public Task<DecoupageModel> GetById1(string id1, string id2)
        {
            var decoupage = Task.FromResult(_dapperService.Get<DecoupageModel>($"SELECT * FROM b_decoupage WHERE IdDecoupage = '{id1}' and idPays = '{id2}'", null, commandType: CommandType.Text));
            return decoupage;
        }

        public Task<DecoupageModel> GetByName(string LibelleDecoupage)
        {
            var decoupage = Task.FromResult(_dapperService.Get<DecoupageModel>($"SELECT * FROM b_decoupage WHERE LibelleDecoupage = '{LibelleDecoupage}'", null, commandType: CommandType.Text));
            return decoupage;
        }

        public Task<int> Delete(string id)
        {
            var deletedecoupage = Task.FromResult(_dapperService.Execute($"DELETE FROM b_decoupage WHERE IdDecoupage = '{id}'", null, commandType: CommandType.Text));
            return deletedecoupage;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var Countdecoupage = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM b_decoupage WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return Countdecoupage;
        }

        public Task<List<DecoupageModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var decoupage = Task.FromResult(_dapperService.GetAll<DecoupageModel>($"SELECT IdDecoupage,LibelleDecoupage,idPays FROM b_decoupage WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return decoupage;
        }

        public Task<int> Update(DecoupageModel decoupage)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdDecoupage", decoupage.IdDecoupage);
            dbPara.Add("pLibelleDecoupage", decoupage.LibelleDecoupage, DbType.String);
            dbPara.Add("pidPays", decoupage.idPays, DbType.String);
            var updatedecoupage = Task.FromResult(_dapperService.Update<int>("Decoupage_Update", dbPara, commandType: CommandType.StoredProcedure));
            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE b_decoupage SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE IdDecoupage = '{decoupage.IdDecoupage}'", null, commandType: CommandType.Text));

            return updatedecoupage;
        }

        public Task<List<DecoupageModel>> InnerJoinList(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var decoupage = Task.FromResult(_dapperService.GetAll<DecoupageModel>($"SELECT b_decoupage.IdDecoupage,b_decoupage.LibelleDecoupage,b_decoupage.idPays,b_pays.LibellePays FROM b_decoupage INNER JOIN b_pays ON b_decoupage.idPays = b_pays.idPays WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return decoupage;
        }

    }
}
