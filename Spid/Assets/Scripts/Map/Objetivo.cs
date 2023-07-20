using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetivo : MonoBehaviour {
    
    public string nombre;
    public string descripcion;
    public bool cumplido;

    public Objetivo(string nombre, string descripcion)
    {
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.cumplido = false;
    }

    public void MarcarCumplido()
    {
        cumplido = true;
    }
}
