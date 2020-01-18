using Unity.Entities;
using Unity.Mathematics;


[GenerateAuthoringComponent]
public struct ShootBallComponent : IComponentData
{
    public bool Shoot;
    public float3 direction;
    public float power;
}
