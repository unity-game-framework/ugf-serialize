namespace UGF.Serialize.Runtime.Tests
{
    public class TestTypes
    {
        [SerializeType(1)]
        public class Target1
        {
        }

        [SerializeType(0.5F)]
        public class Target2
        {
        }

        [SerializeType(10L)]
        public class Target3
        {
        }

        [SerializeType("test")]
        public class Target4
        {
        }

        [SerializeType]
        public class Target5
        {
        }
    }
}
