using System;
using System.IO;
using System.Runtime.Serialization;

namespace UGF.Serialize.Runtime.Formatter
{
    /// <summary>
    /// Represents serialized that use formatter to serialize a specified target to byte array and vice versa.
    /// </summary>
    public class SerializerFormatter : SerializerBase<byte[]>
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
            if (target == null) throw new ArgumentNullException(nameof(target));

            using (var stream = new MemoryStream())
            {
                Formatter.Serialize(stream, target);

                return stream.ToArray();
            }
        }

        public override object Deserialize(Type targetType, byte[] data)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (data == null) throw new ArgumentNullException(nameof(data));

            using (var stream = new MemoryStream(data))
            {
                return Formatter.Deserialize(stream);
            }
        }
    }
}
