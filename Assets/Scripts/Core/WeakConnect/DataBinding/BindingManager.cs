using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BindingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var parameters = GetBindingParameters();
        RaiseOneWayBindings(parameters);
        RaiseTwoWayBindings(parameters);
        LogWarningIfRequired();
    }

    public void RefreshAll()
    {
        var targets = GetBindingParameters().Where(p
            => p.Type == BindingParameter.ParameterType.OneWaySource ||
               p.Type == BindingParameter.ParameterType.TwoWaySource);

        foreach (var param in targets)
            param.RefreshBinding();
    }

    private void RaiseOneWayBindings(BindingParameter[] bindings)
    {
        foreach (var current in bindings)
        {
            if (current.Type != BindingParameter.ParameterType.OneWaySource) continue;

            var destinations = Array.FindAll(bindings, b
                => b.BindingID == current.BindingID && b.Type == BindingParameter.ParameterType.OneWayDestination);

            foreach (var dest in destinations)
            {
                var binding = new OneWayBinding
                {
                    Source = current, Destination = dest
                };

                current.RefreshBindingRequired.AddListener(binding.RefreshBinding);
                dest.RefreshBindingRequired.AddListener(binding.RefreshBinding);
                binding.RefreshBinding();
            }
        }
    }

    private void RaiseTwoWayBindings(BindingParameter[] bindings)
    {
        foreach (var current in bindings)
        {
            if (current.Type != BindingParameter.ParameterType.TwoWaySource) continue;

            var destinations = Array.FindAll(bindings, b
                => b.BindingID == current.BindingID && b.Type == BindingParameter.ParameterType.TwoWayDestination);

            foreach (var dest in destinations)
            {
                var binding = new TwoWayBinding()
                {
                    Source = current, Destination = dest
                };

                current.RefreshBindingRequired.AddListener(binding.RefreshBinding);
                dest.RefreshBindingRequired.AddListener(binding.RefreshBinding);
                binding.RefreshBinding();
            }
        }
    }

    private void LogWarningIfRequired()
    {
        var parameters = GetBindingParameters();
        var problems = parameters.Where(p
            => !parameters.Any(q => q != p && q.BindingID == p.BindingID));

        foreach (var p in problems)
            Debug.LogWarning($"Binding Id \"{p.BindingID}\" has no pair");
    }

    private BindingParameter[] GetBindingParameters()
    {
        return Resources.FindObjectsOfTypeAll<BindingParameter>()
            .Where(o => o.gameObject.scene == SceneManager.GetActiveScene())
            .ToArray();
    }
}