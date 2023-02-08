using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiPool : GOSingleton<KunaiPool>
{
    private GameObject kunaiPrefab;
    private List<GameObject> pools = new List<GameObject>();

    public GameObject GetKunai()
    {
        if(pools.Count == 0)
        {
            GameObject kunai = Instantiate(kunaiPrefab);    
            pools.Add(kunai);
            kunai.SetActive(true);
            return kunai;
        }
        else
        {
            GameObject kunai = pools[0];
            kunai.SetActive(true);
            pools.RemoveAt(0);
            return kunai;
        }
    }

    public void ReturnKunai(GameObject kunai)
    {
        pools.Add(kunai);
        kunai.SetActive(false);
    }

}
