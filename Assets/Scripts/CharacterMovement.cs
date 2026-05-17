using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float hiz = 5f;
    private Vector3 hareketYonu;
    private Rigidbody rb;

    [Header("Rotasyon Ayarlarý")]
    public float donusHizi = 15f;
    public LayerMask zeminKatmani; // Sadece zemini algýlamak için maske
    private Camera anaKamera;


    public Transform kamera;
    Vector3 fark;
    public Transform eleman;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anaKamera = Camera.main;
        rb.freezeRotation = true;
        fark = kamera.position - eleman.position;
    }

    void Update()
    {
        // Girdileri al
        hareketYonu = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        // Fare Takibi
        Ray isin = anaKamera.ScreenPointToRay(Input.mousePosition);

        // Physics.Raycast'e en sona 'zeminKatmani' parametresini ekledik!
        if (Physics.Raycast(isin, out RaycastHit hit, Mathf.Infinity, zeminKatmani))
        {
            Vector3 hedefNokta = hit.point;
            hedefNokta.y = transform.position.y; // Karakterin dik durmasýný saðlar

            Vector3 yon = hedefNokta - transform.position;

            if (yon.magnitude > 0.2f) // Karakterin tam merkezindeki minik ölü nokta
            {
                Quaternion hedefRotasyon = Quaternion.LookRotation(yon);
                transform.rotation = Quaternion.Slerp(transform.rotation, hedefRotasyon, donusHizi * Time.deltaTime);
            }
        }
        kamera.position = eleman.position + fark;
    }

    void FixedUpdate()
    {
        Vector3 yeniHiz = hareketYonu * hiz;
        yeniHiz.y = rb.linearVelocity.y;
        rb.linearVelocity = yeniHiz;
    }
}