using System;
using UGF.EditorTools.Runtime.IMGUI.Types;

namespace UGF.Serialize.Runtime
{
    public class SerializeTypeReferenceDropdownAttribute : TypesDropdownAttributeBase
    {
        public override Type TargetType { get; }

        public SerializeTypeReferenceDropdownAttribute(Type targetType = null)
        {
            TargetType = targetType ?? typeof(object);
        }
    }
}
