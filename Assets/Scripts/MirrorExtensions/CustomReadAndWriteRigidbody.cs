using Mirror;
using UnityEngine;

namespace MirrorExtensions
{
    public static class CustomReadAndWriteRigidbody
    {
        public static void WriteRigidbody(this NetworkWriter writer, Rigidbody rigidbody)
        {
            var networkIdentity = rigidbody.GetComponent<NetworkIdentity>();
            writer.WriteNetworkIdentity(networkIdentity);
        }

        public static Rigidbody ReadRigidbody(this NetworkReader reader)
        {
            var networkIdentity = reader.ReadNetworkIdentity();
            var rigidBody = networkIdentity != null
                ? networkIdentity.GetComponent<Rigidbody>()
                : null;

            return rigidBody;
        }
    }
}
