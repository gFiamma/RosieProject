
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller = default;                       //prendo in reference il character controller
    public static float speed = default;                                    //creo la variabile statica speed per permettere ad altri script di modificarla
    public float baseSpeed = 8;                                             //creo la variabile statica speed per permettere ad altri script di leggerla
    public static float jumpForce = default;                                //creo una variabile statica  che gestisce la forza salto, in modo che anche altri script possano modificarlo
    public float jumpForceBase = 2;                                         //creo una variabile statica base che tiene la forza salto, in modo che gli altri script possano leggerla
    public static float stamina = default;                                  //creo una variabile statica che gestisce la stamina, in modo che gli altri script possano modificarla
    public float baseStamina = 10;                                          //creo una variabile statica base che tiene la stamina, in modo che gli altri script possano leggerla
    private float turnSmoothTime = default;                                 //creo delle variabili float per gestire il modo in cui il player ruota su se stesso quando va in dietro senza girare la camera
    private float turnSmoothVelocity = default;
    [SerializeField]
    private Transform cam = default;                                        //prendo in reference la camera del player
    private Vector3 velocity = default;                                     //creo un vector3 che indicherà la velocità del nostro player
    [SerializeField]
    private float gravity = default;                                        //creo una variabile float che gestirà la gravità che influirà nel nostro player
    [SerializeField]
    private Transform groundCheck = default;                                //prendo in reference dall'inspector il groundcheck per controllare se tocco terra o no
    [SerializeField]
    private float groundDistance = default;                                 //creo una variabile float per dire di quanto tenere lontano il player dal terreno
    [SerializeField]
    private LayerMask groundMask = default;                                 //creo una layermask per far capire al groundcheck quando tocchi il terreno e quando non lo tocchi
    [SerializeField]
    private bool isGrounded = default;                                      //creo una variabile booleana che mi dirà quando il player tocca il terreno o no
    public bool isJumping = false;                                          //creo una variabile booleana che mi dirà se il player ha saltato o no

    public bool isRunning = default;                                        //creo una variabile booleana che controlla se il player sta correndo oppure no
    public bool Morte = default;                                            //creo una variabile statica booleana che controlla se il player è morto oppure no
    public bool canMove = true;                                             //creo una variabile booleana che controlla se il player può muoversi oppure no
    private bool staminaWaiting = default;                                  //creo una variabile booleana che controlla se il player ha smesso di utilizzare la stamina oppure no, per fargli attendere del tempo prima di ricominciare a ricaricarla
    public AudioSource jump = default;                                      //prendo in reference una audiosource per il suono del salto
    public AudioSource footstep = default;                                  //prendo in reference una audiosource per il suono del salto
    public AudioSource deathSound = default;                                //prendo in reference una audiosource per il suono del salto
    public Animator anim;                                                  //prendo in reference l'animator del Player

    //variabile che gestisce gli input
    private InputManager inputManager;
    //nell'awake assegnamo l'inputMaster allo script
    private void Awake()
    {
        inputManager = new InputManager();
    }
    //quando viene abilitato avvia l'inputManager
    private void OnEnable()
    {
        inputManager.Enable();
    }
    //quando viene disabilitato disattiva l'inputManager
    private void OnDisable()
    {
        inputManager.Disable();
    }

    void Start()                                                            //all'inizio 
    {
        Morte = false;                                                  //la variabile morte viene settata a false
        controller.enabled = true;                                      //il character  controller viene settato true
        canMove = true;                                                 //la booleana canmove viene settata true
        Cursor.lockState = CursorLockMode.Locked;                   //il cursore diventa invisibile e bloccato al centro
        Cursor.visible = false;
        speed = baseSpeed;                                          //la velocità è uguale alla velocità base
        jumpForce = jumpForceBase;                                  //la forza del salto è uguale alla forza del salto base
        stamina = baseStamina;                                      //la stamina è uguale alla stamina base
        staminaWaiting = false;                                     //la variabile staminaWaiting è settata a false
        isRunning = false;                                          //la variabile isRunning è settata a false
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);     //la variabile isGrounded è true, solo quando il groundcheck, posizionato ai piedi del player, tocca il terreno

        if (transform.position.y <= -70)                                                        //se il player scendo sotto una determinata posizione
        {
            //facciamo morire il player se scende troppo in basso
        }                                                                                      

        if (Morte == true)                                                                      //se muori
        {
            stamina = baseStamina;                                                              //la stamina viene resettata
        }

        if (isGrounded && velocity.y < 0)                                                       //se sei sul terreno e la velocità di caduta è troppo bassa
        {
            velocity.y = -2f;                                                                   //viene fissata a 2f
        }                                                                                       //questo per assicurarci che il player non schizzi verso il basso quando cadiamo da una piattaforma senza saltare

        float horizontal = inputManager.Input.MovementInput.ReadValue<Vector2>().x;             //prendo in reference l'asse orizzontale dei comandi
        float vertical = inputManager.Input.MovementInput.ReadValue<Vector2>().y;               //prendo in reference l'asse verticale dei comandi
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;                   //creo un vector3 che gestisce la direzione presa dal player

        if(horizontal != 0 && canMove == true|| vertical != 0 && canMove == true)               //se è vero che mi sto muovendo e che posso muovermi
        {
            anim.SetBool("isMoving", true);                                                     //faccio partire l'animazione di camminata
        }
        else                                                                                    //sennò
        {
            anim.SetBool("isMoving", false);                                                    //ferma l'animazione di camminata
        }

        if(isJumping == false && /*DialogueManager.isTyping == false &&*/ canMove == true)          //se è vero che non stai saltando, che non stai parlando con un NPC e che puoi muoverti
        {
            if (inputManager.Input.Cross.WasPressedThisFrame() && isGrounded)                                     //se premi il tasto del salto e sei per terra
            {
                anim.SetTrigger("isJumping");                                                   //faccio partire l'animazione del salto
                jump.Play();                                                                    //faccio partire il suono del salto
                velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);                              //il player salta
            }
        }


        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))                          //controllo sempre la distanza del player dal terreno con un raycast
        {
            var distanceToGround = hit.distance;                                                //ed assegno la distanza dal terreno alla variabile distanceToGround
        }


        if(isGrounded)                                                                          //se sei sul terreno
        {
            anim.SetBool("isFlying", false);                                                    //ferma l'animazione di volo
            isJumping = false;                                                                  //setta isJumping a false
        }
        else                                                                                    //sennò
        {
            isJumping = true;                                                                   //isJumping viene messa a true (questo perchè quando il player è in volo, lo script pensa che il player stia saltando, quindi può avviare la planata, anche quando effettivamente non ha saltato)
        }

        if (canMove == true && /*Pause.GameIsPaused == false && DialogueManager.isTyping == false &&*/ isGrounded == true && footstep.isPlaying == false && controller.velocity.magnitude > 0)               //se il player può muoversi. il gioco non è in pausa, non sta parlando con un npc, è sul terreno, non sta riproducendo il suono dei passi ed il character controller è in movimento
        {
            if (isRunning)                                                                          //se sta correndo
            {
                anim.SetBool("isRunning", true);                                                    //faccio partire l'animazione di corsa
                footstep.volume = Random.Range(0.1f, 0.3f);                                         //setto il volume del suono random tra 0.1 e 0.3
                footstep.pitch = Random.Range(1.4f, 1.6f);                                          //setto la pitch del suono random tra 1.4 e 1.6
            }
            else                                                                                    //sennò
            {
                footstep.volume = Random.Range(0.1f, 0.3f);                                         //setto il volume del suono random tra 0.1 e 0.3
                footstep.pitch = Random.Range(0.9f, 1.1f);                                          //setto la pitch del suono random tra 0.9 e 1.1
            }
            footstep.Play();                                                                        //viene riprodotto il suono dei passi
        }

        if (inputManager.Input.R1.IsPressed() && isGrounded && isRunning == false)                         //se premi il tasto per correre, il player è sul terreno e non sta correndo
        {
            isRunning = true;                                                                       //metto la booleana isRunning a True
            speed += 8;                                                                             //aumento la velocità di corsa di 8 (totale 16)
        }

        if (inputManager.Input.R1.WasReleasedThisFrame() || isRunning == false)                                         //se non premi più il tasto per correre oppure il player non sta correndo
        {
            anim.SetBool("isRunning", false);                                                       //fermo l'animazione della corsa
            isRunning = false;                                                                      //metto la booleana isRunning a false
            speed = baseSpeed;                                                                      //setto la velocità al valore della velocità base
        }

        if (isRunning && controller.velocity.magnitude > 0          )                            //se il player sta correndo e si sta muovendo
        {
            stamina -= Time.deltaTime;                                                              //riduci la stamina over time
            if(stamina<=0)                                                                          //se la stamina è minore o uguale a 0
            {
                stamina = 0;                                                                        //assegno 0 alla stamina
                isRunning = false;                                                                  //il player non può più correre
                staminaWaiting = true;                                                              
                StartCoroutine("staminaWait");                                                      //faccio partire un attesa prima che la stamina ricominci a ricaricarsi
            }
        }else if (stamina<baseStamina && isGrounded && staminaWaiting == false && isJumping == false)       //invece se la stamina è minore della stamina base, il player è sul terreno ,non sta aspettando dopo che ha finito di utilizzare la stamina e non sta saltando
        {
            stamina += Time.deltaTime;                                                              //la stamina si ricarica over time
        }

        velocity.y += gravity * Time.deltaTime;                                                     //setto la gravità sul player

        if (canMove)                                                                                //se puoi muoverti
        {
            controller.Move(velocity * Time.deltaTime);                                             //il charactercontroller viene mosso
        }

        if (direction.magnitude >= 0.1f && canMove)                                                 //se la direzione del player è maggiore o uguale a 0.1 e può muoversi
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;                                  //calcola l'angolo che deve fare il player
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);                                                                           //il player viene ruotato verso dove sta camminando

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;                      //settiamo un movimento del player puù smooth
            controller.Move(moveDir.normalized * speed * Time.deltaTime);                                   //e lo applichiamo al player, in modo che la rotazione sia smooth, e non netta 
        }

        //if (DialogueManager.isTyping == true)                                                               //se stai parlando con qualcuno
        //{
        //    transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);                               //fa guardare il player verso la camera
        //    cam.SetActive(false);                                                                           //disattiva la camera in modo che non puoi muoverla
        //}

    }
    public IEnumerator staminaWait()
    {
        yield return new WaitForSeconds(1f);                                                                //attendo un secondo
        staminaWaiting = false;                                                                             //setto staminawait a false
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);                                                              //attendo 0.5 secondi
    }
}
