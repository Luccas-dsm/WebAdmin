using WebAdmin.Shared.Commons.Enums;

namespace WebAdmin.Shared.Models
{
    public class PedidoModel
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public StatusPedido Status { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime? DataEntrega { get; set; }

    }
}
