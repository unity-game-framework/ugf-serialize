﻿using System;
using NUnit.Framework;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.Serialize.Editor.Unity;
using UGF.Serialize.Runtime;
using UnityEngine;

namespace UGF.Serialize.Editor.Tests.Unity
{
    public class TestSerializerUnityJsonEditor
    {
        [Serializable]
        private class Target
        {
            [SerializeField] private bool m_boolValue = true;
            [SerializeField] private int m_intValue = 10;
            [SerializeField] private float m_floatValue = 10.5F;

            public bool BoolValue { get { return m_boolValue; } set { m_boolValue = value; } }
            public int IntValue { get { return m_intValue; } set { m_intValue = value; } }
            public float FloatValue { get { return m_floatValue; } set { m_floatValue = value; } }
        }

        [Test]
        public void Serialize()
        {
            var serialize = new SerializerUnityJsonEditor();
            var target = new Target();

            string text = serialize.Serialize(target, new Context());

            Assert.NotNull(text);
            Assert.Greater(text.Length, 0);
        }

        [Test]
        public void Deserialize()
        {
            var serialize = new SerializerUnityJsonEditor();
            var target = new Target();

            string text = serialize.Serialize(target, new Context());
            var target0 = serialize.Deserialize<Target>(text, new Context());

            Assert.AreEqual(target.BoolValue, target0.BoolValue);
            Assert.AreEqual(target.IntValue, target0.IntValue);
            Assert.AreEqual(target.FloatValue, target0.FloatValue);
        }

        [Test]
        public void DeserializeEmpty()
        {
            var serialize = new SerializerUnityJsonEditor();

            var target = serialize.Deserialize<Target>(string.Empty, new Context());

            Assert.NotNull(target);
            Assert.IsInstanceOf<Target>(target);
        }

        [Test]
        public void SerializeScriptableObject()
        {
            var serialize = new SerializerUnityJsonEditor();
            var target = ScriptableObject.CreateInstance<TestSerializerUnityYamlEditorTarget>();

            string text = serialize.Serialize(target, new Context());

            Assert.NotNull(text);
            Assert.Greater(text.Length, 0);
        }

        [Test]
        public void DeserializeScriptableObject()
        {
            var serialize = new SerializerUnityJsonEditor();
            var target = ScriptableObject.CreateInstance<TestSerializerUnityYamlEditorTarget>();

            string text = serialize.Serialize(target, new Context());
            var target0 = serialize.Deserialize<TestSerializerUnityYamlEditorTarget>(text, new Context());

            Assert.AreEqual(target.BoolValue, target0.BoolValue);
            Assert.AreEqual(target.IntValue, target0.IntValue);
            Assert.AreEqual(target.FloatValue, target0.FloatValue);
        }

        [Test]
        public void DeserializeScriptableObjectEmpty()
        {
            var serialize = new SerializerUnityJsonEditor();

            var target = serialize.Deserialize<TestSerializerUnityYamlEditorTarget>(string.Empty, new Context());

            Assert.NotNull(target);
            Assert.IsInstanceOf<TestSerializerUnityYamlEditorTarget>(target);
        }

        [Test]
        public void Copy()
        {
            var serializer = new SerializerUnityJsonEditor();
            var target = new Target();

            Target copy = SerializeUtility.Copy(target, serializer, new Context());

            Assert.NotNull(copy);
            Assert.AreNotEqual(copy, target);
            Assert.AreNotSame(copy, target);
            Assert.AreEqual(copy.BoolValue, target.BoolValue);
            Assert.AreEqual(copy.IntValue, target.IntValue);
            Assert.AreEqual(copy.FloatValue, target.FloatValue);
        }
    }
}
