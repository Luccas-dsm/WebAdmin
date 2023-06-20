namespace WebAdmin.Shared.Models
{
    public class FeedbackModel
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public int Avaliacao { get; set; }
        public DateTime Data { get; set; }
    }
}
