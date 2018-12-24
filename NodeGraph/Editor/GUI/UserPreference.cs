﻿using UnityEngine;
using UnityEditor;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using Model=NodeGraph.DataModel;

namespace NodeGraph {

	public static class UserPreference  {

		static readonly string kKEY_USERPREF_GRID = "NodeGraph.UserPref.GridSize";

		private static bool s_prefsLoaded = false;

		private static float s_editorWindowGridSize;

		public static float EditorWindowGridSize {
			get {
				LoadAllPreferenceValues();
				return s_editorWindowGridSize;
			}
			set {
				s_editorWindowGridSize = value;
				SaveAllPreferenceValues();
			}
		}

		private static void LoadAllPreferenceValues() {
			if (!s_prefsLoaded)
			{
				s_editorWindowGridSize = EditorPrefs.GetFloat(kKEY_USERPREF_GRID, 12f);

				s_prefsLoaded = true;
			}
		}

		private static void SaveAllPreferenceValues() {
			EditorPrefs.SetFloat(kKEY_USERPREF_GRID, s_editorWindowGridSize);
		}

		[PreferenceItem("Node Graph")]
		public static void PreferencesGUI() {
			LoadAllPreferenceValues();

			s_editorWindowGridSize = EditorGUILayout.FloatField("背景格子尺寸", s_editorWindowGridSize);
           
            if (GUI.changed) {
				SaveAllPreferenceValues();
			}
		}
	}
}