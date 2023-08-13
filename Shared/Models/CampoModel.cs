using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace WebAdmin.Shared.Models
{
    [FirestoreData]
    public class CampoModel
    {
        [Required(ErrorMessage ="Este campo \"nome do campo\" é obrigatório")]
        [FirestoreProperty]
        public string Input { get; set; }

        [Required(ErrorMessage = "Este campo \"Campo Obrigatório\" é obrigatório")]
        [FirestoreProperty]
        public bool Obrigatorio { get; set; }

        [Required(ErrorMessage = "Este campo \"opções\" é obrigatório")]
        [FirestoreProperty]
        public List<string>? Opcoes { get; set; }

        [Required(ErrorMessage = "Este campo \"informativo\" é obrigatório")]
        [FirestoreProperty]
        public string Informativo { get; set; }
    }
}
