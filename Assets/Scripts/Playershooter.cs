using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float cooldownSeconds = 1f;
    [SerializeField] private Vector3 muzzleOffset = new Vector3(0, 1, 0);

    private bool canShoot = true;

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.spaceKey.wasPressedThisFrame && canShoot)
        {
            Fire();
        }
    }

    private void Fire()
    {
        Instantiate(laserPrefab, transform.position + muzzleOffset, Quaternion.identity);
        canShoot = false;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownSeconds);
        canShoot = true;
    }
}
