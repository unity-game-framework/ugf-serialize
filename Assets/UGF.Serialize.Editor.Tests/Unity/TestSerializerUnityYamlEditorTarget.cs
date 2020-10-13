using System;
using UnityEngine;

namespace UGF.Serialize.Editor.Tests.Unity
{
    [Serializable]
    public class TestSerializerUnityYamlEditorTarget : ScriptableObject
    {
        [SerializeField] private bool m_boolValue = true;
        [SerializeField] private int m_intValue = 10;
        [SerializeField] private float m_floatValue = 10.5F;

        public bool BoolValue { get { return m_boolValue; } set { m_boolValue = value; } }
        public int IntValue { get { return m_intValue; } set { m_intValue = value; } }
        public float FloatValue { get { return m_floatValue; } set { m_floatValue = value; } }
    }
}
