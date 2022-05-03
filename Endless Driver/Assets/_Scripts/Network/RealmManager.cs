using UnityEngine;
using Realms;
using Realms.Sync;

namespace _Scripts.Network
{
    public class RealmManager : MonoBehaviour
    {
        private Realm m_Realm;

       async void OnEnable()
        {
            // Connect Realm backend
            var realmApp = App.Create(new AppConfiguration("application-0-mixpx")
            {
                MetadataPersistenceMode = MetadataPersistenceMode.NotEncrypted
            });

            var realmUser = realmApp.CurrentUser;
            if (realmUser == null)
            {
                realmUser = await realmApp.LogInAsync(Credentials.EmailPassword("email", "password"));
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

        private void OnDisable()
        {
            m_Realm.Dispose();
        }

    }
}
