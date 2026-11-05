using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

// Va en la poción (el objeto de la Asset Store). Solo el Direct Interactor puede
// cogerla (su capa de interacción es "Directo"), es decir, hay que acercar la mano.
// Al agarrarla y al soltarla actualiza el texto del panel de la escena.
[RequireComponent(typeof(XRGrabInteractable))]
public class ActualizarUIAlAgarrar : MonoBehaviour
{
    public Text texto;
    public Color colorAgarrada = new Color(0.4f, 1f, 0.4f);
    public Color colorSuelta = Color.white;

    XRGrabInteractable interactuable;
    int vecesAgarrada;

    void Awake()
    {
        interactuable = GetComponent<XRGrabInteractable>();
    }

    void Start()
    {
        texto.text = "Poción: en la mesa\nAcerca la mano para cogerla";
        texto.color = colorSuelta;
    }

    void OnEnable()
    {
        interactuable.selectEntered.AddListener(AlAgarrar);
        interactuable.selectExited.AddListener(AlSoltar);
    }

    void OnDisable()
    {
        interactuable.selectEntered.RemoveListener(AlAgarrar);
        interactuable.selectExited.RemoveListener(AlSoltar);
    }

    void AlAgarrar(SelectEnterEventArgs args)
    {
        vecesAgarrada++;
        texto.text = "Poción agarrada con " + NombreMano(args.interactorObject) +
                     "\nVeces agarrada: " + vecesAgarrada;
        texto.color = colorAgarrada;
        Debug.Log(texto.text.Replace('\n', ' '));
    }

    void AlSoltar(SelectExitEventArgs args)
    {
        texto.text = "Poción soltada\nVeces agarrada: " + vecesAgarrada;
        texto.color = colorSuelta;
        Debug.Log(texto.text.Replace('\n', ' '));
    }

    // El interactor cuelga del mando izquierdo o derecho del XR Origin.
    static string NombreMano(IXRSelectInteractor interactor)
    {
        string padre = interactor.transform.parent != null ? interactor.transform.parent.name : "";
        if (padre.Contains("Left")) return "la mano izquierda";
        if (padre.Contains("Right")) return "la mano derecha";
        return interactor.transform.name;
    }
}
