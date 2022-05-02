using UnityEngine;
using Realms;

namespace _Scripts.Network
{
    public class RealmManager : MonoBehaviour
    {
        private Realm m_Realm;

        private void OnEnable()
        {
            m_Realm = Realm.GetInstance();
        }

        private void OnDisable()
        {
            m_Realm.Dispose();
        }

    }
}
