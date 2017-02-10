using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlanetPlugin : MonoBehaviour {

    // Add a menu item named "CreatePlanet" to PlanetPlugin in the menu bar.
    [MenuItem("PlanetPlugin/CreatePlanet")]
    static void CreatePlanet()
    {
        GameObject planet = new GameObject() { name = "Planet" };
        planet.AddComponent<Planet>().GetComponent<Planet>().Initialize();//Initialize the Planet
        planet.AddComponent<PlanetMaker>();
    }
}
