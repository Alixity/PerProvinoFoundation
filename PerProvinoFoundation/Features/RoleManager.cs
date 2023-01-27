namespace PerProvinoFoundation.Features
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Exiled.API.Features;
    using Exiled.API.Features.Spawn;
    using Exiled.Events.EventArgs.Player;

    using PlayerRoles;

    using UnityEngine;

    public abstract class RoleManager : MonoBehaviour
    {

        public virtual List<ItemType> SpawnInventory { get; set; } = null;
        public abstract SpawnProperties SpawnProperties { get; set; }
        public abstract string Description { get; set; }
        public abstract RoleTypeId Role { get; set; }

        public virtual Player User { get; private set; }
        protected virtual void Awake()
        {
            User = Player.Get(gameObject);
            SubscribeEvents();
        }

        protected virtual void Start()
        {
            User.Broadcast(5, Description);

            if (SpawnInventory != null)
                User.ResetInventory(SpawnInventory);
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeEvents();
        }

        protected virtual void Destroy()
        {
            if (User is null)
                Destroy(this);
        }

        protected virtual void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
        }

        protected virtual void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
        }

        void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player == User)
                Destroy(this);
        }
    }
}
