using MatchMerge.Interfaces;
using MatchMerge.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MatchMerge.Data
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IDapperService _dapperService;

        public UtilisateurService(IDapperService dapperService)
        {
            this._dapperService = dapperService;
        }

        public string IdUtilisateur = "";

        public void GetUserId(string id)
        {
            IdUtilisateur = id;
        }
     

        public Task<UtilisateurModel> GetById(string id)
        {
            var visiteur = Task.FromResult(_dapperService.Get<UtilisateurModel>($"SELECT * FROM b_utilisateur WHERE IdUtilisateur = '{id}'", null, commandType: CommandType.Text));
            return visiteur;
        }

        public Task<List<UtilisateurModel>> ListAll(string condition = "", string orderBy = "1", string direction = "DESC")
        {
            if (condition != "") condition = " AND (" + condition + ")";
            var visiteur = Task.FromResult(_dapperService.GetAll<UtilisateurModel>($"SELECT * FROM b_utilisateur WHERE 1=1 {condition} ORDER BY {orderBy} {direction}  ", null, commandType: CommandType.Text));
            return visiteur;
        }
     
      
    }
}
