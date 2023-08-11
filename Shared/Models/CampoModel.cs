using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace WebAdmin.Shared.Models
{
    public class CampoModel
    {
        [Required]
        [FirestoreProperty]
        public string? Input { get; set; }

        [Required]
        [FirestoreProperty]
        public bool Obrigatorio { get; set; }

        [Required]
        [FirestoreProperty]
        public List<string?>? Opcoes { get; set; }

        [Required]
        [FirestoreProperty]
        public string? Informativo { get; set; }
    }
}
