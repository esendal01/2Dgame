using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


namespace DiyalogSystem
{
    public class DialogChild : Dialog
    {
        [Header("Text Options")]
        [SerializeField]private string input;
        private Text textholder;


        private void Awake()
        {
            textholder = GetComponent<Text>();
            StartCoroutine(WriteText(input,textholder));
        }
    }
}
