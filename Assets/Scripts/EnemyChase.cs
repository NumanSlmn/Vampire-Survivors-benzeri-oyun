using UnityEngine;
using UnityEngine.UI; // UI elementlerini kontrol etmek için ekledik

public class EnemyChase : MonoBehaviour
{
    [Header("XP Ayarlarý")]
    public GameObject xpKristalPrefab; // Klasördeki XPKristali buraya gelecek
    public float dusmanHizi = 3f;
    public float can = 3f;
    private float maksimumCan; // Düþmanýn ilk canýný hafýzada tutacaðýz

    [Header("UI Ayarlarý")]
    public RawImage canBariGorseli; // Bizim yeþil CanBari resmi buraya gelecek

    private Transform oyuncuHedef;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        maksimumCan = can; // Baþlangýçtaki caný maksimum can olarak kaydet

        GameObject oyuncu = GameObject.Find("Oyuncu_Kök");
        if (oyuncu != null) oyuncuHedef = oyuncu.transform;
    }

    void FixedUpdate()
    {
        if (oyuncuHedef != null && rb != null)
        {
            Vector3 yon = (oyuncuHedef.position - transform.position).normalized;
            yon.y = 0f;
            Vector3 yeniHiz = yon * dusmanHizi;
            yeniHiz.y = rb.linearVelocity.y;
            rb.linearVelocity = yeniHiz;

            if (yon != Vector3.zero) transform.rotation = Quaternion.LookRotation(yon);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        MermiKontrol(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        MermiKontrol(other.gameObject);
    }

    void MermiKontrol(GameObject carpanNesne)
    {
        if (carpanNesne.CompareTag("Mermi") || carpanNesne.name.Contains("Mermi") || carpanNesne.GetComponent<Bullet>() != null)
        {
            Destroy(carpanNesne);
            HasarAl(1f);
        }
    }

    public void HasarAl(float miktar)
    {
        can -= miktar;

        if (canBariGorseli != null)
        {
            float canOrani = can / maksimumCan; // Not: Kodunda 'maksimumCan' veya 'maximumCan' hangisiyse onu yaz reis
            canBariGorseli.rectTransform.localScale = new Vector3(canOrani, 1f, 1f);
        }

        if (can <= 0)
        {
            // Ölmeden hemen önce düþmanýn öldüðü pozisyonda kristal yaratýyoruz
            if (xpKristalPrefab != null)
            {
                Instantiate(xpKristalPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
    // Düþman oyuncuya fiziken temas ettiðinde tetiklenir
    void OnCollisionStay(Collision collision)
    {
        // Temas ettiðim nesne oyuncu mu?
        if (collision.gameObject.name == "Oyuncu_Kök" || collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth oyuncuCan = collision.gameObject.GetComponent<PlayerHealth>();
            if (oyuncuCan != null)
            {
                // Her fizik karesinde (FixedUpdate gibi) oyuncuya 0.5 hasar verir. 
                // Bu deðeri isteðine göre ayarlayabilirsin reis.
                oyuncuCan.HasarAl(0.5f);
            }
        }
    }
}