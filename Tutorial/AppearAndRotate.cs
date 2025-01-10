using UnityEngine;

public class AppearAndRotate : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 2f;

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectRenderer.enabled = false;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            objectRenderer.enabled = true;

            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-direction.x, 0, -direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            if (objectRenderer.enabled)
            {
                objectRenderer.enabled = false;
            }
        }
    }
}
