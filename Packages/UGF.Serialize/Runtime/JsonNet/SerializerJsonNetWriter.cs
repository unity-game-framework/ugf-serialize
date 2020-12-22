#if UGF_SERIALIZE_JSONNET
using System.IO;
using Newtonsoft.Json;

namespace UGF.Serialize.Runtime.JsonNet
{
    public class SerializerJsonNetWriter : JsonTextWriter
    {
        public TextWriter Writer { get; }

        public SerializerJsonNetWriter(TextWriter writer) : base(writer)
        {
            Writer = writer;
        }

        public override string ToString()
        {
            return Writer.ToString();
        }
    }
}
#endif
