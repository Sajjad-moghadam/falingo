#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System;
using System.Reflection;


[CustomEditor (typeof(TranslatorEditor))]
public class TranslatorEditor : EditorWindow
{

	[MenuItem ("Window/Translate")]
	static void Init ()
	{
		EditorWindow.GetWindow (typeof(TranslatorEditor));
	}

	private	string myStr = "";

	bool alignToX = true;
	bool alignToY = true;
	bool alignToZ = true;
	string selected = "";
	string alignTo = "";

	private	Vector3 copiedPose = Vector3.zero;

	void OnGUI ()
	{
		
		myStr =	GUILayout.TextArea (myStr);
		GUILayout.TextArea (Fa.faConvert (myStr));

		if (GUILayout.Button ("Copy"))
			Copy ();
		
		if (GUILayout.Button ("Translate"))
			Translate ();
		if (GUILayout.Button ("Time Speed .1 - 1"))
			TimeToggle ();
		if (GUILayout.Button ("Time Speed 3 - 1"))
			TimeToggle3 ();

		if (GUILayout.Button ("Copy wolrd pose"))
			CopyWorldPose ();
		
		if (GUILayout.Button ("Paste world pose"))
			PasteWorldPose ();

		if (GUILayout.Button ("Clone 20 Up"))
			Clone (0, 20);
		if (GUILayout.Button ("Clone 20 Down"))
			Clone (0, -20);
		if (GUILayout.Button ("Clone 20 Left"))
			Clone (-20, 0);
		if (GUILayout.Button ("Clone 20 Right"))
			Clone (20, 0);

		GUILayout.Label ("Select various Objects in the Hierarchy view");
		selected = Selection.activeTransform ? Selection.activeTransform.name : "";
		foreach (Transform t in Selection.transforms)
			if (t.GetInstanceID () != Selection.activeTransform.GetInstanceID ())
				alignTo += t.name + " ";	
		EditorGUILayout.LabelField ("Align: ", alignTo);
		alignTo = "";

		EditorGUILayout.LabelField ("With: ", selected);

		GUILayout.BeginHorizontal ();
		alignToX = EditorGUILayout.Toggle ("X", alignToX);
		alignToY = EditorGUILayout.Toggle ("Y", alignToY);
		alignToZ = EditorGUILayout.Toggle ("Z", alignToZ);
		GUILayout.EndHorizontal ();

		if (GUILayout.Button ("Align"))
			Align ();
		
	}

	void Clone (int x, int y)
	{
		if (Selection.activeObject == null)
			return;
		Vector2 move = new Vector2 (x, y);
		Transform root = Selection.activeTransform.parent;
		GameObject c=Instantiate (Selection.activeGameObject,root.position,Quaternion.identity)as GameObject;
		c.transform.parent = root;

		RectTransform rectA = Selection.activeGameObject.GetComponent<RectTransform> ();
		RectTransform rectB = c.GetComponent<RectTransform> ();

		rectB.anchoredPosition = rectA.anchoredPosition+move;
		rectB.localScale = rectA.localScale;
		rectB.sizeDelta = rectA.sizeDelta;

		Selection.activeGameObject = c;

		string myName = root.name;
		c.name = myName;
	}

	void Align ()
	{
		if (selected == "" || alignTo == "")
			Debug.LogError ("No objects selected to align");

		foreach (Transform t in Selection.transforms) {
			Vector3 alignementPosition = Selection.activeTransform.position;
			Vector3 newPosition;
			newPosition.x = alignToX ? alignementPosition.x : t.position.x;
			newPosition.y = alignToY ? alignementPosition.y : t.position.y;
			newPosition.z = alignToZ ? alignementPosition.z : t.position.z;
			t.position = newPosition;	
		}
	}

	void CopyWorldPose ()
	{
		var obj = Selection.activeGameObject;
		if (!obj)
			return;
		copiedPose = obj.transform.position;
	}

	void PasteWorldPose ()
	{
		var obj = Selection.activeGameObject;
		if (!obj)
			return;
		obj.transform.position = copiedPose;
	}

	void Copy ()
	{
		GUIUtility.systemCopyBuffer = Fa.faConvert (myStr);
	}

	void TimeToggle ()
	{
		if (Time.timeScale == 1.0F)
			Time.timeScale = 0.1F;
		else
			Time.timeScale = 1.0F;

		float ti = Time.timeScale;
		Debug.Log ("Time.timeScale = " + ti);
	}

	void TimeToggle3 ()
	{
		if (Time.timeScale == 1.0F)
			Time.timeScale = 3F;
		else
			Time.timeScale = 1.0F;

		float ti = Time.timeScale;
		Debug.Log ("Time.timeScale = " + ti);
	}

    void Translate ()
	{
		var obj = Selection.activeGameObject;
		if (!obj)
			return;
		Text myText = obj.GetComponent<Text> ();
		if (!myText) {
			TextMesh myTextM = obj.GetComponent<TextMesh> ();
			if (!myTextM)
				return;
			
			myTextM.text = Fa.faConvert (myTextM.text);
			obj.transform.Translate (Vector3.up * 1);
			obj.transform.Translate (Vector3.up * -1);
			return;
		}
		myText.text = Fa.faConvert (myText.text);
		obj.transform.Translate (Vector3.up * 1);
		obj.transform.Translate (Vector3.up * -1);
	}

}

#endif
