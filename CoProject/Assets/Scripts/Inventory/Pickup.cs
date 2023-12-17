using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public PickupScriptable _type;
    public Image icon;
    public Image background;
    public Vector2Int referencePosition { get; set; }
    public Container currentContainer { get; set; }
    public RectTransform rectTransform { get; set; }
    public ContainerDisplay currentContainerDisplay { get; set; }
    public bool isRotated;
    public int rotateIndex;
    private Vector3 rotateTarget;

    public Vector2Int correctSize
    {
        get
        {
            return new Vector2Int(!isRotated ? _type._width : _type._height, isRotated ? _type._height : _type._width);
        }
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    private void LateUpdate()
    {
        UpdateRotateAnimation();
    }

    /// <summary>
    /// Rotates the item to the correct position the player needs.
    /// </summary>
    public void Rotate()
    {
        if (rotateIndex < 3)
        {
            rotateIndex++;
        }
        else if (rotateIndex >= 3)
        {
            rotateIndex = 0;
        }

        UpdateRotation();
    }

    /// <summary>
    /// Reset the rotate index.
    /// </summary>
    public void ResetRotate()
    {
        rotateIndex = 0;
        UpdateRotation();
    }

    /// <summary>
    /// Update rotation movement.
    /// </summary>
    private void UpdateRotation()
    {
        switch (rotateIndex)
        {
            case 0:
                rotateTarget = new(0, 0, 0);
                isRotated = false;
                break;

            case 1:
                rotateTarget = new(0, 0, -90);
                isRotated = true;
                break;

            case 2:
                rotateTarget = new(0, 0, -180);
                isRotated = false;
                break;

            case 3:
                rotateTarget = new(0, 0, -270);
                isRotated = true;
                break;
        }
    }

    /// <summary>
    /// Updates the item rotation animation.
    /// </summary>
    private void UpdateRotateAnimation()
    {
        Quaternion targetRotation = Quaternion.Euler(rotateTarget);

        if (rectTransform.localRotation != targetRotation)
        {
            rectTransform.localRotation = Quaternion.Slerp(
                rectTransform.localRotation,
                targetRotation,
                InventorySettings.rotationAnimationSpeed * Time.deltaTime
            );
        }
    }
}
