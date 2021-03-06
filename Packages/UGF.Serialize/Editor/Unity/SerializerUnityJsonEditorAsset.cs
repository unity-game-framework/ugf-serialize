﻿using UGF.Serialize.Runtime;
using UnityEngine;

namespace UGF.Serialize.Editor.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Serialize/Serializer Unity Json Editor", order = 2000)]
    public class SerializerUnityJsonEditorAsset : SerializerAsset<string>
    {
        [SerializeField] private bool m_readable;

        public bool Readable { get { return m_readable; } set { m_readable = value; } }

        protected override ISerializer<string> OnBuildTyped()
        {
            return new SerializerUnityJsonEditor(m_readable);
        }
    }
}
