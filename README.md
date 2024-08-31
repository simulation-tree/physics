# Physics
Abstraction for physics simulation.

### Gravity sources
There are directional and point gravity sources available. Directional gravity applies
to all bodies, while point sources affects ones that are within their defined radius:
```cs
using World world = new();
DirectionalGravity globalGravity = new(world, -Vector3.UnitY);

float radius = 700000f;
Vector3 position = new(0, 0, 0);
PointGravity planetaryGravity = new(world, position, radius);
```

### Physics bodies
Entities that would like to have collisions simulated, or velocities advanced are
considered _bodies_. Where they contain an `IsBody` component and are also transform entities:
```cs
//a ball that will fall
SphereShape ballShape = new(world, 0.5f);
Body ballBody = new(world, ballShape, IsBody.Type.Dynamic);
Transform ballTransform = ballBody;
ballTransform.Position = new(0, 5, 0);
ballBody.Velocity = new(0, 4, 0);

//onto a static floor
CubeShape floorShape = new(world, 0.5f, 0.5f, 0.5f);
Body floorBody = new(world, floorShape, IsBody.Type.Static);
Transform floorTransform = floorBody;
floorTransform.Scale = new(100, 1, 100);

TimeSpan time = default;
while (time.TotalSeconds < 5f)
{
    TimeSpan delta = TimeSpan.FromSeconds(0.2f);
    world.Submit(new PhysicsUpdate(delta));
    world.Poll();
    time += delta;
}
```

### Raycasts
Raycasts are dispatched by submitting a `Raycast` event, which will be fulfilled
at the end of a `PhysicsUpdate` event:
```cs
Raycast raycast = new(new(0, 10, 0), new(0, -1, 0), new(&OnHit));
world.Submit(raycast);

[UnmanagedCallersOnly]
private unsafe static void OnHit(World world, Raycast raycast, void* hitsPointer, int hitCount)
{
    Span<RaycastHit> hits = new(hitsPointer, hitCount);
    foreach (RaycastHit hit in hits)
    {
        uint entityHit = hit.entity;
        Vector3 point = hit.point;
        Vector3 normal = hit.normal;
    }
}
```

> The constructor can accept an arbitrary `identifier` value which can be used to
differentiate between multiple raycasts