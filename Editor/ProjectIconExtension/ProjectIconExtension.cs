using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[InitializeOnLoad]
public class ProjectIconExtension {

	private static List<IProjectIconExtensionFeature> extensions = new List<IProjectIconExtensionFeature>();

	static ProjectIconExtension()
	{
		EditorApplication.projectWindowItemOnGUI += onDrawProjectGameObject;
	}

	public static void AddExtension(IProjectIconExtensionFeature feature)
	{
		bool isExist = extensions.Any(ext => feature.GetType() == ext.GetType());
		if (isExist) {
			return;
		}
		extensions.Add(feature);
		extensions = extensions.OrderBy(ext => ext.GetPriority()).ToList();
	}

	public static void RemoveExtension(IProjectIconExtensionFeature feature)
	{
		for (int i = 0 ; i < extensions.Count ; ++i) {
			bool isSameType = extensions[i].GetType() == feature.GetType();
			if (!isSameType) {
				continue;
			}
			extensions.RemoveAt(i);
			break;
		}
	}

	private static void onDrawProjectGameObject(string guid, Rect selectionRect)
	{
		var sortedList = extensions.OrderBy(ext => ext.GetPriority());
		var path = AssetDatabase.GUIDToAssetPath(guid);

		Rect r = new Rect(selectionRect); 
		r.x = r.xMax;

		foreach (var extension in sortedList) {
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
