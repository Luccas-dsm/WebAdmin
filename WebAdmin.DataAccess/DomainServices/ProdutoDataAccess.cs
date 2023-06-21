using Google.Cloud.Firestore;
using Newtonsoft.Json;
using WebAdmin.Shared.Models;

namespace WebAdmin.DataAccess.DomainServices
{
    public class ProdutoDataAccess
    {
        Access fireStoreDb = new Access("genesis-93f18");

        public async Task<List<ProdutoModel>> GetAllProdutos()
        {

            try
            {
                Query produtoQuery = fireStoreDb.AcessoBaseFireStore().Collection("produtos");
                QuerySnapshot produtoQuerySnapshot = await produtoQuery.GetSnapshotAsync();
                List<ProdutoModel> lstProduto = new List<ProdutoModel>();

                foreach (DocumentSnapshot documentSnapshot in produtoQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> city = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(city);
                        ProdutoModel newuser = JsonConvert.DeserializeObject<ProdutoModel>(json);
                        newuser.Id = documentSnapshot.Id;
                        lstProduto.Add(newuser);
                    }
                }

                List<ProdutoModel> ListaOrdenada = lstProduto.OrderBy(x => x.Nome).ToList();
                return ListaOrdenada;
            }
            catch
            {
                throw;
            }
        }
        public async void AddProduto(ProdutoModel produto)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.AcessoBaseFireStore().Collection("produtos");
                await colRef.AddAsync(produto);
            }
            catch
            {
                throw;
            }
        }
        public async void UpdateProduto(ProdutoModel produto)
        {
            try
            {
                DocumentReference empRef = fireStoreDb.AcessoBaseFireStore().Collection("produtos").Document(produto.Id);
                await empRef.SetAsync(produto, SetOptions.Overwrite);
            }
            catch
            {
                throw;
            }
        }
        public async Task<ProdutoModel> GetProduto(string id)
        {
            try
            {
                DocumentReference docRef = fireStoreDb.AcessoBaseFireStore().Collection("produtos").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    ProdutoModel produto = snapshot.ConvertTo<ProdutoModel>();
                    produto.Id = snapshot.Id;
                    return produto;
                }
                else
                {
                    return new ProdutoModel();
                }
            }
            catch
            {
                throw;
            }
        }
        public async void DeleteProduto(string id)
        {
            try
            {
                DocumentReference produtoRef = fireStoreDb.AcessoBaseFireStore().Collection("produtos").Document(id);
                await produtoRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }        
    }
}
