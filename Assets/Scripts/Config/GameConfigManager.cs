using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset res_config1;

    public Restaurent res;

    private void Awake()
    {
        res = JsonUtility.FromJson<Restaurent>(res_config1.text);
        Debug.Log(res.id);
    }

}
