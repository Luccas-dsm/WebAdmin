using Google.Cloud.Firestore;

namespace WebAdmin.Shared.Models
{
    public class ProdutoModel
    {
        [FirestoreProperty]
        public string Nome { get; set; }
        [FirestoreProperty]
        public string Descricao { get; set; }
        [FirestoreProperty]
        public CategoriaModel Categoria { get; set; }
        [FirestoreProperty]
        public decimal Preco { get; set; }
        [FirestoreProperty]
        public List<string> Imagens { get; set; }
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public int Estoque { get; set; }
        [FirestoreProperty]
        public double PesoOuQuantidade { get; set; }
        [FirestoreProperty]
        public Dimensoes? Dimensoes { get; set; }
        [FirestoreProperty]
        public List<string>? OpcoesVariacao { get; set; }
        [FirestoreProperty]
        public bool Disponibilidade { get; set; }
        [FirestoreProperty]
        public List<string>? PalavrasChave { get; set; }
        [FirestoreProperty]
        public bool Destaque { get; set; }
        [FirestoreProperty]
        public bool ProdutoEmPromocao { get; set; }

    }
}
