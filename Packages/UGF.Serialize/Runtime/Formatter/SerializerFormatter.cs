using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace UGF.Serialize.Runtime.Formatter
{
    /// <summary>
    /// Represents serialized that use formatter to serialize a specified target to byte array and vice versa.
    /// </summary>
    public class SerializerFormatter : SerializerAsyncBase<byte[]>
    {
        /// <summary>
        /// Gets the formatter used to convert target to byte array and vice versa.
        /// </summary>
        public IFormatter Formatter { get; }

        /// <summary>
        /// Creates serializer with the specified formatter.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        public SerializerFormatter(IFormatter formatter)
        {
            Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        public override byte[] Serialize(object target)
        {
            return SerializeInternal(Formatter, target);
        }

        public override object Deserialize(Type targetType, byte[] data)
        {
            return DeserializeInternal(Formatter, data);
        }

        public override Task<byte[]> SerializeAsync(object target)
        {
            return Task.Run(() => SerializeInternal(Formatter, target));
        }

        public override Task<object> DeserializeAsync(Type targetType, byte[] data)
        {
            return Task.Run(() => DeserializeInternal(Formatter, data));
        }

        private static byte[] SerializeInternal(IFormatter formatter, object target)
        {
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            if (target == null) throw new ArgumentNullException(nameof(target));

            using var stream = new MemoryStream();

            formatter.Serialize(stream, target);

            return stream.ToArray();
        }

        private static object DeserializeInternal(IFormatter formatter, byte[] data)
        {
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));
            if (data == null) throw new ArgumentNullException(nameof(data));

            using var stream = new MemoryStream(data);

            return formatter.Deserialize(stream);
        }
    }
}
