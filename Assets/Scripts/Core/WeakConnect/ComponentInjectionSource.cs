using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComponentInjectionSource : MonoBehaviour
{
    public string InjectionID;

    private void Awake()
    {
        var targets = GetTargets();

        foreach (var t in targets)
            t.Inject(gameObject);
    }

    private ComponentInjectionTarget[] GetTargets()
    {
        return Resources.FindObjectsOfTypeAll<ComponentInjectionTarget>()
            .Where(o => o.gameObject.scene == SceneManager.GetActiveScene())
            .Where(o => o.InjectionID == InjectionID)
            .ToArray();
    }
}