namespace WebAdmin.Shared.Models
{
    public class Produto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public CategoriaModel Categoria { get; set; }
        public decimal Preco { get; set; }
        public List<string> Imagens { get; set; }
        public string Seq { get; set; }
        public int Estoque { get; set; }
        public double PesoOuQuantidade { get; set; }
        public Dimensoes Dimensoes { get; set; }
        public List<string> OpcoesVariacao { get; set; }
        public bool Disponibilidade { get; set; }
        public List<string> PalavrasChave { get; set; }
        public bool Destaque { get; set; }
        public bool ProdutoEmPromocao { get; set; }

    }
}
