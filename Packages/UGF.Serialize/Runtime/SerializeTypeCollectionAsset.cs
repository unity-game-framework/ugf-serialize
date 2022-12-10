using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.Serialize.Runtime
{
    public abstract class SerializeTypeCollectionAsset : ScriptableObject
    {
        public void GetTypes(IDictionary<object, Type> types)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));

            OnGetTypes(types);
        }

        protected abstract void OnGetTypes(IDictionary<object, Type> types);
    }
}
