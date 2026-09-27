using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected float fallSpeed = 2f;
    [SerializeField] protected float destroyBelowY = -11f;
    [SerializeField] protected int hitsToDestroy = 1;

    protected int hitCount = 0;

    protected virtual void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);

        if (transform.position.y < destroyBelowY)
        {
            Destroy(gameObject);
        }
    }

    public virtual void TakeHit()
    {
        hitCount++;
        if (hitCount >= hitsToDestroy)
        {
            OnDestroyedByLaser();
            Destroy(gameObject);
        }
    }

    // Hook for subclasses/other systems (e.g. screen shake) to react to a kill.
    protected virtual void OnDestroyedByLaser()
    {
        GameEvents.RaiseAsteroidDestroyed();
    }

    // Hook for subclasses to customize what happens to THIS enemy when it
    // hits the player. Base behaviour: nothing extra happens to the enemy.
    protected virtual void OnPlayerCollision() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.RaisePlayerDied();
            Destroy(other.gameObject);
            OnPlayerCollision();
        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            TakeHit();
        }
    }
}