using Google.Cloud.Firestore;
using Newtonsoft.Json;
using WebAdmin.Shared.Commons.Resources;
using WebAdmin.Shared.Models;

namespace WebAdmin.DataAccess.DomainServices
{
    public class ProdutoDataAccess : AccessBase
    {

        private static string NomeTabela { get { return "Produtos"; } }
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
        public static async Task AddProduto(ProdutoModel produto)
        {
            if (produto.Imagem != null && produto.Imagem.Count() > 0)
            {
                foreach (var imagem in produto.Imagem)
                {
                    var rotaPasta = StorageDataAccess.MontaRotaPasta(NomeTabela, "", "Imagem");
                    StorageDataAccess.AddFolder(rotaPasta);
                    rotaPasta = StorageDataAccess.MontaRotaPasta(NomeTabela, imagem.Nome, "Imagem");
                    imagem.Url = StorageDataAccess.SalvaArquivoStorage(imagem, rotaPasta);
                }
            }
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
        public static async Task UpdateProduto(ProdutoModel produto)
        {

            if (produto.Imagem != null && produto.Imagem.Count() > 0)
            {
                foreach (var imagem in produto.Imagem)
                {
                    if (imagem.Conteudo != null)
                    {

                        var rotaPasta = StorageDataAccess.MontaRotaPasta(NomeTabela, imagem.Nome, "Imagem");
                        imagem.Url = StorageDataAccess.SalvaArquivoStorage(imagem, rotaPasta);
                    }
                }
            }

            try
            {
                DocumentReference docRef = FireStore.AcessoBaseFireStore().Collection(NomeTabela).Document(produto.Id);
                await docRef.SetAsync(produto, SetOptions.Overwrite);
            }
            catch (Exception ex)
            {
                Exception exception = new Exception(message: GlobalResources.Err_Atualizar_Produto + ex.Message);
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
        public static async Task DeleteProduto(string id)
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

    }
}


