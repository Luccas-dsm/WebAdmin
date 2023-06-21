using Google.Cloud.Firestore;

namespace WebAdmin.Shared.Models
{
    [FirestoreData]
    public class Dimensoes
    {
        [FirestoreProperty]
        public double Comprimento { get; set; }
        [FirestoreProperty]
        public double Largura { get; set; }
        [FirestoreProperty]
        public double Altura { get; set; }
    }
}
