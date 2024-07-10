# Physics
Simulates physics using fast abstractions for realtime speed.

### Dependencies
* [Simulation](https://github.com/game-simulations/simulation)
* [Transforms](https://github.com/game-simulations/transforms)

### Behaviour
1. Entities with an `IsBody` component are automatically going to have an `IsTransform` component as well
2. Only entities with an `IsBody` component will have collisions and gravity simulated for
3. `Mass` component is optional and defaults to `1`, but if set to infinity then the body is static and immovable
4. Gravity source entities are expected to be transforms, because the directional source must be oriented, and the point must be positioned
   
### Gravity
For gravity, there is directional, and point gravity sources available. Directional gravity
behaves globally and applies to all bodies. Point gravity affects ones that are within
the defined radius.
```cs
EntityID globalGravity = world.CreateEntity();
world.AddComponent(globalGravity, new DirectionalGravity(-9.81f));
world.AddComponent(globalGravity, new IsTransform());
world.AddComponent(globalGravity, new EulerAngles(Vector3.UnitY));

float radius = 700000f;
EntityID planetaryGravity = world.CreateEntity();
world.AddComponent(planetaryGravity, new IsTransform());
world.AddComponent(planetaryGravity, new Position(0, 0, 0));
world.AddComponent(planetaryGravity, new PointGravity(-9.81f, radius));
```

### Collisions
```cs
EntityID cube = world.CreateEntity();
world.AddComponent(cube, new IsBody());
world.AddComponent(cube, new IsCube());
world.AddComponent(cube, new Position(0, 5, 0));

EntityID floor = world.CreateEntity();
world.AddComponent(floor, new IsBody());
world.AddComponent(floor, new IsCube());
world.AddComponent(floor, new Position(0, -1, 0));
world.AddComponent(floor, new Scale(20, 1, 20));
world.AddComponent(floor, new Mass(Math.PositiveInfinity));

float time = 0f;
while (time < 5f)
{
    world.Submit(new PhysicsUpdate());
    world.Poll();
    time += 0.2f;
}
```
### Line tests
```cs
```
