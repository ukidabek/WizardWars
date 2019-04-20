using PlayerLogic;
using UnityEngine;

public abstract class PlayerObserwator : MonoBehaviour
{
    [SerializeField] private Player.ID playerID = Player.ID.A;
    [SerializeField] private Player watchedPlayer = null;

    protected virtual void Awake()
    {
        Player.OnPlayerAdded += ConnectToPlayer;

    }

    private void ConnectToPlayer(Player player)
    {
        if (playerID == player.Id)
            StartObserwation(watchedPlayer = player);
    }

    private void OnDestroy()
    {
        EndObserwation(watchedPlayer);
    }

    protected abstract void StartObserwation(Player player);
    protected abstract void EndObserwation(Player player);
}
