using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Authoring;

public class ShootBallExecuteSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        Entities
            .WithAll<BallCueTag>()
            .ForEach((ref PhysicsVelocity pv, ref ShootBallComponent sbc)=>
            { 
                if(sbc.Shoot)
                {
                    sbc.Shoot = false;
                    pv.Linear = sbc.direction * sbc.power;
                }
            }).Run();

        return default;
    }
}
