using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using static Unity.Mathematics.math;

public class ShootBallInputSystem : JobComponentSystem
{
    Vector3 MousePosition()
    {
        Vector3 v = Input.mousePosition;
        v.z = 1;
        return Camera.main.ScreenToWorldPoint(v);
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        bool mouseReleased = Input.GetMouseButtonUp(0);
        float3 pos = MousePosition();
        pos = new float3(0, 4, -8);
        float3 angularPower = new float3(20, 0, 0);
        float2 mousePos = new float2(pos.x, pos.z);
        EntityCommandBuffer ecb = new EntityCommandBuffer(Unity.Collections.Allocator.TempJob);

        Entities
            .WithAll<BallCueTag>()
            .ForEach((ref ShootBallComponent sbc, in Entity e, in Translation trans) => 
        {
            if (mouseReleased)
            {
                float3 dir = trans.Value - pos;
                dir.y = 0;
                sbc.Shoot = true;
                sbc.direction = normalize(dir);
                sbc.power = length(dir) * 5;
                sbc.angular = angularPower;

                if(sbc.overrideSettings)
                {
                    sbc.power = length(sbc.direction) * sbc.overridePower;
                    sbc.direction = normalize(sbc.overrideDir);
                    sbc.angular = sbc.overrideAngular;
                }
            }
        }).Run();

        ecb.Playback(EntityManager);
        ecb.Dispose();

        return default;
    }

}
