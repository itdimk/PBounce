using System.Linq;
using UnityEngine;

public class PlatformDependent : MonoBehaviour
{
	public RuntimePlatform[] EnabledOnPlatforms;

	private bool? _isVisible;

	private bool IsVisible =>
		_isVisible ?? (bool)(_isVisible = EnabledOnPlatforms.Contains(Application.platform));


	// Start is called before the first frame update
	void OnEnable()
	{
		if (gameObject.activeSelf != IsVisible)
			gameObject.SetActive(IsVisible);
	}
}