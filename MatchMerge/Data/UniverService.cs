using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class UniverService : IUniversService
    {
        private readonly IDapperService _dapperService;


        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }

        public UniverService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }



        public Task<string> Create(UniverModel univers)
        {


            var dbPara = new DynamicParameters();
            dbPara.Add("pIdUnivers", univers.IdUnivers, DbType.String, ParameterDirection.InputOutput, 50);
            dbPara.Add("pLibelleUnivers", univers.LibelleUnivers, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Univers_add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pIdUnivers");

            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE u_univers SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE IdUnivers = '{ReturnValue}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<UniverModel> GetById(string id)
        {
            var univers = Task.FromResult(_dapperService.Get<UniverModel>($"SELECT * FROM u_univers WHERE IdUnivers = '{id}'", null, commandType: CommandType.Text));
            return univers;
        }

        public Task<int> Delete(string id)
        {
            var deleteunivers = Task.FromResult(_dapperService.Execute($"DELETE FROM u_univers WHERE IdUnivers = '{id}'", null, commandType: CommandType.Text));
            return deleteunivers;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var Countunivers = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM u_univers WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return Countunivers;
        }

        public Task<List<UniverModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var univers = Task.FromResult(_dapperService.GetAll<UniverModel>($"SELECT IdUnivers,LibelleUnivers FROM u_univers WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return univers;
        }

        public Task<int> Update(UniverModel univers)
        {



            var dbPara = new DynamicParameters();
            dbPara.Add("pIdUnivers", univers.IdUnivers);
            dbPara.Add("pLibelleUnivers", univers.LibelleUnivers, DbType.String);
            var updateunivers = Task.FromResult(_dapperService.Update<int>("Univers_Update", dbPara, commandType: CommandType.StoredProcedure));

            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE u_univers SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE IdUnivers = '{univers.IdUnivers}'", null, commandType: CommandType.Text));

            return updateunivers;
        }


    }
}
