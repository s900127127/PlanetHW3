using System.Collections;
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
//reference:https://docs.unity3d.com/ScriptReference/Editor.OnSceneGUI.html

[CustomEditor(typeof(PlanetMaker))]
public class PlanetMakerEditor : Editor
{
    PlanetMaker t;
    GameObject selectedObj;
    
    void OnSceneGUI()
    {
        t = target as PlanetMaker;
        try
        {
            selectedObj = Selection.activeTransform.gameObject;
            if (selectedObj.name == t.name)
            {
                setLightTransform();
                t.distance = Vector3.Distance(t.planet.transform.position, t.viewLight.transform.position);

                if (Event.current.keyCode == KeyCode.A || Event.current.keyCode == KeyCode.S)
                    changeTerrain();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void changeTerrain()//以viewLight為中心向球體投設一個與圓球相切的圓錐，在圓錐內部(limitRange)的就向外生長
    {
        Mesh mesh = t.GetComponent<MeshFilter>().sharedMesh;
        List<Vector3> newVertices = new List<Vector3>();
        Vector3 viewLightLocate = t.viewLight.transform.position;

        float max = Mathf.Sqrt(Mathf.Pow(t.distance, 2) + Mathf.Pow(t.GetComponent<Planet>().radius, 2));
        float min = t.distance - t.GetComponent<Planet>().radius;
        float limitRange = min+( (max - min) * (t.paintRange / 100f));//count limitRange

        foreach (Vector3 oldVertices in mesh.vertices)
        {
            Vector3 tempVertices;
            if (Vector3.Distance(oldVertices, viewLightLocate) < limitRange)//if distance lower than limitRange
            {
                if (Event.current.keyCode == KeyCode.A)
                    tempVertices = Vector3.MoveTowards(oldVertices, oldVertices+ ( oldVertices- t.transform.position), t.paintIntensity/100.0f);//move out
                else
                    tempVertices = Vector3.MoveTowards(oldVertices, t.transform.position, t.paintIntensity/100.0f);//move to core
                newVertices.Add(tempVertices);
            }
            else
                newVertices.Add(oldVertices);
        }
        mesh.vertices = newVertices.ToArray();
        t.planet.GetComponent<MeshFilter>().mesh = mesh;
    }

    private void setLightTransform()
    {
        t.viewLight.transform.position = Camera.current.transform.position; //light move to camera
        t.viewLight.transform.LookAt(t.planet.transform); //light look at planet
    }
}
