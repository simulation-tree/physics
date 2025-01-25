using Physics.Components;
using Worlds;

namespace Physics.Tests
{
    public class PhysicsEntityTests : PhysicsTests
    {
        [Test]
        public void VerifyBodyIsItself()
        {
            using World world = CreateWorld();
            Body body = new(world, Shape.Create(new SphereShape(0.5f)), IsBody.Type.Dynamic);
            Assert.That(body.Is(), Is.True);
        }
    }
}