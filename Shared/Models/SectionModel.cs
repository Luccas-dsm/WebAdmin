using Google.Cloud.Firestore;

namespace WebAdmin.Shared.Models
{
    [FirestoreData]
    public class SectionModel
    {
        public string Id { get; set; }

        [FirestoreProperty]
        public List<string> ListaId { get; set; }
        
        [FirestoreProperty]
        public string Titulo { get; set; }
        
        [FirestoreProperty]
        public int Ordem { get; set; }
    }
}
