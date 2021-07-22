using System;
using NUnit.Framework;
using UGF.RuntimeTools.Runtime.Contexts;
using UGF.Serialize.Editor.Unity;
using UGF.Serialize.Runtime;
using UnityEngine;

namespace UGF.Serialize.Editor.Tests.Unity
{
    public class TestSerializerUnityYamlEditor
    {
        [Test]
        public void Serialize()
        {
            var serialize = new SerializerUnityYamlEditor();
            var target = ScriptableObject.CreateInstance<TestSerializerUnityYamlEditorTarget>();

            string text = serialize.Serialize(target, new Context());

            Assert.NotNull(text);
            Assert.Greater(text.Length, 0);
        }

        [Test]
        public void Deserialize()
        {
            var serialize = new SerializerUnityYamlEditor();
            var target = ScriptableObject.CreateInstance<TestSerializerUnityYamlEditorTarget>();

            string text = serialize.Serialize(target, new Context());
            var target0 = serialize.Deserialize<TestSerializerUnityYamlEditorTarget>(text, new Context());

            Assert.AreEqual(target.BoolValue, target0.BoolValue);
            Assert.AreEqual(target.IntValue, target0.IntValue);
            Assert.AreEqual(target.FloatValue, target0.FloatValue);
        }

        [Test]
        public void DeserializeEmpty()
        {
            var serialize = new SerializerUnityYamlEditor();

            Assert.Throws<NotSupportedException>(() => serialize.Deserialize<TestSerializerUnityYamlEditorTarget>(string.Empty, new Context()));
        }

        [Test]
        public void Copy()
        {
            var serializer = new SerializerUnityYamlEditor();
            var target = ScriptableObject.CreateInstance<TestSerializerUnityYamlEditorTarget>();

            TestSerializerUnityYamlEditorTarget copy = SerializeUtility.Copy(target, serializer, new Context());

            Assert.NotNull(copy);
            Assert.AreNotEqual(copy, target);
            Assert.AreNotSame(copy, target);
            Assert.AreEqual(copy.BoolValue, target.BoolValue);
            Assert.AreEqual(copy.IntValue, target.IntValue);
            Assert.AreEqual(copy.FloatValue, target.FloatValue);
        }
    }
}
