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

        public Task UpdateProduto(ProdutoModel produto)
        {
            return ProdutoDataAccess.UpdateProduto(produto);
        }

        public Task<ProdutoModel> GetProduto(string id)
        {
            return ProdutoDataAccess.GetProduto(id);
        }

        public Task DeleteProduto(string id)
        {
           return ProdutoDataAccess.DeleteProduto(id);
        }

        public Task AddProduto(ProdutoModel produto)
        {
           return ProdutoDataAccess.AddProduto(produto);
        }

   
    }
}
