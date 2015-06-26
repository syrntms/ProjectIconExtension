using UnityEngine;
using UnityEditor;
using System;

public interface IProjectIconExtensionFeature {

	int GetPriority();
	Texture2D GetDisplayIcon(System.Object go);
}
