using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using PerProvinoFoundation.Configs;
using PerProvinoFoundation.Handler;

namespace PerProvinoFoundation
{
    /// <summary>
    /// L'ho fatto al volo, se ci sono errori o del codice fatto a cazzo non vi incazzate.
    /// </summary>
    public class Foundation : Plugin<Config>
    {
        public static Foundation Instance { get; set; }

        internal PlayerHandler PlayerHandler;
        public override void OnEnabled()
        {
            CustomRole.RegisterRoles();
            PlayerHandler = new PlayerHandler();

            PlayerHandler.Load(true);

            Instance = this;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            CustomRole.UnregisterRoles();
            PlayerHandler.Load(false);

            PlayerHandler = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}
