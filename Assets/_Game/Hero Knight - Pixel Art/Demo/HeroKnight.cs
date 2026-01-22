using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    [Header("Essential")]
    public static HeroKnight Instance;
    public TPlayerController playerController;
    public SpriteRenderer spriteRenderer;
    public Animator anim;

    [System.Serializable] public enum CurCharacter
    {
        None,
        Edrick,
        Yael,
        Jasper
    } 
    public CurCharacter _curCharacter;
    public int charaIndex;
   
    public Sprite[] _charaSprite;
    public bool jasperAvailable;
    public bool yaelAvailable;
    public Transform yaelCheck;
    public Transform japserCheck;
    public GameObject JaspTrigger;
    public float attackRange;
    public LayerMask enemyLayers;

    public Transform YaelTrig;
    public GameObject YaelBlock;

     private int minchara = 0;
    private int maxchara = 2;

    public float dist; 
    public GameObject[] Party;
    public GameObject instantiatedChara;

   public Transform yaelLastPos;
    public Transform jasLastPos;

 


    // Use this for initialization
    void Start ()
    {
       
        _curCharacter = CurCharacter.Edrick;
         
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
    void Update()
    {
        Vector2 yaelDis = yaelCheck.position;
        Vector2 jasDis = japserCheck.position;
        dist = Vector2.Distance(yaelDis, jasDis);

        if (charaIndex > maxchara)
        {
            charaIndex = minchara;
        }
        if (charaIndex < minchara)
        {
            charaIndex = maxchara;
        }
        if (charaIndex == 0)
        {
            _curCharacter = CurCharacter.Edrick;
        }
        if (charaIndex == 1 && yaelAvailable)
        {
            _curCharacter = CurCharacter.Yael;
            
        }
        if (charaIndex == 2 && jasperAvailable)
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

        if (Input.GetKeyDown("e"))
        {
            Debug.Log("SwapRight");
            SwapRight();
        }
        else if (Input.GetKeyDown("q"))
        {
            Debug.Log("SwapLeft");
            SwapLeft();

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Placing Companion");

            if (charaIndex == 1)
            {
                instantiatedChara = Party[0];
            }
            if (charaIndex == 2)
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
        else if (Input.GetMouseButtonDown(0))
        {
            if (_curCharacter == CurCharacter.Jasper)
            {
                JasperAbility();
            }

            if (_curCharacter == CurCharacter.Yael)
            {
                YaelAbility();
            }
        }

    }

    void JasperAbility()
    {
        GameObject Jasper;
        Jasper = Party[1];
        Animator jaspAnim;
        jaspAnim = Jasper.GetComponent<Animator>();
        Debug.Log(jaspAnim);
        anim.SetBool("isAttack", true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(JaspTrigger.transform.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit");
            DestructibleBox box = enemy.GetComponent<DestructibleBox>();
            box.Break();
        }
        anim.SetBool("isAttack", false);
    }

    void YaelAbility()
    {
        Instantiate(YaelBlock, YaelTrig.position, Quaternion.identity);
    }
    // Animation Events
    // Called in slide animation.

    void SwapRight()
    {
        Debug.Log("Swapping CH Right");
        charaIndex += 1;
        SwapCharaPositions();

    }
    void SwapLeft()
    {
        Debug.Log("Swapping CH Left");
        charaIndex -= 1;
        SwapCharaPositions();
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

    public void SwapCharaPositions()
    {
        Debug.Log("Swapping Positions: Start");
        StartCoroutine(SwapCoroutine());
    }

    public IEnumerator SwapCoroutine()
    {
        Debug.Log("Swapping Positions: End");

        GameObject yael = Party[0];
        GameObject jasper = Party[1];

        if (yael == null || jasper == null)
        {
            Debug.LogWarning("Missing party member reference.");
            yield break;
        }

        // Cache their positions before moving
        Vector3 yaelPos = yael.transform.position;
        Vector3 jasperPos = jasper.transform.position;

        Collider2D yaelCo = yael.GetComponent<Collider2D>();
        Collider2D jasCo = jasper.GetComponent<Collider2D>();

        
        //yield return new WaitForSeconds(0.5f);

        // Disable colliders to avoid overlap
        if (yaelCo) yaelCo.enabled = false;
        if (jasCo) jasCo.enabled = false;

        yield return new WaitForSeconds(0.1f);

        // Swap positions

        if (playerController.moving) // if player is moving
        {
            if (playerController.isFacingRight) //and if player is facing right
            {
                yael.transform.position = jasperPos + new Vector3(0.5f, 0, 0);
                jasper.transform.position = yaelPos + new Vector3(0.5f, 0, 0);
                // Re-enable colliders
                yield return new WaitForSeconds(0.1f);
                if (yaelCo) yaelCo.enabled = true;
                if (jasCo) jasCo.enabled = true;
            }
            else if (!playerController.isFacingRight) // if they are facing left
            {
                yael.transform.position = jasperPos + new Vector3(-0.5f, 0, 0);
                jasper.transform.position = yaelPos + new Vector3(-0.5f, 0, 0);
                // Re-enable colliders
                yield return new WaitForSeconds(0.1f);
                if (yaelCo) yaelCo.enabled = true;
                if (jasCo) jasCo.enabled = true;
            }
            
        }
        else //if they are not moving
        {
            yael.transform.position = jasperPos;
            jasper.transform.position = yaelPos;
            // Re-enable colliders
            yield return new WaitForSeconds(0.1f);
            if (yaelCo) yaelCo.enabled = true;
            if (jasCo) jasCo.enabled = true;
        }
        

        // Re-enable colliders
        yield return new WaitForSeconds(0.1f);
        if (yaelCo) yaelCo.enabled = true;
        if (jasCo) jasCo.enabled = true;

        Debug.Log("Positions swapped successfully.");
    }



    [ContextMenu("SavePOS")]
    public void SavePlayerPosition()
    {
        DataManager.Instance._savedPosition.transform.position = this.gameObject.transform.position;
    }
}
