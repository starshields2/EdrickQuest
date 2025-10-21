using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    [Header("Essential")]
    public static HeroKnight Instance;
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
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(JaspTrigger.transform.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("hit");
                    DestructibleBox box = enemy.GetComponent<DestructibleBox>();
                    box.Break();
                }
            }

            if (_curCharacter == CurCharacter.Yael)
            {
                Instantiate(YaelBlock, YaelTrig.position, Quaternion.identity);
            }
        }

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
       

        yaelLastPos = Party[0].transform;
        jasLastPos = Party[1].transform;

        StartCoroutine(SwapCoroutine());
    }
   
    public IEnumerator SwapCoroutine()
    {
        Debug.Log("Swapping Positions: End");


        yield return new WaitForSeconds(0.5f);
        GameObject Yael;
        GameObject Jasper;

        Yael = Party[0];
        Jasper = Party[1];

        Collider2D YaelCo = Yael.GetComponent<BoxCollider2D>();
        Collider2D JasCo = Jasper.GetComponent<BoxCollider2D>();
        YaelCo.enabled = false;
        JasCo.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Yael.transform.position = jasLastPos.position;
        Jasper.transform.position = yaelLastPos.position;
        YaelCo.enabled = true;
        JasCo.enabled = true;
    }
    

    [ContextMenu("SavePOS")]
    public void SavePlayerPosition()
    {
        DataManager.Instance._savedPosition.transform.position = this.gameObject.transform.position;
    }
}
