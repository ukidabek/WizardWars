using UnityEngine;

namespace PlayerLogic
{
    public class Avatar : MonoBehaviour
    {
        [SerializeField] private Transform spot = null;
        [SerializeField] private Animator animator = null;

        [Space]
        [SerializeField] string castAnimationName = "Cast";

        public void Load(Player.PlayerInfo playerInfo)
        {
            Load(playerInfo.AvatarID);
        }

        public void Load(string avatartID)
        {
            var avatar = AvatarManager.Instance.GetAvatart(avatartID);
            if (avatar != null)
            {
                avatar.transform.SetParent(spot);
                avatar.transform.localPosition = Vector3.zero;
                avatar.transform.localRotation = Quaternion.identity;

                animator = avatar.GetComponent<Animator>();
            }
            else
                Debug.LogErrorFormat("Avatar named {0} does not exist!", avatartID);
        }

        public void Cast()
        {
            animator?.SetTrigger(castAnimationName);
        }
    }
}