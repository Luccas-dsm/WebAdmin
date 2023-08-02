using WebAdmin.Client.Services.Interfaces;
using WebAdmin.Shared.Models;

namespace WebAdmin.Client.Services.Service
{
    public class CampoService : ICampoService
    {
        private List<CampoModel> Campos { get; set; }

        public void CreateListCampo(List<CampoModel> campos)
        {
            Campos = campos;
        }

        public void InsertCampo(CampoModel campo)
        {
            Campos.Add(campo);
        }

        public void DeleteCampo(CampoModel campo)
        {
            Campos.Remove(campo);
        }
        public async Task<List<CampoModel>> GetCampos()
        {
            return Campos;
        }
    }
}
