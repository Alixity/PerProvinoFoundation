namespace PerProvinoFoundation.Features.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Exiled.API.Features.Spawn;

    using PlayerRoles;

    public class BidelloBase : RoleManager
    {
        public override SpawnProperties SpawnProperties { get; set; } = new SpawnProperties()
        {
            RoleSpawnPoints = new List<RoleSpawnPoint>()
            {
                new RoleSpawnPoint() {Chance = 100, Role = RoleTypeId.ClassD}
            }
        };
        public override string Description { get; set; } = "Test";
        public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
    }
}
