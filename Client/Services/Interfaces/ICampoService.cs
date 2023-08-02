using WebAdmin.Shared.Models;

namespace WebAdmin.Client.Services.Interfaces
{
    public interface ICampoService
    {
        void CreateListCampo(List<CampoModel> campos);
        void DeleteCampo(CampoModel campo);
        Task<List<CampoModel>> GetCampos();
        void InsertCampo(CampoModel campo);
    }
}
