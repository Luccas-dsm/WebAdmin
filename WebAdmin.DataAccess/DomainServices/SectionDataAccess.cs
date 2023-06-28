using Google.Cloud.Firestore;
using Newtonsoft.Json;
using WebAdmin.Shared.Models;

namespace WebAdmin.DataAccess.DomainServices
{
    public class SectionDataAccess
    {
        Access fireStore = new Access("genesis-93f18");

        public async Task<List<SectionModel>> GetSectionSelect(List<string> listaSeqs)
        {
            try
            {
                List<SectionModel> lstSection = new List<SectionModel>();

                // Consultar os produtos no Firestore
                Query query = fireStore.AcessoBaseFireStore().Collection("section")
                    .WhereIn(FieldPath.DocumentId, listaSeqs);


                QuerySnapshot querySnapshot = query.GetSnapshotAsync().Result;

                foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> city = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(city);
                        SectionModel newuser = JsonConvert.DeserializeObject<SectionModel>(json);
                        newuser.Id = documentSnapshot.Id;
                        lstSection.Add(newuser);
                    }
                }

                return lstSection.OrderBy(o => o.Ordem).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao fazer o select a lista, codigo :" + ex.Message);
            }
        }

        public async Task<List<SectionModel>> GetAllSections()
        {
            try
            {
                Query sectionQuery = fireStore.AcessoBaseFireStore().Collection("section");
                QuerySnapshot sectionQuerySnapshot = await sectionQuery.GetSnapshotAsync();
                List<SectionModel> lstSection = new List<SectionModel>();

                foreach (DocumentSnapshot documentSnapshot in sectionQuerySnapshot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> city = documentSnapshot.ToDictionary();
                        string json = JsonConvert.SerializeObject(city);
                        SectionModel newuser = JsonConvert.DeserializeObject<SectionModel>(json);
                        newuser.Id = documentSnapshot.Id;
                        lstSection.Add(newuser);
                    }
                }

                return lstSection.OrderBy(x => x.Ordem).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao buscar as sections, codigo: " + ex.Message);
            }
        }
        public async void AddSection(SectionModel section)
        {
            try
            {
                CollectionReference colRef = fireStore.AcessoBaseFireStore().Collection("section");
                await colRef.AddAsync(section);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao buscar adicionar a Section, codigo: " + ex.Message);
            }
        }
        public async void UpdateSection(SectionModel section)
        {
            try
            {
                DocumentReference docRef = fireStore.AcessoBaseFireStore().Collection("section").Document(section.Id);
                await docRef.SetAsync(section, SetOptions.Overwrite);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro em alterar a Section, codigo: " + ex.Message);
            }
        }
        public async Task<SectionModel> GetSection(string id)
        {
            try
            {
                DocumentReference docRef = fireStore.AcessoBaseFireStore().Collection("section").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    SectionModel section = snapshot.ConvertTo<SectionModel>();
                    section.Id = snapshot.Id;
                    return section;
                }
                else
                {
                    return new SectionModel();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro em buscar a Section, codigo: " + ex.Message);
            }
        }
        public async void DeleteSection(string id)
        {
            try
            {
                DocumentReference sectionRef = fireStore.AcessoBaseFireStore().Collection("section").Document(id);
                await sectionRef.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro em deletar a Section, codigo: " + ex.Message);
            }
        }
    }
}
