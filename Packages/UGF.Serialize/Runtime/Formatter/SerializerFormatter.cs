using System;
using System.IO;
using System.Runtime.Serialization;

namespace UGF.Serialize.Runtime.Formatter
{
    public class SerializerFormatter : Serializer<byte[]>
    {
        public IFormatter Formatter { get; }

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
