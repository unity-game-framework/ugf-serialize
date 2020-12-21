#if UGF_SERIALIZE_JSONNET
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UGF.EditorTools.Runtime.IMGUI.Types;
using UGF.JsonNet.Runtime;
using UGF.JsonNet.Runtime.Converters;
using UnityEngine;

namespace UGF.Serialize.Runtime.JsonNet
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Serialize/Serializer JsonNet Convert Types", order = 2000)]
    public class SerializerJsonNetConvertTypesAsset : SerializerJsonNetConvertNamesAsset
    {
        [SerializeField] private List<ConvertTypeData> m_types = new List<ConvertTypeData>();

        public List<ConvertTypeData> Types { get { return m_types; } }

        [Serializable]
        public struct ConvertTypeData
        {
            [SerializeField] private string m_name;
            [SerializeField] private string m_assembly;
            [TypeReferenceDropdown]
            [SerializeField] private TypeReference<object> m_type;

            public string Name { get { return m_name; } set { m_name = value; } }
            public string Assembly { get { return m_assembly; } set { m_assembly = value; } }
            public TypeReference<object> Type { get { return m_type; } set { m_type = value; } }

            public bool IsValid()
            {
                return m_type.HasValue && !string.IsNullOrEmpty(m_name);
            }
        }

        protected override ISerializer<string> OnBuildTyped()
        {
            var binder = new ConvertTypeNameBinder();

            SetupTypes(binder.Provider);

            JsonSerializerSettings settings = JsonNetUtility.CreateDefault();

            settings.SerializationBinder = binder;

            var serializer = new SerializerJsonNetConvertNames(settings, Readable);

            SetupNames(serializer);

            return serializer;
        }

        protected virtual void SetupTypes(IConvertTypeProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            for (int i = 0; i < m_types.Count; i++)
            {
                ConvertTypeData data = m_types[i];

                if (!data.IsValid()) throw new ArgumentException("Value should be valid.", nameof(data));

                Type type = data.Type.Get();

                if (!string.IsNullOrEmpty(data.Assembly))
                {
                    provider.Add(type, data.Name, data.Assembly);
                }
                else
                {
                    provider.Add(type, data.Name);
                }
            }
        }
    }
}
#endif
