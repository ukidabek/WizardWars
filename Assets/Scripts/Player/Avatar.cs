using UnityEngine;

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

        avatar.transform.SetParent(spot);
        avatar.transform.localPosition = Vector3.zero;
        avatar.transform.localRotation = Quaternion.identity;

        animator = avatar.GetComponent<Animator>();
    }

    public void Cast()
    {
        animator?.SetTrigger(castAnimationName);
    }
}
