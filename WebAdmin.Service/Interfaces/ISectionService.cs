using WebAdmin.Shared.Models;

namespace WebAdmin.Service.Interfaces
{
    public interface ISectionService
    {
        Task<List<SectionModel>> GetAllSections();
        void AddSection(SectionModel section);
        void UpdateSection(SectionModel section);
        Task<SectionModel> GetSection(string id);
        void DeleteSection(string id);
    }
}
