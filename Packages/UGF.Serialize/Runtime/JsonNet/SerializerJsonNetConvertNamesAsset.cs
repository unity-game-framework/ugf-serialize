#if UGF_SERIALIZE_JSONNET
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Serialize.Runtime.JsonNet
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Serialize/Serializer JsonNet Convert Names", order = 2000)]
    public class SerializerJsonNetConvertNamesAsset : SerializerJsonNetAsset
    {
        [SerializeField] private List<ConvertNameData> m_serializeNames = new List<ConvertNameData>();
        [SerializeField] private List<ConvertNameData> m_deserializeNames = new List<ConvertNameData>();

        public List<ConvertNameData> SerializeNames { get { return m_serializeNames; } }
        public List<ConvertNameData> DeserializeNames { get { return m_deserializeNames; } }

        [Serializable]
        public struct ConvertNameData
        {
            [SerializeField] private string m_from;
            [SerializeField] private string m_to;

            public string From { get { return m_from; } set { m_from = value; } }
            public string To { get { return m_to; } set { m_to = value; } }

            public bool IsValid()
            {
                return !string.IsNullOrEmpty(m_from) && !string.IsNullOrEmpty(m_to);
            }
        }

        protected override ISerializer<string> OnBuildTyped()
        {
            var serializer = new SerializerJsonNetConvertNames();

            SetupNames(serializer);

            return serializer;
        }

        protected virtual void SetupNames(SerializerJsonNetConvertNames serializer)
        {
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            for (int i = 0; i < m_serializeNames.Count; i++)
            {
                ConvertNameData data = m_serializeNames[i];

                if (!data.IsValid()) throw new ArgumentException("Value should be valid.", nameof(data));

                serializer.SerializeNames.Add(data.From, data.To);
            }

            for (int i = 0; i < m_deserializeNames.Count; i++)
            {
                ConvertNameData data = m_deserializeNames[i];

                if (!data.IsValid()) throw new ArgumentException("Value should be valid.", nameof(data));

                serializer.DeserializeNames.Add(data.From, data.To);
            }
        }
    }
}
#endif
