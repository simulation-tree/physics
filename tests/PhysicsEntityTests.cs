using Shapes.Types;
using Worlds;

namespace Physics.Tests
{
    public class PhysicsEntityTests : PhysicsTests
    {
        [Test]
        public void VerifyBodyIsItself()
        {
            using World world = CreateWorld();
            Body body = new(world, new SphereShape(0.5f), BodyType.Dynamic);
            Assert.That(body.IsCompliant, Is.True);
        }
    }
}