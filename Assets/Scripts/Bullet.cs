using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float mermiHizi = 20f;
    public float yasamSuresi = 3f;
    public float mermiHasari = 1f; // Her merminin vereceði hasar

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.useGravity = false;
        }

        Destroy(gameObject, yasamSuresi);
    }

    // Mermi katý bir cisme çarptýðýnda bu fonksiyon otomatik çalýþýr
    void OnCollisionEnter(Collision collision)
    {
        // Eðer çarptýðýmýz nesnede EnemyChase script'i varsa
        EnemyChase dusman = collision.gameObject.GetComponent<EnemyChase>();

        if (dusman != null)
        {
            dusman.HasarAl(mermiHasari); // Düþmana hasar ver
            Destroy(gameObject); // Mermiyi yok et
        }
    }
}