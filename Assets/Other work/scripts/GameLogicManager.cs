﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameLogicManager : MonoBehaviour
{ 
    public GameObject player;
    public List<GameObject> AIs;
    private EVP.VehicleController playerController;
    public float speedLimit = 20; // KMPH
    private int score = 0;
    private static int counter = 0;

    class AI_Struct
    {
        public UnityStandardAssets.Vehicles.Car.CarAIControl aiController;

        private bool bOnce;

        public bool Once
        {
            get { return bOnce; }
            set { bOnce = value; }
        }

    };

    List<AI_Struct> aiControllers = new List<AI_Struct>();
       
    void Start()
    {
        playerController = player.GetComponent<EVP.VehicleController>();

        foreach (GameObject obj in AIs)
        {
            if (obj != null)
            {
                 
                AI_Struct st = new AI_Struct();
                st.aiController = obj.GetComponent<UnityStandardAssets.Vehicles.Car.CarAIControl>();
                st.Once = false;
                aiControllers.Add(st);
                 
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < aiControllers.Count; i++)
        {
            float forward_projection = Vector3.Dot(player.transform.forward, aiControllers[i].aiController.transform.forward);
            //Debug.Log("projection" + projection.ToString());

            if (forward_projection > 0)
            {


                float dot = Vector3.Dot(aiControllers[i].aiController.transform.right, player.transform.position - aiControllers[i].aiController.transform.position);


                float forwardProjected = Vector3.Dot(aiControllers[i].aiController.transform.forward, player.transform.position - aiControllers[i].aiController.transform.position);

                float projected_vector = forwardProjected / aiControllers[i].aiController.transform.forward.magnitude;
                float distance = Vector3.Distance(aiControllers[i].aiController.transform.position, player.transform.position);
                float aiSpeed = Vector3.Dot(aiControllers[i].aiController.transform.forward, aiControllers[i].aiController.m_Rigidbody.velocity);
                //Debug.Log("aiSpeed " + aiSpeed.ToString());
                float playerSpeed = Mathf.Abs(playerController.speed);

                if (projected_vector > 0 && distance > 10f && playerSpeed > aiSpeed) //   meters
                {

                    if (!aiControllers[i].Once)
                    {
                        aiControllers[i].Once = true;
                        if (dot < 0)
                        {
                            Debug.Log("Left of the truck");
                            score++;
                        }
                        else
                        {
                            Debug.Log("Right of the Truck");
                            score--;
                        }

                        counter++;
                    }

                }

            }

            if (playerController != null)
            {

                float speed = Mathf.Abs(playerController.speed);

                if (speed > speedLimit)
                {

                    Debug.Log("Speed Limit Reached");
                }
                // Debug.Log("Speed" + speed.ToString());

            }

        }


    }


}
