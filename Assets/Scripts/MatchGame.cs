using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MatchGame : MonoBehaviour
{
    public Button[] buttons; // جميع الأزرار
    public AudioSource matchSound, failSound; // أصوات التأثيرات
    public ParticleSystem matchEffect; // تأثير عند التطابق
    private Button firstSelected, secondSelected; // الأزرار المختارة
    private int matchedPairs = 0; // عداد الأزواج المتطابقة
    public float revealTime = 2f; // مدة ظهور الأزرار في البداية

    void Start()
    {
        // في البداية نظهر الأزرار للحظة ثم نخفيها
        StartCoroutine(InitialReveal());

        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => SelectButton(btn));
        }
    }

    IEnumerator InitialReveal()
    {
        yield return new WaitForSeconds(revealTime); // الانتظار قليلاً قبل الإخفاء

        foreach (Button btn in buttons)
        {
            btn.image.enabled = false; // إخفاء صور الأزرار بعد المدة
        }
    }

    void SelectButton(Button btn)
    {
        if (btn.image.enabled) return; // منع الضغط على الأزرار المكشوفة بالفعل

        btn.image.enabled = true; // إظهار الصورة عند الضغط

        if (firstSelected == null)
        {
            firstSelected = btn;
        }
        else if (secondSelected == null && btn != firstSelected)
        {
            secondSelected = btn;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f); // انتظار قبل التحقق

        if (firstSelected.image.sprite == secondSelected.image.sprite)
        {
            matchSound.Play();
            Instantiate(matchEffect, firstSelected.transform.position, Quaternion.identity);
            Instantiate(matchEffect, secondSelected.transform.position, Quaternion.identity);

            firstSelected.interactable = false;
            secondSelected.interactable = false;

            matchedPairs++; // زيادة عدد الأزواج المتطابقة

            if (matchedPairs == buttons.Length / 2)
            {
                Debug.Log("You Win! 🎉");
                // استدعاء شاشة الفوز هنا
            }
        }
        else
        {
            failSound.Play();
            yield return new WaitForSeconds(0.5f); // إتاحة وقت لرؤية الزرين
            firstSelected.image.enabled = false; // إخفاء الزر الأول
            secondSelected.image.enabled = false; // إخفاء الزر الثاني
        }

        firstSelected = null;
        secondSelected = null;
    }
}
