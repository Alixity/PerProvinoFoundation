namespace PerProvinoFoundation.Features
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Exiled.API.Features;

    using PerProvinoFoundation.Features.Attributes;

    using PlayerRoles;

    using UnityEngine.Assertions.Must;

    public abstract class CustomRole<T> : CustomRole
        where T : RoleManager
    {
        public override Type Component { get; set; } = typeof(T);
    }

    public abstract class CustomRole
    {
        public virtual Type Component { get; set; }

        private CustomRoleAttribute customRoleAttribute => GetType().GetCustomAttributes(typeof(CustomRoleAttribute), true).FirstOrDefault() as CustomRoleAttribute;

        public string Name => customRoleAttribute.Name;
        public int Id => customRoleAttribute.Id;

        public bool IsEnabled => customRoleAttribute.IsEnabled;

        public RoleTypeId Role => customRoleAttribute.Role;

        public static HashSet<CustomRole> Registered { get; set; } = new HashSet<CustomRole>();

        public static Dictionary<Player, CustomRole> TrackedPlayer { get; set; } = new Dictionary<Player, CustomRole>();

        public static List<CustomRole> RegisterRole()
        {
            List<CustomRole> customRoles = new List<CustomRole>();
            foreach(Type type in Assembly.GetCallingAssembly().GetTypes())
            {
                if ((type.BaseType != typeof(CustomRole) && !type.IsSubclassOf(typeof(CustomRole))) || type.GetCustomAttribute(typeof(CustomRoleAttribute)) is null)
                    continue;

                CustomRole customRole = Activator.CreateInstance(type) as CustomRole;

                if (!customRole.IsEnabled)
                    continue;

                if (customRole.TryRegister())
                    customRoles.Add(customRole);
            }

            return customRoles;
        }

        public static List<CustomRole> UnRegisterRole()
        {
            List<CustomRole> customRoles = new List<CustomRole>();

            foreach(CustomRole customRole in Registered)
            {
                if (customRole.TryUnregister())
                    customRoles.Add(customRole);
            }

            return customRoles;
        }

        private bool TryRegister()
        {
            if (!Registered.Contains(this))
            {
                if (Registered.Any(x => x.Name == Name))
                {
                    Log.Error($"Impossibile registrare il CustomRole {Name}: Questo nome e' gia' stato registrato");
                    return false;
                }

                if (Registered.Any(x => x.Id == Id))
                {
                    Log.Error($"Impossibile registrare il CustomRole {Name}: Questo ID ( \" {Id} \" ) e' gia stato registrato");
                    return false;
                }

                return true;
            }

            Log.Error($"CustomRole {Name} :: {Id} gia' registrato");
            return false;
        }

        private bool TryUnregister()
        {
            if (Registered.Contains(this))
            {
                return true;
            }

            Log.Error($"CustomRole {Name} :: {Id} gia' rimosso");
            return false;
        }

        private static CustomRole Get<T>() where T : CustomRole => Registered.FirstOrDefault(x => x.GetType().FullName == typeof(T).FullName);

        private static CustomRole Get(int id) => Registered.FirstOrDefault(x => x.Id == id);
        private static CustomRole Get(string name) => Registered.FirstOrDefault(x => x.Name == name);


        public static bool TryGet<T>(out CustomRole customRole)
            where T : CustomRole
        {
            customRole = Get<T>();

            return customRole != null;
        }

        public static bool TryGet(int id, out CustomRole customRole)
        {
            customRole = Get(id);

            return customRole != null;
        }

        public static bool TryGet(string name, out CustomRole customRole)
        {
            customRole = Get(name);

            return customRole != null;
        }

        public static void Spawn(Player player, int id)
        {
            if (TryGet(id, out CustomRole customRole))
            {
                player.GameObject.AddComponent(customRole.Component);
                TrackedPlayer.Add(player, customRole);
            }
        }

        public static bool TryDestroy(Player player)
        {
            if (TrackedPlayer.ContainsKey(player))
            {
                TrackedPlayer.Remove(player);
                return true;
            }

            Log.Info("Il giocatore non ha un CustomRole");
            return false;
        }
    }
}
