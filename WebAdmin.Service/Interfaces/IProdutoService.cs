using WebAdmin.Shared.Models;

namespace WebAdmin.Service.Interfaces
{
    public interface IProdutoService
    {
        Task<List<ProdutoModel>> GetAllProdutos();
        Task AddProduto(ProdutoModel produto);
        Task UpdateProduto(ProdutoModel produto);
        Task<ProdutoModel> GetProduto(string id);
        Task DeleteProduto(string id);
    }
}
