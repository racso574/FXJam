using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ImageToMatrix))]
public class ImageToMatrixEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ImageToMatrix imageToMatrix = (ImageToMatrix)target;
        if (GUILayout.Button("Convert Image to Matrix"))
        {
            imageToMatrix.ConvertAndLogImageToMatrix();
        }
    }
}
