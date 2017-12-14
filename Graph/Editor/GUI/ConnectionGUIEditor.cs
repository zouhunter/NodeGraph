﻿using UnityEngine;
using UnityEditor;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Model = NodeGraph.DataModel;
using BridgeUI;

namespace NodeGraph
{
    [CustomEditor(typeof(ConnectionGUIInspectorHelper))]
    public class ConnectionGUIEditor : Editor
    {
        public override bool RequiresConstantRepaint()
        {
            return true;
        }

        public override void OnInspectorGUI()
        {
            ConnectionGUIInspectorHelper helper = target as ConnectionGUIInspectorHelper;

            var con = helper.connectionGUI;

            if (con == null) {
                return;
            }
            EditorGUILayout.HelpBox("连接信息:", MessageType.Info);

            con.Data.Operation.Object.OnInspectorGUI(con, this, () =>
            {
                con.Controller.Perform();
                con.Data.Operation.Save();
                con.ParentGraph.SetGraphDirty();
            });
        }

        private bool DrawToggle(bool on, string tip)
        {
            using (var hor = new EditorGUILayout.HorizontalScope())
            {
                 on = GUILayout.Toggle(on, tip, EditorStyles.radioButton, GUILayout.Height(60),GUILayout.Width(100));
                EditorGUILayout.LabelField(tip);
            }
            return on;
        }

        private Enum DrawEnum(Enum em, string tip) 
        {
            using (var hor = new EditorGUILayout.HorizontalScope())
            {
                em = EditorGUILayout.EnumPopup(em, EditorStyles.toolbarDropDown, GUILayout.Height(60), GUILayout.Width(100));
                EditorGUILayout.LabelField(tip);
            }
            return em;
        }


    }
}