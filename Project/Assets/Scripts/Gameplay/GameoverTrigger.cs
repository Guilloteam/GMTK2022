using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverTrigger : MonoBehaviour
{
    public GameObject gameoverMenu;

    void Start()
    {
        GetComponent<DamageReceiver>().deathDelegate += () => { gameoverMenu.SetActive(gameoverMenu);};
        
    }

}
