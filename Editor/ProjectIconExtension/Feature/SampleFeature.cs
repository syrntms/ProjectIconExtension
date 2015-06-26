using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[InitializeOnLoad]
public class SampleFeature : IProjectIconExtensionFeature {
	private static Texture2D iconTexture;

	static SampleFeature()
	{
		ProjectIconExtension.AddExtension(new SampleFeature());
		iconTexture = AssetDatabase.LoadAssetAtPath(
			"Assets/ProjectIconExtension/Editor/ProjectIconExtension/Feature/Alice.png",
			typeof(Texture2D)
		) as Texture2D;
	}

	public int GetPriority ()
	{
		return 0;
	}

	public Texture2D GetDisplayIcon(System.Object obj)
	{
		return iconTexture;
	}

}
