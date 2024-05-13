using System.Collections;


using UnityEngine;
using UnityEngine.UI;

namespace DiyalogSystem
{

    public class Dialog : MonoBehaviour
    {
        protected IEnumerator WriteText(string input,Text textholder)
        {
            for(int i = 0; i < input.Length; i++)
            {
                textholder.text+= input[i];
                yield return new WaitForSeconds(0.1f);

            }
        }
    }
}
