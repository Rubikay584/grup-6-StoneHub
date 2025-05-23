using UnityEngine;
using UnityEngine.EventSystems;

public class GameHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public float hoverScale = 1.05f;
    public float hoverSpeed = 5f;

    private RectTransform rt;
    private Vector3 originalScale;
    private bool isHovering = false;

    void Awake() {
        rt = GetComponent<RectTransform>();
        originalScale = rt.localScale;
    }

    void Update() {
        Vector3 targetScale = isHovering ? originalScale * hoverScale : originalScale;
        rt.localScale = Vector3.Lerp(rt.localScale, targetScale, Time.deltaTime * hoverSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        isHovering = false;
    }
}