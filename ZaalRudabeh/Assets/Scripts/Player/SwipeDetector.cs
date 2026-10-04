using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetector : MonoBehaviour
{
    Vector2 startPosition;
    bool isTouching;
    bool isHolding;
    private float holdTimer;
    public PlayerController player;
    private float swipeThreshold = 150;

    void Update()
    {
        if (Touchscreen.current == null)
            return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.wasPressedThisFrame)
        {
            startPosition = touch.position.ReadValue();
            isTouching = true;
        }

        if (isTouching && touch.press.isPressed)
        {
            holdTimer += Time.deltaTime;

            Vector2 currentPosition = touch.position.ReadValue();

            Vector2 direction = currentPosition - startPosition;

            if (direction.magnitude >= swipeThreshold)
            {
                holdTimer = 0;
                isTouching = false;
                isHolding = false;
                DetectDirection(direction);
            }
        }

        if (touch.press.wasReleasedThisFrame)
        {
            holdTimer = 0;
            isTouching = false;
            isHolding = false;
        }
    }

    void DetectDirection(Vector2 direction)
    {
        player.Paddle(direction);

        print(direction.x + "-----" + direction.y);
    }
}
