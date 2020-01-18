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
        v.y = 2;
        return Camera.main.ScreenToWorldPoint(v);
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        bool mouseReleased = Input.GetMouseButtonUp(0);
        float3 pos = MousePosition();
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
            }
        }).Run();

        ecb.Playback(EntityManager);
        ecb.Dispose();

        return default;
    }

}
