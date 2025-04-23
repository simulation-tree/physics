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
            MetadataRegistry.Load<PhysicsMetadataBank>();
            MetadataRegistry.Load<TransformsMetadataBank>();
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