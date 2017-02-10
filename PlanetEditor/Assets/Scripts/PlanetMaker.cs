using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]

public class PlanetMaker :  MonoBehaviour
{
    public GameObject planet; //the planet which is going to deform
    public GameObject viewLight;
    public float paintRange = 1f; //paint range. max to 20
    public float paintIntensity = 1f; //paint intensity. max to 10
    public float distance;//the distance between viewLight and planet

    // Use this for initialization
    void Start () {

        planet = gameObject;
        if (GameObject.Find("ViewLight") == null)//when you create first planet
            InitializeLight();
        else
            viewLight = GameObject.Find("ViewLight");//if more then one planet, only need one light
    }

    private void InitializeLight()
    {
        viewLight = new GameObject() { name = "ViewLight" };
        viewLight.AddComponent<Light>().GetComponent<Light>().type = LightType.Spot;
        viewLight.GetComponent<Light>().color = Color.red;
        viewLight.GetComponent<Light>().range = 50;
        viewLight.GetComponent<Light>().intensity = 8;
    }


}
