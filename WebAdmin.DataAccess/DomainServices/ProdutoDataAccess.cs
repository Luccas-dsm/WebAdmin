using Google.Cloud.Firestore;
using Newtonsoft.Json;
using WebAdmin.Shared.Models;

namespace WebAdmin.DataAccess.DomainServices
{
    public class ProdutoDataAccess
    {
        private static Access FireStore { get { return new Access("genesis-93f18"); }  }
        private static string NomeTabela { get { return "Produtos"; }  }
        private static string Buket { get { return "genesis-93f18.appspot.com"; }  }

        public static async Task<List<ProdutoModel>> GetProdutosSelect(List<string> listaSeqs)
        {
            List<ProdutoModel> lstProduto = new List<ProdutoModel>();
            // Consultar os produtos no Firestore
            Query query = FireStore.AcessoBaseFireStore().Collection(NomeTabela)
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

        public static async Task<List<ProdutoModel>> GetAllProdutos()
        {

           
            try
            {
                Query produtoQuery = FireStore.AcessoBaseFireStore().Collection(NomeTabela);
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
            catch (Exception ex)
            {
                Exception exception = new Exception(message: "Erro ao buscar os produtos." + ex.Message);
                throw exception;
            }
        }
        public static async void AddProduto(ProdutoModel produto)
        {
            if (produto.Imagem != null && produto.Imagem.Count() > 0)
            {
                foreach (var imagem in produto.Imagem)
                {
                    imagem.Url = SalvaArquivoStorage(imagem, Buket);
                }
            }
(using UnitofWork)
            try
            {

                CollectionReference colRef = FireStore.AcessoBaseFireStore().Collection(NomeTabela);
                await colRef.AddAsync(produto);
            }
            catch (Exception ex)
            {
                Exception exception = new Exception(message: "Erro ao buscar adicionar." + ex.Message);
                throw exception;
            }
        }
        public static async void UpdateProduto(ProdutoModel produto)
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
                DocumentReference docRef = FireStore.AcessoBaseFireStore().Collection(NomeTabela).Document(produto.Id);
                await docRef.SetAsync(produto, SetOptions.Overwrite);
            }
            catch (Exception ex)
            {
                Exception exception = new Exception(message: "Erro ao atualizar o produtos." + ex.Message);
                throw exception;
            }
        }
        public static async Task<ProdutoModel> GetProduto(string id)
        {
            try
            {

                DocumentReference docRef = FireStore.AcessoBaseFireStore().Collection(NomeTabela).Document(id);
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
            catch (Exception ex)
            {
                Exception exception = new Exception(message: "Erro ao buscar o produto." + ex.Message);
                throw exception;
            }
        }
        public static async void DeleteProduto(string id)
        {
            try
            {
                DocumentReference produtoRef = FireStore.AcessoBaseFireStore().Collection(NomeTabela).Document(id);
                await produtoRef.DeleteAsync();
            }
            catch (Exception ex)
            {
                Exception exception = new Exception(message: "Erro ao deletar os produto." + ex.Message);
                throw exception;
            }
        }
        public static string SalvaArquivoStorage(ArquivoModel arquivo, string buket)
        {
            try
            {
                string url = "";
                using (var stream = new MemoryStream(arquivo.Conteudo))
                {
                    var retorno = FireStore.AcessoBaseStorage().UploadObject(buket, arquivo.Nome, arquivo.Tipo, stream);

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
                Exception exception = new Exception(message: "Houve um erro ao salvar o arquivo no storage do fire base, código do erro:" + ex.Message);
                throw exception;
            }
        }
    }
}


