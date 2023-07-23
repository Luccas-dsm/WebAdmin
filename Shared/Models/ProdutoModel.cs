using Google.Cloud.Firestore;

namespace WebAdmin.Shared.Models
{
    [FirestoreData]
    public class ProdutoModel
    {
        public string? Id { get; set; }

        [FirestoreProperty]
        public string? Nome { get; set; }

        [FirestoreProperty]
        public string? Descricao { get; set; }

        [FirestoreProperty]
        public List<ArquivoModel?>? Imagem { get; set; }

        [FirestoreProperty]
        public List<CampoModel>? Campos { get; set; }

        [FirestoreProperty]
        public List<string?>? PalavrasChave { get; set; }

        [FirestoreProperty]
        public bool Destaque { get; set; }

        [FirestoreProperty]
        public bool ProdutoEmPromocao { get; set; }

    }
}
