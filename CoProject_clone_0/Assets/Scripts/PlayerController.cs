using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float walkingSpeed = 5f;

    private void Awake()
    {
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

        transform.position += new Vector3(horizontalInput, verticalInput) * Time.deltaTime * walkingSpeed;
    }
}
