using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    public void OnInit(float damage)
    {
        Debug.Log("CombatText");
        hpText.text = damage.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
