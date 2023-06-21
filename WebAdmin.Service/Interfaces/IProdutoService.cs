using WebAdmin.Shared.Models;

namespace WebAdmin.Service.Interfaces
{
    public interface IProdutoService
    {
        Task<List<ProdutoModel>> GetAllProdutos();
        void AddProduto(ProdutoModel produto);
        void UpdateProduto(ProdutoModel produto);
        Task<ProdutoModel> GetProduto(string id);
        void DeleteProduto(string id);
    }
}
