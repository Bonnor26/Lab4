using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float destroyAboveY = 11f;

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (transform.position.y > destroyAboveY)
        {
            Destroy(gameObject);
        }
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}