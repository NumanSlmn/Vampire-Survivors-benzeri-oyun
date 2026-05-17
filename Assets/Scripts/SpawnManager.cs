using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Düþman Ayarlarý")]
    public GameObject dusmanPrefab; // Doðacak düþman prefab'ý

    [Header("Zamanlama Ayarlarý")]
    public float baslangicSpawnAraligi = 2.0f; // Ýlk baþta kaç saniyede bir düþman doðsun?
    public float minimumSpawnAraligi = 0.5f;   // Oyun ne kadar hýzlanýrsa hýzlansýn bu sýnýrýn altýna düþmesin
    public float zorlukArtisHizi = 0.05f;      // Her düþman doðduðunda süre ne kadar kýsalsýn?

    [Header("Mesafe Ayarlarý")]
    public float spawnYaricapi = 15f; // Oyuncudan ne kadar uzakta doðsunlar? (Kamera dýþý kalmasý için 15 idealdir)

    private Transform oyuncuTransform;
    private float mevcutSpawnAraligi;
    private float zamanlayici;

    void Start()
    {
        mevcutSpawnAraligi = baslangicSpawnAraligi;

        // Sahnede oyuncuyu bul
        GameObject oyuncu = GameObject.Find("Oyuncu_Kök");
        if (oyuncu != null)
        {
            oyuncuTransform = oyuncu.transform;
        }
    }

    void Update()
    {
        if (oyuncuTransform == null) return;

        zamanlayici += Time.deltaTime;

        // Zamaný geldiyse düþman doður
        if (zamanlayici >= mevcutSpawnAraligi)
        {
            DusmanDogur();
            zamanlayici = 0f;

            // Oyunu yavaþ yavaþ zorlaþtýr (spawn aralýðýný kýsalt)
            if (mevcutSpawnAraligi > minimumSpawnAraligi)
            {
                mevcutSpawnAraligi -= zorlukArtisHizi;
            }
        }
    }

    void DusmanDogur()
    {
        float rastgeleAci = Random.Range(0f, Mathf.PI * 2f);
        float spawnX = oyuncuTransform.position.x + Mathf.Cos(rastgeleAci) * spawnYaricapi;
        float spawnZ = oyuncuTransform.position.z + Mathf.Sin(rastgeleAci) * spawnYaricapi;
        Vector3 spawnPozisyonu = new Vector3(spawnX, oyuncuTransform.position.y, spawnZ);

        if (dusmanPrefab != null)
        {
            GameObject yeniDusman = Instantiate(dusmanPrefab, spawnPozisyonu, Quaternion.identity);

            // SÝHÝRLÝ DOKUNUÞ: Düþmanýn can koduna ulaþýyoruz
            EnemyChase dusmanScript = yeniDusman.GetComponent<EnemyChase>();
            PlayerStats oyuncuStats = oyuncuTransform.GetComponent<PlayerStats>();

            if (dusmanScript != null && oyuncuStats != null)
            {
                // Düþmanýn canýný oyuncunun seviyesine göre artýrýyoruz!
                // Örneðin: Oyuncu 1 Level ise can: 3. Oyuncu 5 Level ise can: 3 + (4 * 2) = 11 olur!
                dusmanScript.can += (oyuncuStats.mevcutSeviye - 1) * 2f;
            }
        }
    }
}