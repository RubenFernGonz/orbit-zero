using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject obstacle;
    [SerializeField] private Transform PlayerPos;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject indicatorPrefab;

    [Header("Rubble (escombros tras impacto)")]
    [SerializeField] private GameObject rubblePrefab; // ← NUEVO

    [Header("Meteor Settings")]
    public float spawnHeight = 20f;
    public float travelSpeed = 15f;
    public float targetOffsetRadius = 4f;
    public float explosionHeight = 1.5f;
    public float warningTime = 1.5f; 

    private bool canLaunch = true;

    void Update()
    {
        if (canLaunch)
        {
            Launch();
            StartCoroutine(Timer());
        }
    }

    private void Launch()
    {
        Vector3 offset = Random.insideUnitSphere * targetOffsetRadius;
        offset.y = 0;

        Vector3 targetGroundPos = PlayerPos.position + offset;
        Vector3 spawnPos = targetGroundPos + Vector3.up * spawnHeight;

        // Crear indicador
        GameObject indicator = Instantiate(
            indicatorPrefab,
            targetGroundPos + Vector3.up * 0.05f,
            Quaternion.identity
        );

        StartCoroutine(SpawnMeteorDelayed(spawnPos, targetGroundPos, indicator));
    }

    IEnumerator SpawnMeteorDelayed(Vector3 spawnPos, Vector3 target, GameObject indicator)
    {
        yield return new WaitForSeconds(warningTime);

        GameObject meteor = Instantiate(obstacle, spawnPos, Quaternion.identity);
        StartCoroutine(FallDiagonal(meteor, target, indicator));
    }

    IEnumerator FallDiagonal(GameObject meteor, Vector3 target, GameObject indicator)
    {
        Rigidbody rb = meteor.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        while (meteor != null)
        {
            Vector3 dir = (target - meteor.transform.position).normalized;

            if (dir != Vector3.zero)
                meteor.transform.rotation = Quaternion.LookRotation(dir);

            meteor.transform.position += dir * travelSpeed * Time.deltaTime;

            if (meteor.transform.position.y <= explosionHeight)
            {
                Destroy(indicator);

                Instantiate(explosionPrefab, meteor.transform.position, Quaternion.identity);

                CameraShake.Instance.Shake(0.001f, 0.25f);

                CreateRubble(meteor.transform.position);

                Destroy(meteor);
                yield break;
            }

            yield return null;
        }
    }

    private void CreateRubble(Vector3 pos)
    {
        if (rubblePrefab != null)
        {
            // Crear tu prefab de escombros
            GameObject rubble = Instantiate(rubblePrefab, pos, Quaternion.identity);

            // Asegurarse de que tenga colisión estática
            Rigidbody rb = rubble.GetComponent<Rigidbody>();
            if (rb == null)
                rb = rubble.AddComponent<Rigidbody>();

            rb.isKinematic = true;

            // Desaparece a los 5 segundos
          //  Destroy(rubble, 5f);
        }
        else
        {
            Debug.LogWarning("No has asignado el prefab de rubble en el inspector.");
        }
    }

    IEnumerator Timer()
    {
        canLaunch = false;
        yield return new WaitForSeconds(3f);
        canLaunch = true;
    }
}
