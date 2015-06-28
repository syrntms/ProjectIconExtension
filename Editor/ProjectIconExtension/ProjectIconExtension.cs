using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[InitializeOnLoad]
public class ProjectIconExtension {

	static ProjectIconExtension()
	{
		EditorApplication.projectWindowItemOnGUI += onDrawProjectGameObject;
	}

	private static void onDrawProjectGameObject(string guid, Rect selectionRect)
	{
		var path = AssetDatabase.GUIDToAssetPath(guid);
		Rect r = new Rect(selectionRect); 
		r.x = r.xMax;

		foreach (var extension in ProjectIconExtensionList.WorkingExtensions) {
			var obj = AssetDatabase.LoadAssetAtPath(path, typeof(System.Object)) as System.Object;
			var iconTexture = extension.GetDisplayIcon(obj);
			if (iconTexture == null) {
				continue;
			}

			r.x -= 18;
			r.width = 20;
			GUI.Label (r, iconTexture); 
		}
	}
}
