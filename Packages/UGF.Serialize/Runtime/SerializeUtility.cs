using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Serialize.Runtime
{
    public static class SerializeUtility
    {
        public static async Task<T> CopyAsync<T>(T target, ISerializerAsync serializer, IContext context) where T : class
        {
            return (T)await CopyAsync((object)target, serializer, context);
        }

        public static async Task<object> CopyAsync(object target, ISerializerAsync serializer, IContext context)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            object data = await serializer.SerializeAsync(target, context);
            object copy = await serializer.DeserializeAsync(target.GetType(), data, context);

            return copy;
        }

        public static T Copy<T>(T target, ISerializer serializer, IContext context) where T : class
        {
            return (T)Copy((object)target, serializer, context);
        }

        public static object Copy(object target, ISerializer serializer, IContext context)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            object data = serializer.Serialize(target, context);
            object copy = serializer.Deserialize(target.GetType(), data, context);

            return copy;
        }
    }
}
