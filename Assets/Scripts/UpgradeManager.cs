using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("UI Panelleri")]
    public GameObject upgradePaneli;

    [Header("Oyuncu Referanslarý (Editörden Baðla)")]
    public PlayerHealth oyuncuCan;

    private CharacterMovement oyuncuHareket;
    private PlayerShooting oyuncuAtes;

    void Start()
    {
        // Hareket ve Ateþ scriptlerini otomatik bulalým
        oyuncuHareket = FindFirstObjectByType<CharacterMovement>();
        oyuncuAtes = FindFirstObjectByType<PlayerShooting>();

        if (upgradePaneli != null) upgradePaneli.SetActive(false);
    }

    public void MenuyuAc()
    {
        if (upgradePaneli != null) upgradePaneli.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void MenuyuKapat()
    {
        if (upgradePaneli != null) upgradePaneli.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Upgrade_OyuncuHizi()
    {
        if (oyuncuHareket != null) oyuncuHareket.hiz += 1.5f;
        MenuyuKapat();
    }

    public void Upgrade_MermiHizi()
    {
        if (oyuncuAtes != null) oyuncuAtes.mermiHiziArtisi += 5f;
        MenuyuKapat();
    }

    public void Upgrade_Hasar()
    {
        if (oyuncuAtes != null) oyuncuAtes.ekstraHasar += 0.5f;
        MenuyuKapat();
    }

    public void Upgrade_AtisHizi()
    {
        if (oyuncuAtes != null)
        {
            oyuncuAtes.atesAraligi *= 0.8f;
            if (oyuncuAtes.atesAraligi < 0.05f) oyuncuAtes.atesAraligi = 0.05f;
        }
        MenuyuKapat();
    }

    // ÝÞTE BUTONUN ARADIÐI O SÝHÝRLÝ FONKSÝYON
    public void Upgrade_CanYenile()
    {
        if (oyuncuCan != null)
        {
            oyuncuCan.CanYenile();
        }
        MenuyuKapat();
    }
}