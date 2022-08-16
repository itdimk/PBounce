using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformDependent : MonoBehaviour
{
    public RuntimePlatform[] EnabledOnPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        bool isActive = EnabledOnPlatforms.Contains(Application.platform);
        gameObject.SetActive(isActive);
    }
}