using WebAdmin.DataAccess.DomainServices;
using WebAdmin.Service.Interfaces;
using WebAdmin.Shared.Models;

namespace WebAdmin.Service.Services
{
    public class SectionService : ISectionService
    {
        SectionDataAccess _dataAccess = new SectionDataAccess();

        public Task<List<SectionModel>> GetAllSections()
        {
            return _dataAccess.GetAllSections();
        }

        public void UpdateSection(SectionModel section)
        {
            _dataAccess.UpdateSection(section);
        }

        public Task<SectionModel> GetSection(string id)
        {
            return _dataAccess.GetSection(id);
        }

        public void DeleteSection(string id)
        {
            _dataAccess.DeleteSection(id);
        }

        public void AddSection(SectionModel section)
        {
            _dataAccess.AddSection(section);
        }
    }
}
