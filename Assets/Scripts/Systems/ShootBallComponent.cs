using Unity.Entities;
using Unity.Mathematics;


[GenerateAuthoringComponent]
public struct ShootBallComponent : IComponentData
{
    public bool overrideSettings;
    public float3 overrideDir;
    public float3 overrideAngular;
    public float overridePower;

    public bool Shoot;
    public float3 direction;
    public float3 angular;
    public float power;
}
