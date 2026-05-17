using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Gereksinimler")]
    public GameObject mermiPrefab; // Mermi prefab'ý
    public Transform atesNoktasý;  // Namlu ucu

    [Header("Otomatik Atýþ Ayarlarý")]
    public float atesAraligi = 0.5f;   // Kaç saniyede bir otomatik ateþ etsin? (Örn: Saniyede 2 mermi)
    private float atesZamanlayicisi = 0f;

    [Header("Geliþtirme Ayarlarý (Upgrade için)")]
    public float mermiHiziArtisi = 0f;
    public float ekstraHasar = 0f;

    void Update()
    {
        // Zamanlayýcýyý her karede geçen süre kadar artýrýyoruz
        atesZamanlayicisi += Time.deltaTime;

        // Süre dolduysa otomatik ateþ et ve zamanlayýcýyý sýfýrla
        if (atesZamanlayicisi >= atesAraligi)
        {
            AtesEt();
            atesZamanlayicisi = 0f;
        }
    }

    void AtesEt()
    {
        if (mermiPrefab != null && atesNoktasý != null)
        {
            // Mermiyi doður (Karakterin baktýðý/farenin yönündeki rotasyonla doðuyor)
            GameObject yeniMermi = Instantiate(mermiPrefab, atesNoktasý.position, atesNoktasý.rotation);
            yeniMermi.transform.parent = null;

            Rigidbody mermiRb = yeniMermi.GetComponent<Rigidbody>();
            if (mermiRb != null)
            {
                mermiRb.linearVelocity = Vector3.zero;

                Bullet mermiScript = yeniMermi.GetComponent<Bullet>();
                if (mermiScript != null)
                {
                    if (mermiScript.mermiHizi <= 0f)
                    {
                        mermiScript.mermiHizi = 20f;
                    }

                    float toplamHiz = mermiScript.mermiHizi + mermiHiziArtisi;

                    // Mermiyi karakterin ileri (forward) yönüne, yani farenin olduðu tarafa fýrlatýyoruz
                    mermiRb.linearVelocity = atesNoktasý.forward * toplamHiz;

                    mermiScript.mermiHasari += ekstraHasar;
                }
            }
        }
    }
}