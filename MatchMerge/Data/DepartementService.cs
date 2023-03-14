using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class DepartementService : IDepartementService
    {
        private readonly IDapperService _dapperService;

        public DepartementService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<string> Create(DepartementModel departement)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdDepartement", departement.IdDepartement, DbType.String, ParameterDirection.InputOutput, 50);
            dbPara.Add("pIdPays", departement.IdPays, DbType.String, ParameterDirection.Input);
            dbPara.Add("pLibelleDepartement", departement.LibelleDepartement, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Departement_Add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pIdDepartement");
            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE b_departement SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE IdDepartement = '{ReturnValue}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<DepartementModel> GetById(string id)
        {
            var decoupage = Task.FromResult(_dapperService.Get<DepartementModel>($"SELECT * FROM b_departement WHERE IdDepartement = '{id}'", null, commandType: CommandType.Text));
            return decoupage;
        }
        public Task<DepartementModel> GetByLibelle(string id)
        {
            var decoupage = Task.FromResult(_dapperService.Get<DepartementModel>($"SELECT * FROM b_departement WHERE LibelleDepartement = '{id}'", null, commandType: CommandType.Text));
            return decoupage;
        }
        public Task<DepartementModel> GetById1(string id1, string id2)
        {
            var decoupage = Task.FromResult(_dapperService.Get<DepartementModel>($"SELECT * FROM b_departement WHERE IdDepartement = '{id1}' and idPays = '{id2}'", null, commandType: CommandType.Text));
            return decoupage;
        }

        public Task<int> Delete(string id)
        {
            var deletedecoupage = Task.FromResult(_dapperService.Execute($"DELETE FROM b_departement WHERE IdDepartement = '{id}'", null, commandType: CommandType.Text));
            return deletedecoupage;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var Countdepartement = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM b_departement WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return Countdepartement;
        }

        public Task<List<DepartementModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var departement = Task.FromResult(_dapperService.GetAll<DepartementModel>($"SELECT IdDepartement,IdPays,LibelleDepartement FROM b_departement WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return departement;
        }

        public Task<int> Update(DepartementModel departement)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdDepartement", departement.IdDepartement);
            dbPara.Add("pIdPays", departement.IdPays, DbType.String);
            dbPara.Add("pLibelleDepartement", departement.LibelleDepartement, DbType.String);
            var updateDepartement = Task.FromResult(_dapperService.Update<int>("Departement_Update", dbPara, commandType: CommandType.StoredProcedure));
            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE b_departement SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE IdDepartement = '{departement.IdDepartement}'", null, commandType: CommandType.Text));

            return updateDepartement;
        }

        public Task<List<DepartementModel>> InnerJoinList(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var decoupage = Task.FromResult(_dapperService.GetAll<DepartementModel>($"SELECT b_departement.IdPays,b_departement.IdDepartement,b_departement.LibelleDepartement,b_pays.LibellePays from b_departement INNER JOIN b_pays ON b_departement.IdPays = b_pays.idPays WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return decoupage;
        }


    }
}
