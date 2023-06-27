using Google.Cloud.Firestore;
using Newtonsoft.Json;
using WebAdmin.Shared.Models;

namespace WebAdmin.DataAccess.DomainServices
{
    public class ProdutoDataAccess
    {
        Access fireStore = new Access("genesis-93f18");

        public async Task<List<ProdutoModel>> GetAllProdutos()
        {

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
                    using (var stream = new MemoryStream(imagem.Conteudo))
                    {

                        var retorno = fireStore.AcessoBaseStorage().UploadObject("genesis-93f18.appspot.com", imagem.Nome, imagem.Tipo, stream);

                        var storageObject = fireStore.AcessoBaseStorage().GetObject("genesis-93f18.appspot.com", imagem.Nome);

                        string[] sp = storageObject.Id.Split('/');

                        //remove a ultima posição do split
                        Array.Resize(ref sp, sp.Length - 1);

                        string url = "https://storage.googleapis.com/" + string.Join("/", sp);

                        if (url != null)
                        {
                            imagem.Url = url;
                        }

                        stream.Close();
                    }
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
                    {
                        using (var stream = new MemoryStream(imagem.Conteudo))
                        {

                            var retorno = fireStore.AcessoBaseStorage().UploadObject("genesis-93f18.appspot.com", imagem.Nome, imagem.Tipo, stream);

                            var storageObject = fireStore.AcessoBaseStorage().GetObject("genesis-93f18.appspot.com", imagem.Nome);

                            string[] sp = storageObject.Id.Split('/');

                            //remove a ultima posição do split
                            Array.Resize(ref sp, sp.Length - 1);

                            string url = "https://storage.googleapis.com/" + string.Join("/", sp);

                            if (url != null)
                            {
                                imagem.Url = url;
                            }

                            stream.Close();
                        }
                    }
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
    }
}


public class teste
{
    public string Nome { get; set; }
}