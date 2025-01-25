using Types;
using Worlds;
using Worlds.Tests;

namespace Physics.Tests
{
    public abstract class PhysicsTests : WorldTests
    {
        static PhysicsTests()
        {
            TypeRegistry.Load<Physics.TypeBank>();
            TypeRegistry.Load<Transforms.TypeBank>();
        }

        protected override Schema CreateSchema()
        {
            Schema schema = base.CreateSchema();
            schema.Load<Physics.SchemaBank>();
            schema.Load<Transforms.SchemaBank>();
            return schema;
        }
    }
}