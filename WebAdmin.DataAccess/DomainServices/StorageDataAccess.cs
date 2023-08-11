using Google.Cloud.Storage.V1;
using WebAdmin.Shared.Commons.Resources;
using WebAdmin.Shared.Models;

namespace WebAdmin.DataAccess.DomainServices
{
    public class StorageDataAccess : AccessBase
    {
        /// <summary>
        /// Salva um arquivo no Storage
        /// </summary>
        /// <param name="arquivo"></param>
        /// <returns></returns>
        public static string SalvaArquivoStorage(ArquivoModel arquivo,string rotaPasta)
        {
            try
            {
                string url = "";
                using (var stream = new MemoryStream(arquivo.Conteudo))
                {


                    var retorno = FireStore.AcessoBaseStorage().UploadObject(Buket, rotaPasta, arquivo.Tipo, stream);

                    string[] sp = retorno.Id.Split('/');

                    // remove a ultima posição do split
                    Array.Resize(ref sp, sp.Length - 1);

                    url = "https://storage.googleapis.com/" + string.Join("/", sp);

                    stream.Close();
                }
                return url;
            }
            catch (Exception ex)
            {
                Exception exception = new Exception(message: GlobalResources.Err_SalvarArquivoStorage + ex.Message);
                throw exception;
            }
        }

        /// <summary>
        /// Cria a pasta de acordo com o caminho especificado
        /// </summary>
        /// <param name="rotaPasta"></param>
        /// <returns></returns>
        public static string AddFolder(string rotaPasta)
        {
            try
            {
                FireStore.AcessoBaseStorage().UploadObject(Buket, rotaPasta, null, new MemoryStream(new byte[0]));
                return GlobalResources.MSG_Sucesso_CriarPasta;
            }
            catch (Exception e)
            {
                return GlobalResources.Err_CriarPasta + e.Message;
            }
        }
        /// <summary>
        /// Retorna o conteudo do arquivo
        /// </summary>
        /// <param name="rotaPasta"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static byte[] BuscarArquivo(string rotaPasta)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    FireStore.AcessoBaseStorage().DownloadObject(Buket, rotaPasta, memoryStream);

                    var conteudo = memoryStream.ToArray();

                    return conteudo;
                }
            }
            catch (Exception e)
            {
                throw new Exception(message: e.Message);
            }

        }
        /// <summary>
        /// Lista todos os arquivos e pastas do buket
        /// </summary>
        /// <param name="rotaPasta"></param>
        /// <returns></returns>
        public static List<string> ArquivosEncontrados(string rotaPasta)
        {
            try
            {
                var lista = new List<string>();
                // List objects
                foreach (var obj in FireStore.AcessoBaseStorage().ListObjects(Buket, ""))
                {
                    lista.Add(obj.Name);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception (message: GlobalResources.Err_ListarArquivos + e.Message);
            }
        }

        /// <summary>
        /// Apaga somente o arquivo passado pelo parametro
        /// </summary>
        /// <param name="rotaPasta"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool ApagarArquivo(string rotaPasta)
        {
            try
            {
                FireStore.AcessoBaseStorage().DeleteObject(Buket, rotaPasta);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(message:GlobalResources.Err_DeletarArquivo +  e.Message);
            }

        }

        /// <summary>
        /// Apaga os arquivos em cascata, inclusive apaga suas pastas
        /// </summary>
        /// <param name="rotaPasta"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool ApagarPastaComArquivos(string rotaPasta)
        {
            try
            {
                var listOptions = new ListObjectsOptions
                {
                    Delimiter = rotaPasta
                };

                var objects = FireStore.AcessoBaseStorage().ListObjects(Buket, options: listOptions);

                foreach (var storageObject in objects)
                {
                    FireStore.AcessoBaseStorage().DeleteObject(Buket, storageObject.Name);
                    Console.WriteLine($"Deleted object: {storageObject.Name}");
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(message: GlobalResources.Err_DeletarArquivosEmCascata + e.Message);
            }
        }
        public static string MontaRotaPasta(string nomeTabela, string nomeArquivo, string subPasta)
        {
            string retorno = "";

            if (!string.IsNullOrEmpty(nomeArquivo))
            {
                if (string.IsNullOrEmpty(subPasta))
                {
                    retorno = $"{nomeTabela}/{nomeArquivo}";
                }
                else
                {
                    retorno = $"{nomeTabela}/{subPasta}/{nomeArquivo}";                }
            }
            else
            {
                retorno = $"{nomeTabela}/{subPasta}";
            }

            return retorno;
        }
    }
}
