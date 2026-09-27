using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float horizontalScreenLimit = 10f;
    [SerializeField] private float verticalScreenLimit = 6f;

    private void Update()
    {
        Vector2 input = ReadMoveInput();
        transform.Translate(new Vector3(input.x, input.y, 0f) * speed * Time.deltaTime);
        WrapAroundScreen();
    }

    private Vector2 ReadMoveInput()
    {
        Vector2 input = Vector2.zero;
        var keyboard = Keyboard.current;
        if (keyboard == null) return input;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) input.x -= 1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) input.x += 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) input.y -= 1f;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) input.y += 1f;

        return input;
    }

    private void WrapAroundScreen()
    {
        Vector3 pos = transform.position;
        if (pos.x > horizontalScreenLimit || pos.x <= -horizontalScreenLimit)
            pos.x *= -1f;
        if (pos.y > verticalScreenLimit || pos.y <= -verticalScreenLimit)
            pos.y *= -1f;
        transform.position = pos;
    }
}