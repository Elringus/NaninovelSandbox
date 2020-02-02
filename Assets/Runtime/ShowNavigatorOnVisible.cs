using UnityEngine;
using Naninovel;

[RequireComponent(typeof(ScriptableUIBehaviour))]
public class ShowNavigatorOnVisible : MonoBehaviour
{
    private ScriptableUIBehaviour scriptableUIBehaviour;
    private ScriptManager scriptManager;

    private void Awake ()
    {
        scriptableUIBehaviour = GetComponent<ScriptableUIBehaviour>();
        scriptManager = Engine.GetService<ScriptManager>();
    }

    private void OnEnable ()
    {
        scriptableUIBehaviour.OnVisibilityChanged += HandleVisibilityChanged;
    }

    private void OnDisable ()
    {
        scriptableUIBehaviour.OnVisibilityChanged -= HandleVisibilityChanged;
    }

    private void HandleVisibilityChanged (bool isVisible)
    {
        if (isVisible) scriptManager.ScriptNavigator.Show();
    }
}
