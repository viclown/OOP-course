using System;
using Isu.Tools;

namespace Isu.Services
{
    public class GroupName
    {
        public GroupName(string groupName)
        {
            if (!IsNameAllowed(groupName)) throw new InvalidGroupNameException();
            Name = groupName;
        }

        public string Name { get; }

        public static bool operator ==(GroupName first, GroupName second)
        {
            return first.Name == second.Name;
        }

        public static bool operator !=(GroupName first, GroupName second)
        {
            return first.Name != second.Name;
        }

        public override bool Equals(object o)
        {
            return o != null;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        private static bool IsNameAllowed(string groupName)
        {
            return groupName.Length == 5 && char.IsLetter(groupName[0]) && (groupName[1] == '3' || groupName[1] == '4')
                   && groupName[2] <= '4' && groupName[2] >= '1' && char.IsDigit(groupName[3]) && char.IsDigit(groupName[4]);
        }
    }
}