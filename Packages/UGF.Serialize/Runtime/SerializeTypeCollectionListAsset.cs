using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Serialize.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Serialize/Serialize Type Collection List", order = 2000)]
    public class SerializeTypeCollectionListAsset : SerializeTypeCollectionAsset
    {
        [SerializeField] private List<SerializeTypeData> m_types = new List<SerializeTypeData>();

        public List<SerializeTypeData> Types { get { return m_types; } }

        protected override void OnGetTypes(IDictionary<object, Type> types)
        {
            for (int i = 0; i < m_types.Count; i++)
            {
                SerializeTypeData data = m_types[i];

                types.Add(data.GetId(), data.Type.Get());
            }
        }
    }
}
