using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
This is Jessepike on answers.unity.com's code.
Accessed on Nov 27 2020 for the Tower Defence Prototype project

https://answers.unity.com/questions/1255001/all-objects-currently-seen-by-the-camera.html

He also said
"
Renderer's have a "isVisible" property, although I haven't had much luck with it. Searched
Unity Answers and found a solution rather quickly, thanks to aldonaletto from this question.
You can check if a Renderer is within the bounds of a camera's frustum.
"

I commented the Update method out, but you could use the update method to update the list
every frame. For this project, I used this as a parent class in my DrawHealthBars.cs so I
could acess the methods.
*/

namespace Imported
{
    public class VisibleRenderers : MonoBehaviour
    {

        public List<Renderer> visibleRenderers = new List<Renderer>();

        public bool LogDebug = false;
        /*
        void Update()
        {
            FindObjects();
        }
        */

        // Find and store visible renderers to a list
        public void FindObjects()
        {
            // Retrieve all renderers in scene
            Renderer[] sceneRenderers = FindObjectsOfType<Renderer>();

            // Store only visible renderers
            visibleRenderers.Clear();
            for (int i = 0; i < sceneRenderers.Length; i++)
                if (IsVisible(sceneRenderers[i]))
                    visibleRenderers.Add(sceneRenderers[i]);

            // debug console
            string result = "Total Renderers = " + sceneRenderers.Length + ".  Visible Renderers = " + visibleRenderers.Count;
            foreach (Renderer renderer in visibleRenderers)
                result += "\n " + renderer.name;

            if(LogDebug) Debug.Log(result);
        }

        // Is the renderer within the camera frustrum?
        bool IsVisible(Renderer renderer)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            return (GeometryUtility.TestPlanesAABB(planes, renderer.bounds)) ? true : false;
        }
    }
}
