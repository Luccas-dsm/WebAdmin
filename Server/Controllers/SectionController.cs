using Microsoft.AspNetCore.Mvc;
using WebAdmin.Service.Interfaces;
using WebAdmin.Service.Services;
using WebAdmin.Shared.Models;

namespace WebAdmin.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SectionController
    {
        ISectionService sectionDataAccess = new SectionService();

        [HttpGet]
        public async Task<List<SectionModel>> Get()
        {
            return await sectionDataAccess.GetAllSections();
        }
        [HttpGet("{id}")]
        public async Task<SectionModel> Get(string id)
        {
            return await sectionDataAccess.GetSection(id);
        }
        [HttpPost]
        public void Post([FromBody] SectionModel produto)
        {
            sectionDataAccess.AddSection(produto);
        }
        [HttpPut]
        public void Put([FromBody] SectionModel produto)
        {
            sectionDataAccess.UpdateSection(produto);
        }
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            sectionDataAccess.DeleteSection(id);
        }
    }
}
