using Microsoft.AspNetCore.Mvc;
using WebAdmin.Service.Interfaces;
using WebAdmin.Service.Services;
using WebAdmin.Shared.Models;

namespace WebAdmin.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController 
    {
        IProdutoService produtoDataAccess = new ProdutoService();

        [HttpGet]
        public async Task<List<ProdutoModel>> Get()
        {
            return await produtoDataAccess.GetAllProdutos();
        }
        [HttpGet("{id}")]
        public async Task<ProdutoModel> Get(string id)
        {
            return await produtoDataAccess.GetProduto(id);
        }
        [HttpPost]
        public void Post([FromBody] ProdutoModel produto)
        {
            produtoDataAccess.AddProduto(produto);
        }
    }
}
