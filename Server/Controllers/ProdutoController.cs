using Microsoft.AspNetCore.Mvc;
using WebAdmin.Service;
using WebAdmin.Service.Interfaces;
using WebAdmin.Service.Services;
using WebAdmin.Shared.Models;

namespace WebAdmin.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController 
    {
        private readonly IProdutoService _produtoService;
        public  ProdutoController()
        {
            _produtoService = IOCService.GetService<IProdutoService>();
        }

        [HttpGet]
        public async Task<List<ProdutoModel>> Get()
        {
            return await _produtoService.GetAllProdutos();
        }

        [HttpGet("{id}")]
        public async Task<ProdutoModel> Get(string id)
        {
            return await _produtoService.GetProduto(id);
        }

        [HttpPost]
        public Task Post([FromBody] ProdutoModel produto)
        {
           return _produtoService.AddProduto(produto);
        }

        [HttpPut]
        public Task Put([FromBody] ProdutoModel produto)
        {
            return _produtoService.UpdateProduto(produto);
        }

        [HttpDelete("{id}")]
        public Task Delete(string id)
        {
           return _produtoService.DeleteProduto(id);
        }
    }
}
