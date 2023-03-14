using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class TypeIndividuService : ITypeIndividuService
    {
        private readonly IDapperService _dapperService;

        public TypeIndividuService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<string> Create(TypeIndividuModel typeIndividu)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pidTypeIndividu", typeIndividu.idTypeIndividu, DbType.String, ParameterDirection.InputOutput, 50);
            dbPara.Add("pLibelleTypeIndividu", typeIndividu.LibelleTypeIndividu, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Type_Individu_Add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pidTypeIndividu");
            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE b_type_individu SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE idTypeIndividu = '{ReturnValue}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<TypeIndividuModel> GetById(string id)
        {
            var typeIndividu = Task.FromResult(_dapperService.Get<TypeIndividuModel>($"SELECT * FROM b_type_individu WHERE idTypeIndividu = '{id}'", null, commandType: CommandType.Text));
            return typeIndividu;
        }
        public Task<TypeIndividuModel> GetByLibelle(string id)
        {
            var typeIndividu = Task.FromResult(_dapperService.Get<TypeIndividuModel>($"SELECT * FROM b_type_individu WHERE LibelleTypeIndividu = '{id}'", null, commandType: CommandType.Text));
            return typeIndividu;
        }

        public Task<int> Delete(string id)
        {
            var deletetypeIndividu = Task.FromResult(_dapperService.Execute($"DELETE FROM b_type_individu WHERE idTypeIndividu = '{id}'", null, commandType: CommandType.Text));
            return deletetypeIndividu;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var CounttypeIndividu = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM b_type_individu WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return CounttypeIndividu;
        }

        public Task<List<TypeIndividuModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var typeIndividu = Task.FromResult(_dapperService.GetAll<TypeIndividuModel>($"SELECT idTypeIndividu,LibelleTypeIndividu FROM b_type_individu WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return typeIndividu;
        }

        public Task<int> Update(TypeIndividuModel typeIndividu)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pidTypeIndividu", typeIndividu.idTypeIndividu);
            dbPara.Add("pLibelleTypeIndividu", typeIndividu.LibelleTypeIndividu, DbType.String);
            var updatetypeIndividu = Task.FromResult(_dapperService.Update<int>("Type_Individu_Update", dbPara, commandType: CommandType.StoredProcedure));
            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE b_type_individu SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE idTypeIndividu = '{typeIndividu.idTypeIndividu}'", null, commandType: CommandType.Text));

            return updatetypeIndividu;
        }
    }
}
