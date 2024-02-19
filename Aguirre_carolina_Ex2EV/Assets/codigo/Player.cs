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
    float movy
    bool rodar=false;
    //camaras
    [SerializeField] CinemachineVirtualCamera vcam1;
   
    [SerializeField] CinemachineFreeLook fcam1;


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
        inputController.mov.movyy.performed += ctx => movy = ctx.ReadValue<float>();


        //movimiento horizontal

        inputController.Player.DesDer.performed += ctx =>
        {
            movHd = 1f;
            animator.SetBool("caminando", true);

        };
        inputController.Player.DesDer.canceled += ctx =>
        {
            movHd = 0f;
            animator.SetBool("caminando", false);
        };

        inputController.Player.DesIzq.performed += ctx =>
        {
            movHi = -1f;
            animator.SetBool("caminando", true);
        };
        inputController.Player.DesIzq.canceled += ctx =>
        {
            movHi = 0f;
            animator.SetBool("caminando", false);
        };


      


    }

    //started  performed canceled

    // Start is called before the first frame update
    void Start()
    {

        //acceder al animator
        animator = gameObject.GetComponent<Animator>();

        // Obtener el componente CharacterController del personaje
        characterController = GetComponent<CharacterController>();

     



    }

    // Update is called once per frame
    void Update()
    {
       

        animator.SetFloat("mov", movy);
        animator.SetBool("rodar",rodar);
       

    }

    void mover()
    {


        transform.Rotate(Vector3.up * rotar * Time.deltaTime * 360 * rotarSpeed);

    }

    void salto()
    {
        if (velocidadY == 0) { velocidadY = jumpForce; }


    }



    /* void apuntado()
     {
         
         if(Imput.GetKeyDown(KeyCode.LeftControl))
         {
             freeCam.SetActive(false);
             IamCam.Setactive(true);
         }
         if (Imput.GetKeyUp(KeyCode.LeftControl))
         {
             freeCam.SetActive(true);
             IamCam.Setactive(false);
         }
         
}
*/

    void gravedad()
    {
        // Verificar si el CharacterController está en el suelo
        if (characterController.isGrounded)
        {
            // Si está en el suelo, resetear la velocidad vertical
            velocidadY = 0;
        }
        else
        {
            // Si no está en el suelo, aplicar la gravedad
            velocidadY -= gravedadR * Time.deltaTime;
        }

        // Movimiento horizontal
        float movimientoHorizontal = movH * velocidadandar;
        float movimientoVertical = movV * velocidadandar;
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical);

        if (movV < -0.1)
        {
            animator.SetBool("caminando", true);
        }
        if (movV > 0.1)
        { animator.SetBool("caminando", true); }
        else
        {
            animator.SetBool("caminando", false);
        }
        // Aplicar movimiento relativo al espacio del objeto
        movimiento = transform.TransformDirection(movimiento);

        // Aplicar velocidad de movimiento al personaje
        characterController.Move(movimiento * Time.deltaTime);

        // Aplicar la velocidad vertical al personaje
        characterController.Move(new Vector3(0, velocidadY, 0) * Time.deltaTime);
    }

    void velocidadEnY()
    {
        // Obtener la posición actual del personaje en el eje Y
        float posicionActualY = transform.position.y;

        // Calcular la velocidad en el eje Y
        float velocidadY = (posicionActualY - posicionAnteriorY) / Time.deltaTime;

        // Actualizar la posición anterior del personaje en el eje Y
        posicionAnteriorY = posicionActualY;

        // Si la velocidad es positiva, el personaje va hacia arriba
        if (velocidadY > 0)
        {
            Debug.Log("El personaje está subiendo. Velocidad: " + velocidadY);
        }
        // Si la velocidad es negativa, el personaje va hacia abajo
        else if (velocidadY < 0)
        {
            Debug.Log("El personaje está bajando. Velocidad: " + velocidadY);
        }
        // Si la velocidad es cero, el personaje está en reposo en el eje Y
        else
        {
            // Debug.Log("El personaje está en reposo en el eje Y.");
        }
    }

    void Apuntar()
    {
        vcam2.Priority = 20;
        print("apuntas");
    }

    void DesApuntar()
    {
        vcam2.Priority = 5;
        print("no apuntas");
    }


    void cinematica()
    {
        //

    }
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto tiene la etiqueta "cinematica"
        if (other.CompareTag("cinematica"))
        {
            // Hacer algo cuando se detecta un objeto con la etiqueta "cinematica"
            Debug.Log("Objeto con etiqueta 'cinematica' detectado: " + other.name);

            vcam3.Priority = 40;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que sale tiene la etiqueta "cinematica"
        if (other.CompareTag("cinematica"))
        {
            // Hacer algo cuando un objeto con la etiqueta "cinematica" sale del triggerA
            Debug.Log("Objeto con etiqueta 'cinematica' salió del trigger: " + other.name);
            vcam3.Priority = 2;
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