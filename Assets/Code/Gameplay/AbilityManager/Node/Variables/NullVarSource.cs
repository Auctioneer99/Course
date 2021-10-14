using System;

namespace AbilitySystem.Variables
{
    public sealed class NullVarSource<T> : IIndependentSource<T>
    {
        public static NullVarSource<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NullVarSource<T>();
                }
                return _instance;
            }
        }
        private static NullVarSource<T> _instance;

        public T Value => default(T);

        public object RawValue => Value;

        private NullVarSource()
        {

        }

        public Type GetVarType()
        {
            return typeof(T);
        }
    }
}
