namespace UGF.Serialize.Runtime
{
    public abstract class SerializerAsset<TData> : SerializerAsset
    {
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
