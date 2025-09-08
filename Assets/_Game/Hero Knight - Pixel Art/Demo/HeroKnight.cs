using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {


    public static HeroKnight Instance;
    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    private bool                m_isWallSliding = false;
    private bool                m_grounded = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;

    public SpriteRenderer spriteRenderer;

    [System.Serializable] public enum CurCharacter
    {
        None,
        Edrick,
        Yael,
        Jasper
    }
 public CurCharacter _curCharacter;
    public int charaIndex;
    private int minchara = 0;
    private int maxchara = 2;
    public Sprite[] _charaSprite;
    public bool jasperAvailable;
    public bool yaelAvailable;
    public Transform yaelCheck;
    public Transform japserCheck;
    public float dist; 
    public GameObject[] Party;
    public GameObject instantiatedChara;
    


    // Use this for initialization
    void Start ()
    {
       
        _curCharacter = CurCharacter.Edrick;
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        Vector2 yaelDis = yaelCheck.position;
        Vector2 jasDis = japserCheck.position;
        dist = Vector2.Distance(yaelDis, jasDis);
        
        if(charaIndex > maxchara)
        {
            charaIndex = minchara;
        }
        if(charaIndex < minchara)
        {
            charaIndex = maxchara;
        }
        if(charaIndex == 0)
        {
            _curCharacter = CurCharacter.Edrick;
        }
        if(charaIndex == 1 && yaelAvailable)
        {
            _curCharacter = CurCharacter.Yael;
        }
        if(charaIndex == 2 && jasperAvailable)
        {
            _curCharacter = CurCharacter.Jasper;
        }
        switch (_curCharacter)
        {
            case CurCharacter.Edrick:
                spriteRenderer.sprite = _charaSprite[0];
                break;
            case CurCharacter.Yael:
                spriteRenderer.sprite = _charaSprite[1];
                break;
            case CurCharacter.Jasper:
                spriteRenderer.sprite = _charaSprite[2];
                break;
            case CurCharacter.None:
                break;
            default:
                Debug.Log("unknown character.");
                break;
        }



        // Increase timer that checks roll duration
      

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Move
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Wall Slide
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);

        //Death
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("SwapRight");
            SwapRight();
        }
            
        //Hurt
        else if (Input.GetKeyDown("q"))
        {
     Debug.Log("SwapLeft");
            SwapLeft();

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Placing Companion");
            
            if(charaIndex == 1)
            {
                instantiatedChara = Party[0];
            }
            if(charaIndex == 2)
            {
                instantiatedChara = Party[1];
            }
            PlaceCompanion();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Recall();
        }
       
        //Attack
        else if(Input.GetMouseButtonDown(0))
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
      
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Reset timer
          
        }

        // Block
        else if (Input.GetMouseButtonDown(1))
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_isWallSliding)
        {
          
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }
            

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
         
           // m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
           
        }
    }

    // Animation Events
    // Called in slide animation.

    void SwapRight()
    {
        Debug.Log("Swapping CH Right");
        charaIndex += 1;
        
    }
    void SwapLeft()
    {
        Debug.Log("Swapping CH Left");
        charaIndex -= 1;
    }
    void PlaceCompanion()
    {
        if(charaIndex != 0)
        {
            Debug.Log("Placing Companion: " + _curCharacter);
        }
        
        Party[charaIndex].transform.parent = null;
        Party[charaIndex].SetActive(true);
        
        if(charaIndex == 1)
        {
            yaelAvailable = false;
        }
        if (charaIndex == 2)
        {
            jasperAvailable = false;
        }

    }

    public void Recall()
    {
        Debug.Log("Recall!");
        foreach (GameObject partyMember in Party)
        {
            partyMember.SetActive(false);
            partyMember.transform.parent = this.gameObject.transform;
            yaelAvailable = true;
            jasperAvailable = true;
        }
    }
   
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }

    [ContextMenu("SavePOS")]
    public void SavePlayerPosition()
    {
        DataManager.Instance._savedPosition.transform.position = this.gameObject.transform.position;
    }
}
