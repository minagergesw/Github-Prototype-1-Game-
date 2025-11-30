using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Cinemachine;

using UnityEngine.InputSystem.OnScreen;
using System.Collections.Generic;

public class DisableCameraSwipeOverJoystick : MonoBehaviour
{
    public CinemachineInputAxisController axisController;
    public OnScreenStick leftStick;
    private bool isUsingStick = false;
    private Vector2 stickValue;

    [Tooltip("List of UI RectTransforms (joystick outerRing, buttons...) where touches should NOT trigger camera swipe.")]
    public List<RectTransform> blockingRects = new List<RectTransform>();

    [Tooltip("If your Canvas is Screen Space - Camera or World Space, set the camera here. Leave null for Screen Space - Overlay.")]
    public Camera uiCamera;

    [Header("Settings")]
    [Tooltip("If true, tiny movements of the joystick's dot within this pixel radius will be ignored as 'not using joystick'.")]
    public float deadZonePixels = 6f;

    void Update()
    {
        bool allowCamera = false;

        // No touches: allow camera
        if (Input.touchCount == 0)
        {
#if UNITY_EDITOR
            // also allow mouse drag in editor
            if (Input.GetMouseButton(0))
            {
                Vector2 mp = Input.mousePosition;
                allowCamera = !IsPointOverBlockingRects(mp);
            }
            else
            {
                allowCamera = true;
            }
#else
            allowCamera = true;
#endif
        }
        else
        {
            // If any touch is NOT over a blocking rect => allow camera
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);

                // only consider active touches (Began, Moved, Stationary)
                if (t.phase == TouchPhase.Canceled || t.phase == TouchPhase.Ended)
                    continue;

                Vector2 pos = t.position;

                // if this touch is NOT over any blocking rect => allow camera
                if (!IsPointOverBlockingRects(pos))
                {
                    allowCamera = true;
                    break;
                }
            }
        }

        // apply result
        if (axisController != null)
            axisController.enabled = allowCamera;
    }

    bool IsPointOverBlockingRects(Vector2 screenPoint)
    {
        if (blockingRects == null || blockingRects.Count == 0) return false;

        foreach (var rect in blockingRects)
        {
            if (rect == null) continue;

            // If the rect belongs to a joystick and you want deadzone checking (optional),
            // you can compute anchoredPosition magnitude vs deadZonePixels here.
            // But in most cases RectangleContainsScreenPoint is enough:

            if (RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint, uiCamera))
            {
                // If you want to consider small dot movement inside the joystick as "not using joystick",
                // you can inspect the dot RectTransform separately and measure its anchoredPosition magnitude.
                return true;
            }
        }

        return false;
    }

}
