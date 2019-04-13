using BaseGameLogic.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic
{
    [CreateAssetMenu(fileName = "AvatarManager", menuName = "Player/Avatar Manager")]
    public class AvatarManager : SingletonScriptableObject<AvatarManager>
    {
        [SerializeField] private List<GameObject> avatars = new List<GameObject>();
        private Dictionary<string, GameObject> avatarsDictionary = new Dictionary<string, GameObject>();

        protected override void Initialize()
        {
            foreach (var item in avatars)
                avatarsDictionary.Add(item.name, item);
        }

        public GameObject GetAvatart(string name)
        {
            GameObject avatar = null;
            avatarsDictionary.TryGetValue(name, out avatar);
            return avatar == null ? null : Instantiate(avatar);
        }
    }
}