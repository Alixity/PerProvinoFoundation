namespace PerProvinoFoundation.Features.Role
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using PerProvinoFoundation.Features.Attributes;
    using PerProvinoFoundation.Features.Components;


    /// <summary>
    /// Inutile aggiungere nulla, serve solo per registrare il ruolo.
    /// </summary>
    [CustomRole("Bidello", PlayerRoles.RoleTypeId.ClassD, 1)]
    public class Bidello : CustomRole<BidelloBase>
    {

    }
}
