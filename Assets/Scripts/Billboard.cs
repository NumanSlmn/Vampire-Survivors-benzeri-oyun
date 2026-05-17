using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform anaKameraTransform;

    void Start()
    {
        if (Camera.main != null)
        {
            anaKameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if (anaKameraTransform != null)
        {
            // Can barýnýn rotasyonunu her zaman kameranýn rotasyonuna eþitliyoruz
            transform.LookAt(transform.position + anaKameraTransform.forward);
        }
    }
}