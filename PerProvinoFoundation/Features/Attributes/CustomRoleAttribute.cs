namespace PerProvinoFoundation.Features.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using PlayerRoles;

    public class CustomRoleAttribute : Attribute
    {
        internal readonly string Name;
        internal readonly RoleTypeId Role;
        internal readonly int Id;
        internal readonly bool IsEnabled;

        public CustomRoleAttribute(string name, RoleTypeId role, int id, bool isEnabled = true)
        {
            Name = name;
            Role = role;
            Id = id;
            IsEnabled = isEnabled;
        }
    }
}
