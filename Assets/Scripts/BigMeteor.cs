
public class BigMeteor : EnemyBase
{
    private void Awake()
    {
        fallSpeed = 0.5f;
        hitsToDestroy = 5;
    }

    protected override void OnDestroyedByLaser()
    {
        base.OnDestroyedByLaser();
        GameEvents.RaiseBigMeteorDestroyed();
    }
}