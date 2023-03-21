using System.Threading.Tasks;
using UnityEngine;
using Realms;
using Realms.Sync;
using Realms.Sync.Exceptions;

namespace _Scripts.Network
{
    public class RealmManager : MonoBehaviour
    {
        private Realm m_Realm;
        public static RealmManager Instance;
        
        void Awake() {
            
            if (Instance == null)
                Instance = this;
            else
            {
                //
            }
            DontDestroyOnLoad(gameObject);

        }

        private App realmApp;
        private User realmUser;
        async void OnEnable()
        {
            // Connect Realm backend
            realmApp = App.Create(new AppConfiguration("application-0-mixpx")
            {
                MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
            });
            
            realmUser = realmApp.CurrentUser;
            if (realmUser == null)
            {
                realmUser = await realmApp.LogInAsync(Credentials.EmailPassword("dalcikferhateren@ogr.iu.edu.tr",
                    "testUser"));
                m_Realm = await Realm.GetInstanceAsync(new SyncConfiguration("partition", realmUser));
            }
            else
            {
                m_Realm = Realm.GetInstance(new SyncConfiguration("partition", realmUser));
            }

            // Get realm Instance if player is null create one.
            //  m_Realm = Realm.GetInstance();

            var playerProfile = m_Realm.Find<PlayerProfile>(realmUser.Id);
            if (playerProfile == null)
            {
                m_Realm.Write(() => playerProfile = m_Realm.Add(new PlayerProfile(realmUser.Id)));
            }
        }
        
        void OnDisable() {

            if(m_Realm != null) {
                m_Realm.Dispose();

            }

        }

        public async Task<string> Login(string email, string password)
        {
            if (email == "" || password == "") return "";
            try {

                if(realmUser == null) {

                    realmUser = await realmApp.LogInAsync(Credentials.EmailPassword(email, password));

                    m_Realm = await Realm.GetInstanceAsync(new SyncConfiguration(email, realmUser));

                } else {

                    m_Realm = Realm.GetInstance(new SyncConfiguration(email, realmUser));

                }

            } catch (ClientResetException clientResetEx) {

                if(m_Realm != null) {

                    m_Realm.Dispose();

                }

                clientResetEx.InitiateClientReset();

            }

            return realmUser.Id;

        }

        public PlayerProfile GetPlayerProfile()
        {
            PlayerProfile _playerProfile = m_Realm.Find<PlayerProfile>(realmUser.Id);

            if(_playerProfile == null) {

                m_Realm.Write(() => {

                    _playerProfile = m_Realm.Add(new PlayerProfile(realmUser.Id));

                });

            }

            return _playerProfile;
        }

        public void IncreaseScore()
        {
            PlayerProfile _playerProfile = GetPlayerProfile();

            if(_playerProfile != null) {

                m_Realm.Write(() => {

                    _playerProfile.Score++;

                });

            }
        }

        public void ResetScore()
        {
            PlayerProfile _playerProfile = GetPlayerProfile();

            if(_playerProfile != null) {

                m_Realm.Write(() => {

                    if(_playerProfile.Score > _playerProfile.HighScore) {

                        _playerProfile.HighScore = _playerProfile.Score;

                    }

                    _playerProfile.Score = 0;

                });

            }
        }


    }
}
