using UnityEngine;

public class XPMagnet : MonoBehaviour
{
    public float cekimHizi = 10f;
    private Transform oyuncu;
    private bool cekiliyorMu = false;

    void Start()
    {
        oyuncu = GameObject.Find("Oyuncu_Kök").transform;
    }

    void Update()
    {
        // Oyuncu ile kristal arasýndaki mesafeyi ölç
        float mesafe = Vector3.Distance(transform.position, oyuncu.position);

        // Eðer oyuncu 5 metre yakýna gelirse mýknatýs çalýþsýn
        if (mesafe < 5f) cekiliyorMu = true;

        if (cekiliyorMu)
        {
            transform.position = Vector3.MoveTowards(transform.position, oyuncu.position, cekimHizi * Time.deltaTime);
        }
    }
}