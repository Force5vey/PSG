using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockpitController : MonoBehaviour
{
   [SerializeField] private List<GameObject> screenLights;
   [SerializeField] private float delayBetweenLights;
   

   // Start is called before the first frame update
   void Start()
    {
      StartCoroutine(EnableLightsWithDelay());
    }

    IEnumerator EnableLightsWithDelay()
   {
      foreach(var light in screenLights)
      {
         if (light != null)
         {
            light.SetActive(true);
            yield return new WaitForSeconds(delayBetweenLights);
         }
      }
   }
}
