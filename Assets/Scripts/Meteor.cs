
public class Meteor : EnemyBase
{
    private void Awake()
    {
        fallSpeed = 2f;
        hitsToDestroy = 1;
    }

    protected override void OnDestroyedByLaser()
    {
        base.OnDestroyedByLaser();
        GameEvents.RaiseRegularMeteorDestroyed();
    }

    protected override void OnPlayerCollision()
    {
        Destroy(gameObject);
    }
}

