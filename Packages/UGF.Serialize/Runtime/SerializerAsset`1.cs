using System;

namespace UGF.Serialize.Runtime
{
    public abstract class SerializerAsset<TData> : SerializerAsset
    {
        public override Type DataType { get; } = typeof(TData);

        public new ISerializer<TData> Build()
        {
            return OnBuildTyped();
        }

        protected override ISerializer OnBuild()
        {
            return OnBuildTyped();
        }

        protected abstract ISerializer<TData> OnBuildTyped();
    }
}
