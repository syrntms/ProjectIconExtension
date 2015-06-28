using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[InitializeOnLoad]
public class ProjectIconExtensionList
{
	private static List<IProjectIconExtensionFeature> extensions = new List<IProjectIconExtensionFeature>();
	public static List<IProjectIconExtensionFeature> WorkingExtensions = new List<IProjectIconExtensionFeature>();

	public static IEnumerable<IProjectIconExtensionFeature> GetAllExtensions()
	{
		return extensions;
	}
	public static void AddExtension(IProjectIconExtensionFeature feature)
	{
		bool isExist = extensions.Any(ext => feature.GetType() == ext.GetType());
		if (isExist) {
			return;
		}
		extensions.Add(feature);
		extensions = extensions.OrderBy(ext => ext.GetPriority()).ToList();
		UpdateWorkingExtensions();
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
		UpdateWorkingExtensions();
	}

	public static void UpdateWorkingExtensions()
	{
		WorkingExtensions.Clear();
		foreach (var extension in extensions) {
			var stateString = EditorUserSettings.GetConfigValue(extension.GetType().ToString())
				?? ((int)ProjectIconExtensionMenu.ExtensionState.Idle).ToString();
			var state = (ProjectIconExtensionMenu.ExtensionState)int.Parse(stateString);
			switch (state) {
			case ProjectIconExtensionMenu.ExtensionState.Idle:
				break;
			case ProjectIconExtensionMenu.ExtensionState.Work:
				WorkingExtensions.Add(extension);
				break;
			default:
				break;
			}
		}
	}
}
