using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float walkingSpeed = 5f;
    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = GetComponent<Inventory>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var mouseMovement = Input.GetAxis("Mouse ScrollWheel");

        if(mouseMovement != 0) playerInventory.Scroll(mouseMovement);
        transform.position += new Vector3(horizontalInput, verticalInput).normalized * Time.deltaTime * walkingSpeed;
    }
}
