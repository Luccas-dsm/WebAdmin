using WebAdmin.DataAccess.DomainServices;
using WebAdmin.Service.Interfaces;
using WebAdmin.Shared.Models;

namespace WebAdmin.Service.Services
{
    public class ProdutoService : IProdutoService
    {

        public Task<List<ProdutoModel>> GetAllProdutos()
        { 
            return ProdutoDataAccess.GetAllProdutos();
        }

        public void UpdateProduto(ProdutoModel produto)
        {
            ProdutoDataAccess.UpdateProduto(produto);
        }

        public Task<ProdutoModel> GetProduto(string id)
        {
            return ProdutoDataAccess.GetProduto(id);
        }

        public void DeleteProduto(string id)
        {
            ProdutoDataAccess.DeleteProduto(id);
        }

        public void AddProduto(ProdutoModel produto)
        {
            ProdutoDataAccess.AddProduto(produto);
        }
    }
}
