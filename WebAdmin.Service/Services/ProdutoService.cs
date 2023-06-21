using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAdmin.DataAccess.DomainServices;
using WebAdmin.Service.Interfaces;
using WebAdmin.Shared.Models;

namespace WebAdmin.Service.Services
{
    public class ProdutoService : IProdutoService
    {
        ProdutoDataAccess _dataAccess = new ProdutoDataAccess();


        public Task<List<ProdutoModel>> GetAllProdutos()
        {
            return _dataAccess.GetAllProdutos();
        }

        public void UpdateProduto(ProdutoModel produto)
        {
            _dataAccess.UpdateProduto(produto);
        }

        public Task<ProdutoModel> GetProduto(string id)
        {
            return _dataAccess.GetProduto(id);
        }

        public void DeleteProduto(string id)
        {
            _dataAccess.DeleteProduto(id);
        }

        public void AddProduto(ProdutoModel produto)
        {
            _dataAccess.AddProduto(produto);
        }
    }
}
