using System;


public static class GameEvents
{
    public static event Action PlayerDied;
    public static event Action RegularMeteorDestroyed;
    public static event Action AsteroidDestroyed;
    public static event Action BigMeteorSpawned;

    public static void RaisePlayerDied() => PlayerDied?.Invoke();
    public static void RaiseRegularMeteorDestroyed() => RegularMeteorDestroyed?.Invoke();
    public static void RaiseAsteroidDestroyed() => AsteroidDestroyed?.Invoke();
    public static void RaiseBigMeteorSpawned() => BigMeteorSpawned?.Invoke();
}
