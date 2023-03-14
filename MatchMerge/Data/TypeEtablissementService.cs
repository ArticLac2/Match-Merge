using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class TypeEtablissementService : ITypeEtablissementService
    {
        private readonly IDapperService _dapperService;

        public TypeEtablissementService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<string> Create(TypeEtablissementModel types)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdTypeEtablissement", types.IdTypeEtablissement, DbType.String, ParameterDirection.InputOutput, 50);
            dbPara.Add("pLibelleTypeEtablissement", types.LibelleTypeEtablissement, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Type_Etablissement_Add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pIdTypeEtablissement");
            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE b_type_etablissement SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE IdTypeEtablissement = '{ReturnValue}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<TypeEtablissementModel> GetById(string id)
        {
            var types = Task.FromResult(_dapperService.Get<TypeEtablissementModel>($"SELECT * FROM b_type_etablissement WHERE IdTypeEtablissement = '{id}'", null, commandType: CommandType.Text));
            return types;
        }
        public Task<TypeEtablissementModel> GetByLibelle(string id)
        {
            var types = Task.FromResult(_dapperService.Get<TypeEtablissementModel>($"SELECT * FROM b_type_etablissement WHERE LibelleTypeEtablissement = '{id}'", null, commandType: CommandType.Text));
            return types;
        }

        public Task<int> Delete(string id)
        {
            var deletetypes = Task.FromResult(_dapperService.Execute($"DELETE FROM b_type_etablissement WHERE IdTypeEtablissement = '{id}'", null, commandType: CommandType.Text));
            return deletetypes;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var Counttypes = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM b_type_etablissement WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return Counttypes;
        }

        public Task<List<TypeEtablissementModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var types = Task.FromResult(_dapperService.GetAll<TypeEtablissementModel>($"SELECT IdTypeEtablissement,LibelleTypeEtablissement FROM b_type_etablissement WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return types;
        }

        public Task<int> Update(TypeEtablissementModel types)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdTypeEtablissement", types.IdTypeEtablissement);
            dbPara.Add("pLibelleTypeEtablissement", types.LibelleTypeEtablissement, DbType.String);
            var updatetypes = Task.FromResult(_dapperService.Update<int>("Type_Etablissement_Update", dbPara, commandType: CommandType.StoredProcedure));
            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE b_type_etablissement SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE IdTypeEtablissement = '{types.IdTypeEtablissement}'", null, commandType: CommandType.Text));

            return updatetypes;
        }

    }
}
