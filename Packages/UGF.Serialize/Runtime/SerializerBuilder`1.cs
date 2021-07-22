using UGF.Builder.Runtime;

namespace UGF.Serialize.Runtime
{
    public abstract class SerializerBuilder<TData> : SerializerBuilder, IBuilder<ISerializer<TData>>
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

        T IBuilder<ISerializer<TData>>.Build<T>()
        {
            return (T)OnBuildTyped();
        }
    }
}
