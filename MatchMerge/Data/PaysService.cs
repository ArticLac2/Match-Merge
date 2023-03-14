using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class PaysService : IPaysService
    {
        private readonly IDapperService _dapperService;

        public PaysService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<string> Create(PaysModel pays)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pidPays", pays.idPays, DbType.String, ParameterDirection.InputOutput, 50);
            dbPara.Add("pLibellePays", pays.LibellePays, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Pays_add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pidPays");
            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE b_pays SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE idPays = '{ReturnValue}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<PaysModel> GetById(string id)
        {
            var pays = Task.FromResult(_dapperService.Get<PaysModel>($"SELECT * FROM b_pays WHERE idPays = '{id}'", null, commandType: CommandType.Text));
            return pays;
        }

        public Task<int> Delete(string id)
        {
            var deletepays = Task.FromResult(_dapperService.Execute($"DELETE FROM b_pays WHERE idPays = '{id}'", null, commandType: CommandType.Text));
            return deletepays;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var Countpays = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM b_pays WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return Countpays;
        }

        public Task<List<PaysModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var pays = Task.FromResult(_dapperService.GetAll<PaysModel>($"SELECT idPays,LibellePays FROM b_pays WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return pays;
        }

        public Task<int> Update(PaysModel pays)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pidPays", pays.idPays);
            dbPara.Add("pLibellePays", pays.LibellePays, DbType.String);
            var updatepays = Task.FromResult(_dapperService.Update<int>("Pays_Update", dbPara, commandType: CommandType.StoredProcedure));
            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE b_pays SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE idPays = '{pays.idPays}'", null, commandType: CommandType.Text));

            return updatepays;
        }


    }
}
