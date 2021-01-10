using UGF.Serialize.Runtime;
using UnityEngine;

namespace UGF.Serialize.Editor.Unity
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Serialize/Serializer Unity Yaml Editor", order = 2000)]
    public class SerializerUnityYamlEditorAsset : SerializerAsset<string>
    {
        protected override ISerializer<string> OnBuildTyped()
        {
            return new SerializerUnityYamlEditor();
        }
    }
}
