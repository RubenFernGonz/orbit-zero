using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalPos;

    void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;
    }

    public void Shake(float strength, float duration)
    {
        StartCoroutine(ShakeRoutine(strength, duration));
    }

    private IEnumerator ShakeRoutine(float strength, float duration)
{
    float elapsed = 0f;

    while (elapsed < duration)
    {
        Vector3 offset = new Vector3(
            Random.Range(-strength, strength),
            0f, // no mover en Y
            Random.Range(-strength, strength)
        );

        transform.localPosition = originalPos + offset;

        elapsed += Time.deltaTime;
        yield return null;
    }

    transform.localPosition = originalPos;
}

}
