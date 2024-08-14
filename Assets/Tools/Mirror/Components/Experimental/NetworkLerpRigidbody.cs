using System;
using UnityEngine;

namespace Mirror.Experimental
{
    [AddComponentMenu("")]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-lerp-rigidbody")]
    [Obsolete("Use the new NetworkRigidbodyReliable/Unreliable component with Snapshot Interpolation instead.")]
    public class NetworkLerpRigidbody : NetworkBehaviour
    {
        [Header("Settings")]
        [SerializeField] internal Rigidbody target = null;

        [Tooltip("How quickly current velocity approaches target velocity")]
        [SerializeField]
        private float lerpVelocityAmount = 0.5f;

        [Tooltip("How quickly current position approaches target position")]
        [SerializeField]
        private float lerpPositionAmount = 0.5f;

        [Tooltip("Set to true if moves come from owner client, set to false if moves always come from server")]
        [SerializeField]
        private bool clientAuthority = false;

        private double nextSyncTime;

        [SyncVar()] private Vector3 targetVelocity;

        [SyncVar()] private Vector3 targetPosition;

        /// <summary>
        /// Ignore value if is host or client with Authority
        /// </summary>
        private bool IgnoreSync => isServer || ClientWithAuthority;

        private bool ClientWithAuthority => clientAuthority && isOwned;

        protected override void OnValidate()
        {
            base.OnValidate();
            Reset();
        }

        public virtual void Reset()
        {
            if (target == null)
                target = GetComponent<Rigidbody>();

            syncDirection = SyncDirection.ClientToServer;
        }

        private void Update()
        {
            if (isServer)
                SyncToClients();
            else if (ClientWithAuthority)
                SendToServer();
        }

        private void SyncToClients()
        {
            targetVelocity = target.velocity;
            targetPosition = target.position;
        }

        private void SendToServer()
        {
            var now = NetworkTime.localTime; // Unity 2019 doesn't have Time.timeAsDouble yet
            if (now > nextSyncTime)
            {
                nextSyncTime = now + syncInterval;
                CmdSendState(target.velocity, target.position);
            }
        }

        [Command]
        private void CmdSendState(Vector3 velocity, Vector3 position)
        {
            target.velocity = velocity;
            target.position = position;
            targetVelocity = velocity;
            targetPosition = position;
        }

        private void FixedUpdate()
        {
            if (IgnoreSync) { return; }

            target.velocity = Vector3.Lerp(target.velocity, targetVelocity, lerpVelocityAmount);
            target.position = Vector3.Lerp(target.position, targetPosition, lerpPositionAmount);
            // add velocity to position as position would have moved on server at that velocity
            target.position += target.velocity * Time.fixedDeltaTime;

            // TODO does this also need to sync acceleration so and update velocity?
        }
    }
}
