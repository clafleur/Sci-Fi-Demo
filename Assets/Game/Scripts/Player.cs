using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float speed = 3.5f;
    [SerializeField]
    private float gravity = 9.81f;
    [SerializeField]
    private GameObject muzzelFlash;
    [SerializeField]
    private GameObject hitMarkerPrefab;
    [SerializeField]
    private AudioSource weaponAudio;

    private CharacterController charController;
	// Use this for initialization
	void Start ()
    {
        charController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        CalculateMovement();
        PlayerShot();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * speed;
        velocity.y -= gravity;

        velocity = transform.transform.TransformDirection(velocity);
        charController.Move(velocity * Time.deltaTime);
    }

    void PlayerShot()
    {
        if (Input.GetMouseButton(0))
        {
            muzzelFlash.SetActive(true);
            if (!weaponAudio.isPlaying)
            {
                weaponAudio.Play();
            }
            Ray rayOrgin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrgin, out hitInfo))
            {
                Debug.Log("Raycast hit something " + hitInfo.transform.name);
                GameObject hitMarker = Instantiate(hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                Destroy(hitMarker, 1.0f);
            }
        }
        else
        {
            muzzelFlash.SetActive(false);
            weaponAudio.Stop();
        }
    }
}
