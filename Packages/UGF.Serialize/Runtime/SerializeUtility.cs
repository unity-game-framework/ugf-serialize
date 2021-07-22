using System;
using System.Threading.Tasks;

namespace UGF.Serialize.Runtime
{
    public static class SerializeUtility
    {
        public static async Task<T> CopyAsync<T>(T target, ISerializerAsync serializer) where T : class
        {
            return (T)await CopyAsync((object)target, serializer);
        }

        public static async Task<object> CopyAsync(object target, ISerializerAsync serializer)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            object data = await serializer.SerializeAsync(target);
            object copy = await serializer.DeserializeAsync(target.GetType(), data);

            return copy;
        }

        public static T Copy<T>(T target, ISerializer serializer) where T : class
        {
            return (T)Copy((object)target, serializer);
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
