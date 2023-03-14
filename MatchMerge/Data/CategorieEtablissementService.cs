using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MatchMerge.Data { 
    public class CategorieEtablissementService : ICategorieEtablissementService
    {
        private readonly IDapperService _dapperService;

        public CategorieEtablissementService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
        public Task<string> Create(CategorieEtablissementModel categories)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdCategorieEtablissement", categories.IdCategorieEtablissement, DbType.String, ParameterDirection.InputOutput, 50);
            dbPara.Add("pLibelleCategorieEtablissement", categories.LibelleCategorieEtablissement, DbType.String, ParameterDirection.Input);
            var Out = Task.FromResult(_dapperService.Insert<int>("Categorie_Etablissement_Add", ref dbPara, commandType: CommandType.StoredProcedure));
            var ReturnValue = dbPara.Get<string>("pIdCategorieEtablissement");
            var userId = IdUtilisateur;
            var majcreate = Task.FromResult(_dapperService.Execute($"UPDATE b_categorie_etablissement SET CreatedBy = '{userId}', UpdatedBy = '{userId}', CreatedAt = GETDATE() , UpdatedAt = GETDATE(), ClientId = (SELECT ClientId from b_baseid)  WHERE IdCategorieEtablissement = '{ReturnValue}'", null, commandType: CommandType.Text));

            return Task.FromResult(ReturnValue);
        }

        public Task<CategorieEtablissementModel> GetById(string id)
        {
            var categories = Task.FromResult(_dapperService.Get<CategorieEtablissementModel>($"SELECT * FROM b_categorie_etablissement WHERE IdCategorieEtablissement = '{id}'", null, commandType: CommandType.Text));
            return categories;
        }
        public Task<CategorieEtablissementModel> GetByLibelle(string id)
        {
            var categories = Task.FromResult(_dapperService.Get<CategorieEtablissementModel>($"SELECT * FROM b_categorie_etablissement WHERE LibelleCategorieEtablissement = '{id}'", null, commandType: CommandType.Text));
            return categories;
        }

        public Task<int> Delete(string id)
        {
            var deletecategories = Task.FromResult(_dapperService.Execute($"DELETE FROM b_categorie_etablissement WHERE IdCategorieEtablissement = '{id}'", null, commandType: CommandType.Text));
            return deletecategories;
        }

        public Task<int> Count(string condition)
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var Countcategories = Task.FromResult(_dapperService.Get<int>($"SELECT COUNT(*) FROM b_categorie_etablissement WHERE 1=1 {condition}", null, commandType: CommandType.Text));
            return Countcategories;
        }

        public Task<List<CategorieEtablissementModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var categories = Task.FromResult(_dapperService.GetAll<CategorieEtablissementModel>($"SELECT IdCategorieEtablissement,LibelleCategorieEtablissement FROM b_categorie_etablissement WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return categories;
        }

        public Task<int> Update(CategorieEtablissementModel categories)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("pIdCategorieEtablissement", categories.IdCategorieEtablissement);
            dbPara.Add("pLibelleCategorieEtablissement", categories.LibelleCategorieEtablissement, DbType.String);
            var updatecategories = Task.FromResult(_dapperService.Update<int>("Categorie_Etablissement_Update", dbPara, commandType: CommandType.StoredProcedure));
            var userId = IdUtilisateur;
            var majupdate = Task.FromResult(_dapperService.Execute($"UPDATE b_categorie_etablissement SET UpdatedBy = '{userId}', UpdatedAt = GETDATE()  WHERE IdCategorieEtablissement = '{categories.IdCategorieEtablissement}'", null, commandType: CommandType.Text));

            return updatecategories;
        }

    }
}
