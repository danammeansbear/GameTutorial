using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class EnterVehicle : MonoBehaviour
{
    private bool inVehicle = false;
    CarUserControl vehicleScript;
    
    GameObject player;


    void Start()
    {
        vehicleScript = GetComponent<CarUserControl>();
        player = GameObject.FindWithTag("Player");

        
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && inVehicle == false)
        {
            
            if (Input.GetKey(KeyCode.F))
            {
                
                player.transform.parent = gameObject.transform;              
                vehicleScript.enabled = true;
                player.SetActive(false);
                inVehicle = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
        }
    }
    void Update()
    {
        if (inVehicle == true && Input.GetKey(KeyCode.G))
        {
            vehicleScript.enabled = false;
            player.SetActive(true);
            player.transform.parent = null;
            inVehicle = false;
        }
    }
}