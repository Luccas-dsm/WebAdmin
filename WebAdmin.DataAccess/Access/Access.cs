using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Storage.V1;
namespace WebAdmin.DataAccess
{
    public class Access
    {
        private string BancoReferente;

        public Access() { }
        public Access(string bancoReferente)
        {
            this.BancoReferente = bancoReferente;
        }

        public FirestoreDb AcessoBaseFireStore()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", JsonPath());
            return FirestoreDb.Create(this.BancoReferente);
        }

        public StorageClient AcessoBaseStorage()
        {
            return StorageClient.Create(GoogleCredential.FromFile(JsonPath()));
        }

        private string JsonPath() =>  "../WebAdmin.DataAccess/Access/genesis-93f18-firebase-adminsdk-j2gxw-f50f8eb355.json";

    }
}
