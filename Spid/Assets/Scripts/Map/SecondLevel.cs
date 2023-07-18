using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevel : MonoBehaviour {

    public GameObject panelObjetivos;
    public TextMeshProUGUI textoObjetivos;

    private bool objetivo;

    private void Start() {

        resetearObjetivos();

        objetivo = false;
    }

    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            objetivo = true;
        }

        MostrarObjetivos();
    }


}
