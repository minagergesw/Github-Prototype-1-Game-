using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Cinemachine; // تأكد من وجود Using ده (اسم النيمسبيس فى Cinemachine 3)

public class CameraSwipePanel_OrbitalFollow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Tooltip("سرعة السوايب - جرب قيم صغيرة مثل 0.05 أو 0.1")]
    public float swipeSpeed = 0.12f;

    // لو عندك أكتر من OrbitalFollow خليه مربوط يدوياً، وإلا هيجيب الأول منه فى المشهد
    public CinemachineOrbitalFollow orbitalFollow;

    private Vector2 lastPos;
    private bool dragging;

    void Start()
    {
        if (orbitalFollow == null)
        {
            // FindFirstObjectByType متاح فى Unity 2023+، لو مش شغال عندك استبدله بـ FindObjectOfType
            orbitalFollow = Object.FindObjectOfType<CinemachineOrbitalFollow>();
            if (orbitalFollow == null)
                Debug.LogWarning("No CinemachineOrbitalFollow found in scene. Link it manually.", this);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
        lastPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging || orbitalFollow == null) return;

        Vector2 cur = eventData.position;
        Vector2 delta = cur - lastPos;
        lastPos = cur;

        // اقرأ الحقول كـ struct، عدّل Value، واكتبهم تانى
        var h = orbitalFollow.HorizontalAxis; // InputAxis struct
        var v = orbitalFollow.VerticalAxis;

        // دلتا.x يحرك الـ horizontal (دوران يمين/شمال)
        // دلتا.y يحرك الـ vertical (لف/رفع الكاميرا)
        h.Value += delta.x * swipeSpeed;
        v.Value -= delta.y * swipeSpeed; // سالب لو عايز عكس الاتجاه الرأسي — عدّل لو لازم

        // لو في حدود للرنج، InputAxis سيقوم بالـ clamping لو معمول فى الإنسبيكتور
        orbitalFollow.HorizontalAxis = h;
        orbitalFollow.VerticalAxis = v;
    }
}
