
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable : MonoBehaviour
{
    public void SetEnable()
    {
        gameObject.SetActive(true);
    }

    public void SetDisable()
    {
        gameObject.SetActive(false);
    }
}