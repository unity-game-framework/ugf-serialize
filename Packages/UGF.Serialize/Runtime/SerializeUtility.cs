using System;

namespace UGF.Serialize.Runtime
{
    public static class SerializeUtility
    {
        public static T Copy<T>(T target, ISerializer serializer) where T : class
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            object data = serializer.Serialize(target);
            var copy = serializer.Deserialize<T>(data);

            return copy;
        }

        public static object Copy(object target, ISerializer serializer)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            object data = serializer.Serialize(target);
            object copy = serializer.Deserialize(target.GetType(), data);

            return copy;
        }
    }
}
