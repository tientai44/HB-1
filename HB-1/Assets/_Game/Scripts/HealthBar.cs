using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset;
    float hp;
    float maxHp;

    Transform target;
    // Update is called once per frame
    void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount,hp/maxHp,Time.deltaTime*5f);
        transform.position = offset+target.position;
    }

    public void OnInit(float maxhp,Transform target)
    {
        this.maxHp = maxhp;
        hp= maxhp;
        imageFill.fillAmount=1;
        this.target = target;
    }

    public void SetNewHP(float hp)
    {
        this.hp = hp;
        //imageFill.fillAmount=hp/maxHp;
    }
}
