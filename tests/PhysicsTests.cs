using Transforms;
using Types;
using Worlds;
using Worlds.Tests;

namespace Physics.Tests
{
    public abstract class PhysicsTests : WorldTests
    {
        static PhysicsTests()
        {
            MetadataRegistry.Load<PhysicsTypeBank>();
            MetadataRegistry.Load<TransformsTypeBank>();
        }

        protected override Schema CreateSchema()
        {
            Schema schema = base.CreateSchema();
            schema.Load<PhysicsSchemaBank>();
            schema.Load<TransformsSchemaBank>();
            return schema;
        }
    }
}