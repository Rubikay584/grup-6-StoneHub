using UnityEngine;

public class GameAppearEffect : MonoBehaviour {
    public float appearDuration = 0.6f;
    public float delay = 0f;

    private RectTransform rt;

    void Start() {
        rt = GetComponent<RectTransform>();
        StartCoroutine(AppearAnimation());
    }

    System.Collections.IEnumerator AppearAnimation() {
        yield return new WaitForSeconds(delay);

        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = Vector3.one;

        float time = 0f;

        rt.localScale = initialScale;

        while (time < appearDuration) {
            time += Time.deltaTime;
            float progress = time / appearDuration;

            float eased = EaseOutBack(progress);
            rt.localScale = Vector3.LerpUnclamped(initialScale, targetScale, eased);
            yield return null;
        }

        rt.localScale = targetScale;
    }

    float EaseOutBack(float t) {
        float c1 = 1.1234f;
        float c3 = c1 + 1;

        return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
    }
}