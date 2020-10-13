using NUnit.Framework;
using UGF.Serialize.Editor.Unity;
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

            string text = serialize.Serialize(target);

            Assert.NotNull(text);
            Assert.Greater(text.Length, 0);
        }

        [Test]
        public void Deserialize()
        {
            var serialize = new SerializerUnityYamlEditor();
            var target = ScriptableObject.CreateInstance<TestSerializerUnityYamlEditorTarget>();

            string text = serialize.Serialize(target);
            var target0 = serialize.Deserialize<TestSerializerUnityYamlEditorTarget>(text);

            Assert.AreEqual(target.BoolValue, target0.BoolValue);
            Assert.AreEqual(target.IntValue, target0.IntValue);
            Assert.AreEqual(target.FloatValue, target0.FloatValue);
        }
    }
}
