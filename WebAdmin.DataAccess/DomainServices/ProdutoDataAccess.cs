using Google.Cloud.Firestore;
using Newtonsoft.Json;
using WebAdmin.Shared.Models;

namespace WebAdmin.DataAccess.DomainServices
{
    public class ProdutoDataAccess
    {
        Access fireStore = new Access("genesis-93f18");
        private string Buket = "genesis-93f18.appspot.com";


        public async Task<List<ProdutoModel>> GetProdutosSelect(List<string> listaSeqs)
        {
            List<ProdutoModel> lstProduto = new List<ProdutoModel>();
            // Consultar os produtos no Firestore
            Query query = fireStore.AcessoBaseFireStore().Collection("produto")
                .WhereIn(FieldPath.DocumentId, listaSeqs);

            QuerySnapshot querySnapshot = query.GetSnapshotAsync().Result;


            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
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
            return lstProduto;
        }

        public async Task<List<ProdutoModel>> GetAllProdutos()
        {


            //var produtos =  fireStore.AcessoBaseFireStore().Collection("produto").WhereIn(FieldPath.DocumentId, lista);
            try
            {
                Query produtoQuery = fireStore.AcessoBaseFireStore().Collection("produto");
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
            if (produto.Imagem.Count() > 0)
            {
                foreach (var imagem in produto.Imagem)
                {
                    imagem.Url = SalvaArquivoStorage(imagem, Buket);
                }
            }

            try
            {

                CollectionReference colRef = fireStore.AcessoBaseFireStore().Collection("produto");
                await colRef.AddAsync(produto);
            }
            catch
            {
                throw;
            }
        }
        public async void UpdateProduto(ProdutoModel produto)
        {

            if (produto.Imagem.Count() > 0)
            {
                foreach (var imagem in produto.Imagem)
                {
                    if (imagem.Conteudo != null)
                        imagem.Url = SalvaArquivoStorage(imagem, Buket);
                }
            }

            try
            {
                DocumentReference docRef = fireStore.AcessoBaseFireStore().Collection("produto").Document(produto.Id);
                await docRef.SetAsync(produto, SetOptions.Overwrite);
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

                DocumentReference docRef = fireStore.AcessoBaseFireStore().Collection("produto").Document(id);
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
                DocumentReference produtoRef = fireStore.AcessoBaseFireStore().Collection("produto").Document(id);
                await produtoRef.DeleteAsync();
            }
            catch
            {
                throw;
            }
        }
        public string SalvaArquivoStorage(ArquivoModel arquivo, string buket)
        {
            try
            {
                string url = "";
                using (var stream = new MemoryStream(arquivo.Conteudo))
                {
                    var retorno = fireStore.AcessoBaseStorage().UploadObject(buket, arquivo.Nome, arquivo.Tipo, stream);

                    string[] sp = retorno.Id.Split('/');

                    //remove a ultima posição do split
                    Array.Resize(ref sp, sp.Length - 1);

                    url = "https://storage.googleapis.com/" + string.Join("/", sp);

                    stream.Close();
                }
                return url;
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um erro ao salvar o arquivo no storage do fire base, código do erro:" + ex.Message);
            }
        }
    }
}


