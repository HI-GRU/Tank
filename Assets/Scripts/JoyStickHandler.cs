using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("JoyStick")]
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    private float handleRange;
    private bool isDragging;
    private Vector2 touchStartPos;
    private Vector2 backgroundStartPos;
    private Vector2 inputDir;
    public Vector2 GetInputDir()
    {
        return isDragging ? inputDir : Vector2.zero;
    }

    private void Start()
    {
        background.gameObject.SetActive(false);
        handle.gameObject.SetActive(false);
        handleRange = background.sizeDelta.x / 2F;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchStartPos = eventData.position;
        background.position = touchStartPos;
        handle.position = touchStartPos;
        backgroundStartPos = background.anchoredPosition;

        Change(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        Vector2 dir = eventData.position - touchStartPos;
        if (dir.magnitude > handleRange) dir = dir.normalized * handleRange;

        handle.anchoredPosition = backgroundStartPos + dir;
        inputDir = dir.normalized;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Change(false);
    }

    private void Change(bool bo)
    {
        background.gameObject.SetActive(bo);
        handle.gameObject.SetActive(bo);
        isDragging = bo;
    }
}