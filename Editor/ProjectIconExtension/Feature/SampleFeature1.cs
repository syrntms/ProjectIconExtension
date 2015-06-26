using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[InitializeOnLoad]
public class SampleFeature1 : IProjectIconExtensionFeature {
	private static Texture2D iconTexture;

	static SampleFeature1()
	{
		ProjectIconExtension.AddExtension(new SampleFeature1());
		iconTexture = AssetDatabase.LoadAssetAtPath(
			"Assets/ProjectIconExtension/Editor/ProjectIconExtension/Feature/Bob.png",
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
