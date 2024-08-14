using Mirror;

namespace MirrorExtensions
{
    public static class CustomReadAndWritePlayer
    {
        public static void WritePlayer(this NetworkWriter writer, Player.Player player)
        {
            var networkIdentity = player.GetComponent<NetworkIdentity>();
            writer.WriteNetworkIdentity(networkIdentity);
        }

        public static Player.Player ReadPlayer(this NetworkReader reader)
        {
            var networkIdentity = reader.ReadNetworkIdentity();
            var player = networkIdentity != null
                ? networkIdentity.GetComponent<Player.Player>()
                : null;

            return player;
        }
    }
}
