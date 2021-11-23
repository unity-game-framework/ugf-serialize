using System;
using System.Threading.Tasks;
using UGF.RuntimeTools.Runtime.Contexts;

namespace UGF.Serialize.Runtime.Container
{
    public abstract class SerializerContainer<TContainer, TTarget, TData> : SerializerAsync<TData> where TContainer : class, new()
    {
        public ISerializerAsync<TData> ContainerSerializer { get; }

        protected SerializerContainer(ISerializerAsync<TData> containerSerializer)
        {
            ContainerSerializer = containerSerializer ?? throw new ArgumentNullException(nameof(containerSerializer));
        }

        protected override object OnSerialize(object target, IContext context)
        {
            TContainer container = OnCreateContainer((TTarget)target, context);
            TData data = ContainerSerializer.Serialize(container, context);

            return data;
        }

        protected override object OnDeserialize(Type targetType, TData data, IContext context)
        {
            var container = ContainerSerializer.Deserialize<TContainer>(data, context);
            TTarget target = OnCreateTarget(container, context);

            return target;
        }

        protected override async Task<TData> OnSerializeAsync(object target, IContext context)
        {
            TContainer container = OnCreateContainer((TTarget)target, context);
            TData data = await ContainerSerializer.SerializeAsync(container, context);

            return data;
        }

        protected override async Task<object> OnDeserializeAsync(Type targetType, TData data, IContext context)
        {
            var container = await ContainerSerializer.DeserializeAsync<TContainer>(data, context);
            TTarget target = OnCreateTarget(container, context);

            return target;
        }

        protected abstract TContainer OnCreateContainer(TTarget target, IContext context);
        protected abstract TTarget OnCreateTarget(TContainer container, IContext context);
    }
}
