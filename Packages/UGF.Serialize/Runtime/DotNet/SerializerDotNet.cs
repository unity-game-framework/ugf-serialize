using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace UGF.Serialize.Runtime.DotNet
{
    public class SerializerDotNet : SerializerBase
    {
        public IFormatter Formatter { get; }

        public static ISerializer Default { get; } = new SerializerDotNet(new BinaryFormatter());
        public static int DefaultMode { get; } = 10;

        public SerializerDotNet(IFormatter formatter)
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
