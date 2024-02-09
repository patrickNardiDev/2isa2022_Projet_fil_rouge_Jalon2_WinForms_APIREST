using Domain.Entities;

namespace GestionMaterielIntExt.ObjTransf
{
    public class UserConnected
    {
        private bool _Connected = false;
        private string _Name = string.Empty;
        private Role.RoleEnum _Role = Domain.Entities.Role.RoleEnum.noRole;

        public bool Conneced { get => _Connected; private set => _Connected = value; }
        public string Name { get => _Name; private set => _Name = value; }
        public Role.RoleEnum Role { get => _Role; private set => _Role = value; }

        public UserConnected() { }
        public UserConnected(bool connected, string name, Role.RoleEnum role)
        {
            Conneced = connected;
            Name = name;
            Role = role;
        }
    }
}
