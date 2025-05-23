using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageCarousel : MonoBehaviour {
    public Sprite[] images;
    public float changeInterval = 3f;
    public float transitionDuration = 0.8f;

    private Image imageCurrent;
    private Image imageNext;
    private int currentIndex = 0;

    private void Start() {
        if (images == null || images.Length == 0) {
            Debug.LogWarning("No hay imágenes en el carrusel.");
            return;
        }

        Transform rounded = transform.Find("Rounded");
        Transform imageObj = rounded.Find("Image");
        Transform imageNextObj = rounded.Find("ImageNext");

        imageCurrent = imageObj.GetComponent<Image>();
        imageNext = imageNextObj.GetComponent<Image>();

        imageNext.raycastTarget = false;
        imageNext.preserveAspect = false;

        CanvasGroup cg = imageNext.GetComponent<CanvasGroup>();
        cg.alpha = 0f;

        imageCurrent.sprite = images[0];
        imageNext.sprite = images[0];

        if (images.Length > 1) {
            StartCoroutine(SwapImages());
        }
    }

    IEnumerator SwapImages() {
        while (true) {
            yield return new WaitForSeconds(changeInterval);
            currentIndex = (currentIndex + 1) % images.Length;
            yield return StartCoroutine(AnimateTransition(images[currentIndex]));
        }
    }

    IEnumerator AnimateTransition(Sprite newSprite) {
        imageNext.sprite = newSprite;

        CanvasGroup cg = imageNext.GetComponent<CanvasGroup>();
        cg.alpha = 0f;

        float t = 0f;
        while (t < transitionDuration) {
            t += Time.deltaTime;
            float progress = Mathf.Clamp01(t / transitionDuration);
            float eased = Mathf.Pow(progress, 2); // Ease-in

            cg.alpha = eased;
            imageCurrent.color = new Color(1, 1, 1, 1 - eased);

            yield return null;
        }

        imageCurrent.sprite = newSprite;
        imageCurrent.color = Color.white;
        cg.alpha = 0f;
    }
}
