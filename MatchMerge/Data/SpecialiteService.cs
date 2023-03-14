using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class SpecialiteService : ISpecialiteService
    {
        private readonly IDapperService _dapperService;

        public SpecialiteService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<string> Create(SpecialiteModel specialites)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdSpecialite", specialites.IdSpecialite, DbType.String, ParameterDirection.InputOutput, 50);
            dbPara.Add("pLibelleSpecialite", specialites.LibelleSpecialite, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Specialite_add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pIdSpecialite");
            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE b_specialite SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE IdSpecialite = '{ReturnValue}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<SpecialiteModel> GetById(string id)
        {
            var specialite = Task.FromResult(_dapperService.Get<SpecialiteModel>($"SELECT * FROM b_specialite WHERE IdSpecialite = '{id}'", null, commandType: CommandType.Text));
            return specialite;
        }
        public Task<SpecialiteModel> GetByLibelle(string id)
        {
            var specialite = Task.FromResult(_dapperService.Get<SpecialiteModel>($"SELECT * FROM b_specialite WHERE LibelleSpecialite = '{id}'", null, commandType: CommandType.Text));
            return specialite;
        }

        public Task<int> Delete(string id)
        {
            var deleteSpecialite = Task.FromResult(_dapperService.Execute($"DELETE FROM b_specialite WHERE IdSpecialite = '{id}'", null, commandType: CommandType.Text));
            return deleteSpecialite;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var CountSpecialite = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM b_specialite WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return CountSpecialite;
        }

        public Task<List<SpecialiteModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var specialites = Task.FromResult(_dapperService.GetAll<SpecialiteModel>($"SELECT IdSpecialite,LibelleSpecialite FROM b_specialite WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return specialites;
        }

        public Task<int> Update(SpecialiteModel specialite)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdSpecialite", specialite.IdSpecialite);
            dbPara.Add("pLibelleSpecialite", specialite.LibelleSpecialite, DbType.String);
            var updateSpecialite = Task.FromResult(_dapperService.Update<int>("Specialite_Update", dbPara, commandType: CommandType.StoredProcedure));
            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE b_specialite SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE IdSpecialite = '{specialite.IdSpecialite}'", null, commandType: CommandType.Text));

            return updateSpecialite;
        }
    }
}
