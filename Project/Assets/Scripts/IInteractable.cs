using System.Text;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(Transform interactTransform);
    //string GetInteractText();
    Transform GetTransform();
}
