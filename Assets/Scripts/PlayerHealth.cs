using UnityEngine;
using UnityEngine.UI; // RawImage için
using UnityEngine.SceneManagement; // Oyunu yeniden baþlatabilmek için þart

public class PlayerHealth : MonoBehaviour
{
    [Header("Can Ayarlarý")]
    public float maksimumCan = 100f;
    private float mevcutCan;

    [Header("UI Elemanlarý")]
    public RawImage canBariGorseli; // Kýrmýzý Raw Image
    public GameObject gameOverPaneli; // Game Over paneli

    private bool olduMu = false;

    void Start()
    {
        mevcutCan = maksimumCan;
        if (gameOverPaneli != null) gameOverPaneli.SetActive(false);
        CanUI_Guncelle();
    }

    public void HasarAl(float miktar)
    {
        if (olduMu) return;

        mevcutCan -= miktar;
        CanUI_Guncelle();

        if (mevcutCan <= 0)
        {
            Oluþ();
        }
    }

    void CanUI_Guncelle()
    {
        if (canBariGorseli != null)
        {
            float canOrani = Mathf.Clamp01(mevcutCan / maksimumCan);
            // Bizim meþhur scale taktiðiyle can barýný küçültüyoruz
            canBariGorseli.rectTransform.localScale = new Vector3(canOrani, 1f, 1f);
        }
    }

    void Oluþ()
    {
        olduMu = true;
        Debug.LogError("Oyuncu Öldü!");

        if (gameOverPaneli != null) gameOverPaneli.SetActive(true);

        Time.timeScale = 0f; // Oyunu durdur
        Cursor.lockState = CursorLockMode.None; // Fareyi serbest býrak
        Cursor.visible = true;
    }

    // Butona baðlayacaðýmýz yeniden baþlatma fonksiyonu
    public void YenidenBaslat()
    {
        Time.timeScale = 1f; // Zamaný geri aç
        // Mevcut açýk olan sahneyi (SampleScene) sýfýrdan yükler
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Caný ve maksimum can sýnýrýný %10 artýran yeni fonksiyonumuz
    public void CanYenile()
    {
        // Maksimum canýnýn %10'u kadar bir iyileþme miktarý hesaplýyoruz (Örn: 100 canda 10 can verir)
        float iyilesmeMiktari = maksimumCan * 0.10f;

        // Mevcut canýmýza bu miktarý ekliyoruz
        mevcutCan += iyilesmeMiktari;

        // Canýmýzýn maksimum caný aþmasýný engelliyoruz (Taþma korumasý)
        if (mevcutCan > maksimumCan)
        {
            mevcutCan = maksimumCan;
        }

        // Kýrmýzý can barýný (Raw Image) yeni doluluk oranýna göre güncelliyoruz
        void CanUI_Guncelle()
        {
            if (canBariGorseli != null)
            {
                float canOrani = Mathf.Clamp01(mevcutCan / maksimumCan);

                // Can barýnýn ölçeðini deðiþtiriyoruz
                canBariGorseli.rectTransform.localScale = new Vector3(canOrani, 1f, 1f);

                // SÝHÝRLÝ DOKUNUÞ: Oyun dursun ya da durmasýn, Unity'ye bu arayüz elemanýný 
                // ekran kartýna zorla yeniden çizdir diyoruz (Layout Rebuilder).
                LayoutRebuilder.ForceRebuildLayoutImmediate(canBariGorseli.rectTransform);
            }
        }
        }
}