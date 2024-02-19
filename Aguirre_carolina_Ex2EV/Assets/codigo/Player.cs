using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.InputSystem.XInput;

public class Player : MonoBehaviour
{


    InputController inputController;

    //calcular v caidaa

    //Valores camara
    [SerializeField] GameObject freeCam;
    [SerializeField] GameObject IamCam;
   
    //Charactercontroler
    private CharacterController characterController;

   

    //darle gravedad
    public float gravedadR = 9.81f;
    private float velocidadY;





    //animator controler
    private Animator animator;


    //movimiento
    float movy;
    float movx;
    bool rodar=false;
    public float rotationSpeed = 100f;
    //camaras
    [SerializeField] CinemachineVirtualCamera vcam1;
   
    [SerializeField] CinemachineFreeLook fcam1;


    //invoke camara

    private float tiempoTranscurrido = 0f;
    private bool delayCumplido = false;

    private void Awake()
    {
        inputController = new InputController();

        inputController.mov.rodar.started += ctx =>
        {
            rodar = true;
        };
        inputController.mov.rodar.canceled += ctx =>
        {
            rodar = false;
        };


        //movimientos de ambos ejes 
        inputController.mov.movyy.performed += ctx => movy = ctx.ReadValue<float>();
        inputController.mov.rotar.performed += ctx => movx = ctx.ReadValue<float>();



    }

    //started  performed canceled

    // Start is called before the first frame update
    void Start()
    {
        vcam1.Priority = 40;

        //acceder al animator
        animator = gameObject.GetComponent<Animator>();

        // Obtener el componente CharacterController del personaje
        characterController = GetComponent<CharacterController>();

     



    }

    // Update is called once per frame
    void Update()
    {
        rotar();
        cambiocamara();
        movenx();
        moveny();
        animator.SetFloat("mov", movy);
        animator.SetBool("rodar",rodar);
       

    }

    void rotar()
    {


        // Obtener la entrada del joystick horizontal
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calcular la cantidad de rotación en función de la entrada del joystick y la velocidad de rotación
        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime * movx;

        // Rotar el objeto del jugador alrededor del eje vertical (Y) basado en la cantidad de rotación calculada
        transform.Rotate(Vector3.up, rotationAmount);

    }

    void cambiocamara()
    {
        
        if (!delayCumplido)
        {
            tiempoTranscurrido += Time.deltaTime;

            if (tiempoTranscurrido >= 4f) // Si han pasado 4 segundos
            {
                // Ejecutar la lógica de tu clase después del retraso
                vcam1.Priority = 5;

                // Marcar que el delay ha sido cumplido
                delayCumplido = true;
            }
        }



   
void moveny()
        {
       
            
                // Aplicar movimiento al objeto en el eje X
                transform.Translate(movimiento * velocidadMovimiento *movy* Time.deltaTime);
            }


        }
  




    
    

    void movenx()
    {
        // Obtener la entrada del joystick horizontal
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calcular la cantidad de rotación en función de la entrada del joystick y la velocidad de rotación
        float rotationAmount = horizontalInput * rotationSpeed * movx* Time.deltaTime;

        // Rotar el objeto del jugador alrededor del eje vertical (Y) basado en la cantidad de rotación calculada
        transform.Rotate(Vector3.up, rotationAmount);
    }
}
     


    private void OnEnable()
    {
        inputController.Enable();
    }

    private void OnDisable()
    {
        inputController.Disable();
    }
}