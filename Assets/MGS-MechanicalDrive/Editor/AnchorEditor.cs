/*************************************************************************
 *  Copyright (C), 2017-2018, Mogoson tech. Co., Ltd.
 *  FileName: AnchorEditor.cs
 *  Author: Mogoson   Version: 1.0   Date: 7/3/2017
 *  Version Description:
 *    Internal develop version,mainly to achieve its function.
 *  File Description:
 *    Ignore.
 *  Class List:
 *    <ID>           <name>             <description>
 *     1.         AnchorEditor             Ignore.
 *  Function List:
 *    <class ID>     <name>             <description>
 *     1.
 *  History:
 *    <ID>    <author>      <time>      <version>      <description>
 *     1.     Mogoson     7/3/2017         1.0        Build this file.
 *************************************************************************/

namespace Developer.MechanicalDrive
{
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using UnityEngine;

    public class AnchorEditor : EditorWindow
    {
        #region Property and Field
        protected static AnchorEditor instance;
        protected static Vector2 scrollPos;
        protected static float leftAlign = 80;
        public static bool isOpen { protected set; get; }

        protected static Chain targetChain;
        protected static Material material;
        protected static string materialPath = "Assets/MGS-MechanicalDrive/Material/Blue_Mat.mat";

        public static Transform center { protected set; get; }
        public static float radius { protected set; get; }
        public static float from { protected set; get; }
        public static float to { protected set; get; }
        public static float countC { protected set; get; }
        public static bool isCircularSettingsReasonable
        {
            get
            {
                return center && radius > 0 && from < to && countC > 0;
            }
        }

        public static Transform start { protected set; get; }
        public static Transform end { protected set; get; }
        public static float countL { protected set; get; }
        public static bool isLinearSettingsReasonable
        {
            get
            {
                return start && end && countL > 0;
            }
        }

        protected static string prefix = "Anchor";
        protected static string rendererName = "AnchorRenderer";
        protected static float size = 0.05f;
        #endregion

        #region Private Method
        [MenuItem("Tool/AnchorEditor &A")]
        private static void ShowEditor()
        {
            targetChain = GetChainFromSelection();
            ShowEditorWindow();
        }
        #endregion

        #region protected Method
        protected static void ShowEditorWindow()
        {
            material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
            instance = GetWindow<AnchorEditor>("Anchor Editor", true);
            instance.autoRepaintOnSceneChange = true;
            instance.Show();
            isOpen = true;
        }

        protected static Chain GetChainFromSelection()
        {
            if (Selection.activeGameObject)
                return Selection.activeGameObject.GetComponent<Chain>();
            else
                return null;
        }

        protected virtual void OnSelectionChange()
        {
            var chain = GetChainFromSelection();
            if (targetChain == chain)
                return;
            targetChain = chain;
            Repaint();
        }

        protected virtual void OnGUI()
        {
            if (targetChain)
            {
                if (targetChain.anchorRoot)
                {
                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

                    #region Circular Anchor Creater
                    GUILayout.BeginVertical("Circular Anchor Creater", "Window", GUILayout.Height(140));
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Center", GUILayout.Width(leftAlign));
                    EditorGUI.BeginChangeCheck();
                    center = (Transform)EditorGUILayout.ObjectField(center, typeof(Transform), true);
                    if (EditorGUI.EndChangeCheck())
                        ActiveSceneWindow();
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Radius", GUILayout.Width(leftAlign));
                    radius = EditorGUILayout.FloatField(radius);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("From", GUILayout.Width(leftAlign));
                    from = EditorGUILayout.FloatField(from);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("To", GUILayout.Width(leftAlign));
                    to = EditorGUILayout.FloatField(to);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Count", GUILayout.Width(leftAlign));
                    countC = EditorGUILayout.FloatField(countC);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(leftAlign + 5);
                    if (GUILayout.Button("Reset"))
                    {
                        center = null;
                        radius = from = to = countC = 0;
                        ActiveSceneWindow();
                    }
                    if (GUILayout.Button("Create"))
                    {
                        if (isCircularSettingsReasonable)
                            CreateCircularAnchors();
                        else
                            Debug.LogError("The parameter settings of circular anchor creater is not reasonable.");
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    #endregion

                    #region Linear Anchor Creater
                    GUILayout.BeginVertical("Linear Anchor Creater", "Window", GUILayout.Height(105));
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Start", GUILayout.Width(leftAlign));
                    EditorGUI.BeginChangeCheck();
                    start = (Transform)EditorGUILayout.ObjectField(start, typeof(Transform), true);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("End", GUILayout.Width(leftAlign));
                    end = (Transform)EditorGUILayout.ObjectField(end, typeof(Transform), true);
                    if (EditorGUI.EndChangeCheck())
                        ActiveSceneWindow();
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Count", GUILayout.Width(leftAlign));
                    countL = EditorGUILayout.FloatField(countL);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(leftAlign + 5);
                    if (GUILayout.Button("Reset"))
                    {
                        start = end = null;
                        countL = 0;
                        ActiveSceneWindow();
                    }
                    if (GUILayout.Button("Create"))
                    {
                        if (isLinearSettingsReasonable)
                            CreateLinearAnchors();
                        else
                            Debug.LogError("The parameter settings of linear anchor creater is not reasonable.");
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    #endregion

                    #region Unified Anchor Manager
                    GUILayout.BeginVertical("Unified Anchor Manager", "Window");
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Prefix", GUILayout.Width(leftAlign));
                    prefix = EditorGUILayout.TextField(prefix);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(leftAlign + 5);
                    if (GUILayout.Button("Reset"))
                        prefix = "Anchor";
                    if (GUILayout.Button("Rename"))
                    {
                        if (prefix.Trim() == string.Empty)
                            Debug.LogError("The value of prefix cannot be empty.");
                        else
                            RenameAnchors();
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(2.5f);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Renderer", GUILayout.Width(leftAlign));
                    size = EditorGUILayout.FloatField(size);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(leftAlign + 5);
                    if (GUILayout.Button("Attach"))
                    {
                        RemoveAnchorRenderer();
                        AttachAnchorRenderer();
                    }
                    if (GUILayout.Button("Remove"))
                        RemoveAnchorRenderer();
                    GUILayout.EndHorizontal();

                    GUILayout.Space(2.5f);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Anchors", GUILayout.Width(leftAlign));
                    if (GUILayout.Button("Delete"))
                    {
                        var delete = EditorUtility.DisplayDialog(
                            "Delete Anchors",
                            "This operate will delete all of the chain anchors.\nAre you sure continue to do this?",
                            "Yes",
                            "Cancel"
                            );
                        if (delete)
                            DeleteAnchors();
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    #endregion

                    EditorGUILayout.EndScrollView();
                }
                else
                    EditorGUILayout.HelpBox("The anchor root of chain has not been assigned.", MessageType.Error);
            }
            else
                EditorGUILayout.HelpBox("No chain object is selected.", MessageType.Info);
        }

        protected virtual void OnDestroy()
        {
            ActiveSceneWindow();
            targetChain = null;
            isOpen = false;
        }

        protected void ActiveSceneWindow()
        {
            EditorApplication.ExecuteMenuItem("Window/Scene");
        }

        protected void CreateCircularAnchors()
        {
            var space = (to - from) / (countC == 1 ? 1 : countC - 1);
            for (int i = 0; i < countC; i++)
            {
                var direction = Quaternion.AngleAxis(from + space * i, targetChain.anchorRoot.forward) * Vector3.up;
                var tangent = -Vector3.Cross(direction, targetChain.anchorRoot.forward);
                var position = center.position + direction * radius;
                CreateAnchor("CircularAnchor" + " (" + i + ")", position, position + tangent, direction, center.GetSiblingIndex());
            }
            center = null;
            RefreshChainCurve();
            EditorSceneManager.MarkAllScenesDirty();
        }

        protected void CreateLinearAnchors()
        {
            var direction = (end.position - start.position).normalized;
            var space = Vector3.Distance(start.position, end.position) / (countL + 1);
            for (int i = 0; i < countL; i++)
            {
                CreateAnchor("LinearAnchor" + " (" + i + ")", start.position + direction * space * (i + 1),
                    end.position, Vector3.Cross(direction, targetChain.anchorRoot.forward), end.GetSiblingIndex());
            }
            start = end = null;
            RefreshChainCurve();
            EditorSceneManager.MarkAllScenesDirty();
        }

        protected void CreateAnchor(string anchorName, Vector3 position, Vector3 lookAtPos, Vector3 worldUp, int siblingIndex)
        {
            var newAnchor = new GameObject(anchorName).transform;
            newAnchor.position = position;
            newAnchor.LookAt(lookAtPos, worldUp);
            newAnchor.parent = targetChain.anchorRoot;
            newAnchor.SetSiblingIndex(siblingIndex);
            AttachRenderer(newAnchor);
        }

        protected void RefreshChainCurve()
        {
            if (targetChain.anchorRoot.childCount >= 2)
                targetChain.CreateCurve();
        }

        protected void RenameAnchors()
        {
            for (int i = 0; i < targetChain.anchorRoot.childCount; i++)
            {
                targetChain.anchorRoot.GetChild(i).name = prefix.Trim() + " (" + i + ")";
            }
            EditorSceneManager.MarkAllScenesDirty();
        }

        protected void AttachAnchorRenderer()
        {
            foreach (Transform anchor in targetChain.anchorRoot)
            {
                AttachRenderer(anchor);
            }
            EditorSceneManager.MarkAllScenesDirty();
        }

        protected void AttachRenderer(Transform anchor)
        {
            var renderer = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            DestroyImmediate(renderer.GetComponent<Collider>());
            renderer.GetComponent<Renderer>().sharedMaterial = material;
            renderer.name = rendererName;
            renderer.parent = anchor;
            renderer.localPosition = Vector3.zero;
            renderer.localRotation = Quaternion.identity;
            renderer.localScale = Vector3.one * size;
        }

        protected void RemoveAnchorRenderer()
        {
            foreach (Transform anchor in targetChain.anchorRoot)
            {
                var renderer = anchor.FindChild(rendererName);
                if (renderer)
                    DestroyImmediate(renderer.gameObject);
            }
            EditorSceneManager.MarkAllScenesDirty();
        }

        protected void DeleteAnchors()
        {
            while (targetChain.anchorRoot.childCount > 0)
            {
                DestroyImmediate(targetChain.anchorRoot.GetChild(0).gameObject);
            }
            EditorSceneManager.MarkAllScenesDirty();
        }
        #endregion

        #region Public Method
        public static void ShowEditor(Chain chain)
        {
            targetChain = chain;
            ShowEditorWindow();
        }
        #endregion
    }//class_end
}//namespace_end