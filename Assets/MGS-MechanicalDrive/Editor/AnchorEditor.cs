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
        public static bool isOpen { protected set; get; }

        protected static Vector2 scrollPos;
        protected const float leftAlign = 150;
        protected const float paragraph = 2.5f;

        protected static Chain targetChain;
        protected static Material material;
        protected const string materialPath = "Assets/MGS-MechanicalDrive/Material/Anchor.mat";

        public static Transform center { protected set; get; }
        public static float radius { protected set; get; }
        public static float from { protected set; get; }
        public static float to { protected set; get; }
        public static int countC { protected set; get; }
        public static bool isCircularSettingsReasonable
        {
            get
            {
                return center && radius > 0 && from < to && countC > 0;
            }
        }

        public static Transform start { protected set; get; }
        public static Transform end { protected set; get; }
        public static int countL { protected set; get; }
        public static bool isLinearSettingsReasonable
        {
            get
            {
                return start && end && countL > 0;
            }
        }

        protected static string prefix = "Anchor";
        protected const string rendererName = "AnchorRenderer";
        protected static float size = 0.05f;
        #endregion

        #region Private Method
        [MenuItem("Tool/Anchor Editor &A")]
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

                    EditorGUI.BeginChangeCheck();
                    center = (Transform)EditorGUILayout.ObjectField("Center", center, typeof(Transform), true);
                    radius = EditorGUILayout.FloatField("Radius", radius);
                    from = EditorGUILayout.FloatField("From", from);
                    to = EditorGUILayout.FloatField("To", to);
                    countC = EditorGUILayout.IntField("Count", countC);
                    if (EditorGUI.EndChangeCheck())
                        ActiveSceneWindow();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(leftAlign);
                    if (GUILayout.Button("Create"))
                    {
                        if (isCircularSettingsReasonable)
                            CreateCircularAnchors();
                        else
                            ShowNotification(new GUIContent("The parameter settings of circular anchor creater is not reasonable."));
                    }
                    if (GUILayout.Button("Reset"))
                        ResetCircularAnchorCreater();
                    GUILayout.EndHorizontal();

                    GUILayout.EndVertical();
                    #endregion

                    #region Linear Anchor Creater
                    GUILayout.BeginVertical("Linear Anchor Creater", "Window", GUILayout.Height(105));

                    EditorGUI.BeginChangeCheck();
                    start = (Transform)EditorGUILayout.ObjectField("Start", start, typeof(Transform), true);
                    end = (Transform)EditorGUILayout.ObjectField("End", end, typeof(Transform), true);
                    countL = EditorGUILayout.IntField("Count", countL);
                    if (EditorGUI.EndChangeCheck())
                        ActiveSceneWindow();

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(leftAlign);
                    if (GUILayout.Button("Create"))
                    {
                        if (isLinearSettingsReasonable)
                            CreateLinearAnchors();
                        else
                            ShowNotification(new GUIContent("The parameter settings of linear anchor creater is not reasonable."));
                    }
                    if (GUILayout.Button("Reset"))
                        ResetLinearAnchorCreater();
                    GUILayout.EndHorizontal();

                    GUILayout.EndVertical();
                    #endregion

                    #region Unified Anchor Manager
                    GUILayout.BeginVertical("Unify Anchor Manager", "Window");
                    prefix = EditorGUILayout.TextField("Prefix", prefix);

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(leftAlign);
                    if (GUILayout.Button("Rename"))
                    {
                        if (prefix.Trim() == string.Empty)
                            ShowNotification(new GUIContent("The value of prefix cannot be empty."));
                        else
                            RenameAnchors();
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(paragraph);
                    size = EditorGUILayout.FloatField("Renderer", size);

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(leftAlign);
                    if (GUILayout.Button("Attach"))
                    {
                        RemoveAnchorRenderer();
                        AttachAnchorRenderer();
                    }
                    if (GUILayout.Button("Remove"))
                        RemoveAnchorRenderer();
                    GUILayout.EndHorizontal();

                    GUILayout.Space(paragraph);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Anchors", GUILayout.Width(leftAlign - 4));
                    if (GUILayout.Button("Delete"))
                    {
                        var delete = EditorUtility.DisplayDialog(
                         "Delete Anchors",
                         "This operate will delete all of the chain anchors.\nAre you sure continue to do this?",
                         "Yes",
                         "Cancel");
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
            ResetCircularAnchorCreater();
            RefreshChainCurve();
            EditorSceneManager.MarkAllScenesDirty();
        }

        protected void ResetCircularAnchorCreater()
        {
            center = null;
            radius = from = to = countC = 0;
            ActiveSceneWindow();
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
            ResetLinearAnchorCreater();
            RefreshChainCurve();
            EditorSceneManager.MarkAllScenesDirty();
        }

        protected void ResetLinearAnchorCreater()
        {
            start = end = null;
            countL = 0;
            ActiveSceneWindow();
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
    }
}