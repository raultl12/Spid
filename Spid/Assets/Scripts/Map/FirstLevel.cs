using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FirstLevel : MonoBehaviour
{
    public GameObject panelObjetivos;
    public TextMeshProUGUI textoObjetivos;

    private List<Objetivo> objetivosWASD = new List<Objetivo>();
    private List<Objetivo> objetivosSaltos = new List<Objetivo>();
    private List<Objetivo> objetivosDeslizar = new List<Objetivo>();
    private List<Objetivo> objetivosWallrun = new List<Objetivo>();

    public BoxCollider bloqueoCollider;

    public enum ZonaActual {
        WASD,
        Saltos,
        Deslizar,
        Wallrun
    }

    private ZonaActual zonaActual;

    private void Start()
    {
        bloqueoCollider = GetComponent<BoxCollider>();
        bloqueoCollider.enabled = true;

        // Crear y agregar objetivos para la zona WASD
        objetivosWASD.Add(new Objetivo("Objetivo 1", "Usar W para avanzar"));
        objetivosWASD.Add(new Objetivo("Objetivo 2", "Usar S para retroceder"));
        objetivosWASD.Add(new Objetivo("Objetivo 3", "Usar A para caminar hacia la izquierda"));
        objetivosWASD.Add(new Objetivo("Objetivo 4", "Usar D para caminar hacia la derecha"));

        // Crear y agregar objetivos para la zona de saltos
        objetivosSaltos.Add(new Objetivo("Objetivo 1", "Usar Espacio para saltar"));
        objetivosSaltos.Add(new Objetivo("Objetivo 2", "Salta hasta alcanzar la plataforma más alta"));

        // Crear y agregar objetivos para la zona de deslizar
        objetivosDeslizar.Add(new Objetivo("Objetivo 1", "Usar LeftCtrl para deslizar"));
        objetivosDeslizar.Add(new Objetivo("Objetivo 2", "Supera los obstáculos utilizando las mecánicas anteriores"));

        // Crear y agregar objetivos para la zona de wallrun
        objetivosWallrun.Add(new Objetivo("Objetivo 1", "Salta a una pared para comenzar el wallrun"));
        objetivosWallrun.Add(new Objetivo("Objetivo 2", "Supera los obstáculos para completar los movimientos básicos"));

        MostrarObjetivos(objetivosWASD); // Aquí puedes llamar al método correspondiente para la zona inicial.
    }

    private void Update()
    {
        // Comprobar la zona actual antes de permitir completar o mostrar los objetivos
        switch (zonaActual) {
            
            case ZonaActual.WASD:
                if (Input.GetKeyDown(KeyCode.W)) {
                    MarcarObjetivoCumplido(objetivosWASD, "Objetivo 1");
                }
                else if (Input.GetKeyDown(KeyCode.S)) {
                    MarcarObjetivoCumplido(objetivosWASD, "Objetivo 2");
                }
                else if (Input.GetKeyDown(KeyCode.A)) {
                    MarcarObjetivoCumplido(objetivosWASD, "Objetivo 3");
                }
                else if (Input.GetKeyDown(KeyCode.D)) {
                    MarcarObjetivoCumplido(objetivosWASD, "Objetivo 4");
                }
                break;
            
            case ZonaActual.Saltos:
                
                if (Input.GetKeyDown(KeyCode.Space)) {
                    MarcarObjetivoCumplido(objetivosSaltos, "Objetivo 1");
                }
                break;
            
            case ZonaActual.Deslizar:
                
                if (Input.GetKeyDown(KeyCode.LeftControl)) {
                    MarcarObjetivoCumplido(objetivosDeslizar, "Objetivo 1");
                }

                break;
            
            case ZonaActual.Wallrun:
                
                break;
        }
        Debug.Log(zonaActual);
    }

    private void OnCollisionEnter(Collision other) {
        
        if (other.gameObject.CompareTag("Player")) {
            switch (zonaActual) {

                case ZonaActual.WASD:

                    if (SonObjetivosCompletados(objetivosWASD)) {
                        zonaActual = ZonaActual.Saltos;
                        MostrarObjetivos(objetivosSaltos);
                        bloqueoCollider.enabled = false;
                    }

                    break;

                case ZonaActual.Saltos:

                    if (SonObjetivosCompletados(objetivosSaltos)) {
                        zonaActual = ZonaActual.Deslizar;
                        MostrarObjetivos(objetivosDeslizar);
                        bloqueoCollider.enabled = false;
                    }

                    break;

                case ZonaActual.Deslizar:

                    if (SonObjetivosCompletados(objetivosDeslizar)) {
                        zonaActual = ZonaActual.Wallrun; 
                        MostrarObjetivos(objetivosWallrun);
                        bloqueoCollider.enabled = false; 
                    }

                    break;

                case ZonaActual.Wallrun:

                    if (SonObjetivosCompletados(objetivosWallrun)) {
                        // Se acaba la escena
                    }

                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("ZonaWASD")) {
            
            zonaActual = ZonaActual.WASD;
            MostrarObjetivos(objetivosWASD);

        } else if (other.CompareTag("ZonaSaltos")) {
            
            zonaActual = ZonaActual.Saltos;
            MostrarObjetivos(objetivosSaltos);

        } else if (other.CompareTag("ZonaDeslizar")) {
            
            zonaActual = ZonaActual.Deslizar;
            MostrarObjetivos(objetivosDeslizar);

        } else if (other.CompareTag("ZonaWallrun")) {
            
            zonaActual = ZonaActual.Wallrun;
            MostrarObjetivos(objetivosWallrun);
        }
    }

    private void OnCollisionExit(Collision other) {
        
        textoObjetivos.text = "Objetivos:\n"; // Limpiar el panel de objetivos al salir de las zonas
        
        switch (zonaActual) {
            
            case ZonaActual.WASD:
                MostrarObjetivos(objetivosWASD);
                break;

            case ZonaActual.Saltos:
                MostrarObjetivos(objetivosSaltos);
                break;

            case ZonaActual.Deslizar:
                MostrarObjetivos(objetivosDeslizar);
                break;

            case ZonaActual.Wallrun:
                MostrarObjetivos(objetivosWallrun);
                break;
        }
    }

    private void MostrarObjetivos(List<Objetivo> listaObjetivos) {
        
        textoObjetivos.text = "Objetivos:\n";

        foreach (Objetivo objetivo in listaObjetivos) {
            
            if (objetivo.cumplido) {
                textoObjetivos.text += $"<color=green>{objetivo.nombre}: {objetivo.descripcion} (Completado)</color>\n";
            }
            else {
                textoObjetivos.text += $"{objetivo.nombre}: {objetivo.descripcion}\n";
            }
        }
    }

    private bool SonObjetivosCompletados(List<Objetivo> listaObjetivos) {
        
        foreach (Objetivo objetivo in listaObjetivos) {
            
            if (!objetivo.cumplido) {
                
                return false;
            }
        }
        return true;
    }

    private void MarcarObjetivoCumplido(List<Objetivo> listaObjetivos, string nombreObjetivo) {
        
        Objetivo objetivo = listaObjetivos.Find(obj => obj.nombre == nombreObjetivo);

        if (objetivo != null && !objetivo.cumplido) {
            objetivo.cumplido = true;
            MostrarObjetivos(listaObjetivos);
        }
    }
}
