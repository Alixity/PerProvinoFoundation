namespace PerProvinoFoundation.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Exiled.Events.EventArgs.Player;

    using PerProvinoFoundation.Features.Extension;

    public class PlayerHandler
    {
        public void Load(bool x)
        {
            if (x)
            {
                Exiled.Events.Handlers.Player.Spawning += OnPlayerSpawn;
            }
            else
            {
                Exiled.Events.Handlers.Player.Spawning -= OnPlayerSpawn;

            }
        }

        /// <summary>
        /// e' giusto per testare.
        /// </summary>
        /// <param name="ev"></param>
        void OnPlayerSpawn(SpawningEventArgs ev)
        {
            if (!ev.Player.HasCustomRole())
                ev.Player.SpawnCustomRole(1);
        }
    }
}
