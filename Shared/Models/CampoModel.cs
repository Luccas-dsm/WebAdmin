using Google.Cloud.Firestore;

namespace WebAdmin.Shared.Models
{
    public class CampoModel
    {
        [FirestoreProperty]
        public string Input { get; set; }

        [FirestoreProperty]
        public bool Obrigatorio { get; set; }

        [FirestoreProperty]
        public List<string>? Opcoes { get; set; }

        [FirestoreProperty]
        public string Informativo { get; set; }
    }
}
