// Modified for the VoreWar mercenary-company conversion.
// 2026-11-03: Use the shared blue popup palette without changing dialog actions (spec 17).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    private void Awake()
    {
        UITheme.StylePopup(gameObject);
    }


    public TextMeshProUGUI InfoText;

    public void Update()
    {
        if (!gameObject.activeSelf)
            return;
        if (Input.GetButtonDown("Cancel"))
            DestroySelf();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
