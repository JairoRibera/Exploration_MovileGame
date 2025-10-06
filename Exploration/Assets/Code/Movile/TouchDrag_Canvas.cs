using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TouchDragUI : MonoBehaviour
{
    public Camera cam; // Normalmente la cámara del Canvas es null (Screen Space - Overlay)
    public Canvas canvas; // Referencia al Canvas
    public GraphicRaycaster raycaster; // Asignar el GraphicRaycaster del Canvas
    private RectTransform objectToDrag;

    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    public float speed = 10f;

    void Start()
    {
        eventSystem = EventSystem.current;
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = touch.position;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            if (results.Count > 0)
            {
                foreach (RaycastResult result in results)
                {
                    RectTransform rt = result.gameObject.GetComponent<RectTransform>();
                    if (rt != null)
                    {
                        objectToDrag = rt;
                        break;
                    }
                }
            }
        }

        if (touch.phase == TouchPhase.Moved && objectToDrag != null)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                touch.position,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : cam,
                out pos
            );

            objectToDrag.anchoredPosition = Vector2.Lerp(objectToDrag.anchoredPosition, pos, speed * Time.deltaTime);
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            objectToDrag = null;
        }
    }
}
