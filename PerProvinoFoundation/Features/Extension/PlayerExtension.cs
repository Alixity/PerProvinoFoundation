namespace PerProvinoFoundation.Features.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Exiled.API.Features;

    using PlayerStatsSystem;

    public static class PlayerExtension
    {
        public static bool HasCustomRole(this Player player) => CustomRole.TrackedPlayer.ContainsKey(player);

        public static void SpawnCustomRole(this Player player, int id)
        {
            CustomRole.Spawn(player, id);
        }

        public static void DestroyCustomRole(this Player player)
        {
            CustomRole.TryDestroy(player);
        }
    }
}
