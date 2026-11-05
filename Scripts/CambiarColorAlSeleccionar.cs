using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

// Va en el cubo y en la esfera. Solo el Ray Interactor puede cogerlos (su capa de
// interacción es "Rayo"). Cada vez que el rayo los selecciona cambian de color.
[RequireComponent(typeof(XRGrabInteractable))]
public class CambiarColorAlSeleccionar : MonoBehaviour
{
    public Color[] colores = { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta };

    XRGrabInteractable interactuable;
    Renderer render;
    int indice = -1;

    void Awake()
    {
        interactuable = GetComponent<XRGrabInteractable>();
        render = GetComponent<Renderer>();
    }

    // Los eventos del interactuable se escuchan mientras el objeto está activo.
    void OnEnable()
    {
        interactuable.selectEntered.AddListener(AlSeleccionar);
    }

    void OnDisable()
    {
        interactuable.selectEntered.RemoveListener(AlSeleccionar);
    }

    void AlSeleccionar(SelectEnterEventArgs args)
    {
        indice = (indice + 1) % colores.Length;
        render.material.color = colores[indice];
        Debug.Log(name + " seleccionado por " + args.interactorObject.transform.name +
                  ": color " + colores[indice]);
    }
}
