using UnityEngine;
using TMPro;

public class FirstLevel : MonoBehaviour
{
    public GameObject panelObjetivos;
    public TextMeshProUGUI textoObjetivos;

    private bool objetivo1Cumplido;
    private bool objetivo2Cumplido;
    private bool objetivo3Cumplido;
    private bool objetivo4Cumplido;

    private BoxCollider bloqueoCollider;

    private void Start()
    {
        bloqueoCollider = GetComponent<BoxCollider>();
        bloqueoCollider.enabled = true;

        objetivo1Cumplido = false;
        objetivo2Cumplido = false;
        objetivo3Cumplido = false;
        objetivo4Cumplido = false;

        MostrarObjetivos();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            objetivo1Cumplido = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            objetivo2Cumplido = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            objetivo3Cumplido = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            objetivo4Cumplido = true;
        }

        MostrarObjetivos();

        if (objetivo1Cumplido && objetivo2Cumplido && objetivo3Cumplido && objetivo4Cumplido)
        {
            bloqueoCollider.enabled = false;
        }
    }

    private void MostrarObjetivos()
    {
        textoObjetivos.text = "Objetivos:\n";

        if (objetivo1Cumplido)
        {
            textoObjetivos.text += $"<color=green>Objetivo 1: Usar W para avanzar (Completado)</color>\n";
        }
        else
        {
            textoObjetivos.text += $"Objetivo 1: Usar W para avanzar\n";
        }

        if (objetivo2Cumplido)
        {
            textoObjetivos.text += $"<color=green>Objetivo 2: Usar S para retroceder (Completado)</color>\n";
        }
        else
        {
            textoObjetivos.text += $"Objetivo 2: Usar S para retroceder\n";
        }

        if (objetivo3Cumplido)
        {
            textoObjetivos.text += $"<color=green>Objetivo 3: Usar A para caminar hacia la izquierda (Completado)</color>\n";
        }
        else
        {
            textoObjetivos.text += $"Objetivo 3: Usar A para caminar hacia la izquierda\n";
        }

        if (objetivo4Cumplido)
        {
            textoObjetivos.text += $"<color=green>Objetivo 4: Usar D para caminar hacia la derecha (Completado)</color>\n";
        }
        else
        {
            textoObjetivos.text += $"Objetivo 4: Usar D para caminar hacia la derecha\n";
        }
    }
}
