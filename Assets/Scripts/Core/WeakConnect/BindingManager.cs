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

                current.ValueChanged.AddListener(binding.RefreshBinding);
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

                current.ValueChanged.AddListener(binding.RefreshBinding);
                dest.ValueChanged.AddListener(binding.RefreshBinding);
                binding.RefreshBinding();
            }
        }
    }

    private BindingParameter[] GetBindingParameters()
    {
        return Resources.FindObjectsOfTypeAll<BindingParameter>()
            .Where(o => o.gameObject.scene == SceneManager.GetActiveScene())
            .ToArray();
    }
}