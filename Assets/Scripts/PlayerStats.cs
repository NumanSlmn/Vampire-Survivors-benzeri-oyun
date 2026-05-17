using UnityEngine;
using UnityEngine.UI; // RawImage'ý kodun tanýmasý için bu þart!
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public UpgradeManager upgradeManager;
    [Header("Seviye Verileri")]
    public int mevcutSeviye = 1;
    public float mevcutXP = 0f;
    public float hedefXP = 10f;

    [Header("UI Elemanlarý (Raw Image)")]
    public RawImage xpBariGorseli; // Mavi renkli Raw Image buraya gelecek
    public TextMeshProUGUI levelMetni; // Level yazýsý

    void Start()
    {
        UI_Guncelle();
    }

    public void XP_Kazan(float miktar)
    {
        mevcutXP += miktar;

        if (mevcutXP >= hedefXP)
        {
            SeviyeAtla();
        }

        UI_Guncelle();
    }

    void SeviyeAtla()
    {
        mevcutXP -= hedefXP;
        mevcutSeviye++;
        hedefXP = Mathf.Round(hedefXP * 1.5f);

        Debug.LogWarning("Level Atlandý!");

        // BURAYA EKLEDÝK: Level atlayýnca menüyü aç diyoruz
        if (upgradeManager != null)
        {
            upgradeManager.MenuyuAc();
        }
    }

    void UI_Guncelle()
    {
        if (xpBariGorseli != null)
        {
            // Barýn doluluk oranýný (0 ile 1 arasý) hesapla
            float xpOrani = mevcutXP / hedefXP;

            // Barý Source Image olmadan, geniþliðini (Scale X) deðiþtirerek pürüzsüzce büyütüp küçültüyoruz
            xpBariGorseli.rectTransform.localScale = new Vector3(xpOrani, 1f, 1f);
        }

        if (levelMetni != null)
        {
            levelMetni.text = "Level: " + mevcutSeviye;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("XPKristali"))
        {
            XP_Kazan(1f);
            Destroy(other.gameObject);
        }
    }
}