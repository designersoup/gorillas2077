using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState

{
    titleCard,
    mainMenu,
    gameStep,
    player1turn,
    player1force,
    player1angle,
    player1fire,
    player2turn,
    player2force,
    player2angle,
    player2fire,
    player1win,
    player2win,
    gameSetup
}

public class gameController : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public GameObject bananaPrefab;
    public GameObject explosion;
    public GameObject explosionPart;
    public GameState currentState;
    public bool gamePaused;

    public int Player1Score;
    public int Player2Score;
   
    

    

    public GameObject[,] buildings;
    public int[] buildingHeights;
    public int maxHeight;
    public GameObject buildingBlock;
    public Sprite[] buildingBlocks;
    private GameObject[] destroyBuildings;
    private GameObject[] destroyExplosions;



    [Header("GUI Panels")]
    public GameObject titleCard;
    public GameObject menuCard;
    public GameObject optionsCard;
    public GameObject setupScreen;
  
    public GameObject player1win;
    public GameObject player2win;
    public GameObject pausePanel;
    public GameObject MainGameGUI;
    public GameObject Player1ScoreDisplay;
    public GameObject Player2ScoreDisplay;

    [Header("HUD Items")]
    public GameObject forceBar;
    public GameObject angleBar;
    public GameObject turnTimer;
    public bool turnTimeLeft;

    [Header("Building Customisation")]

    public Color buildingColour1;
    public Color buildingColour2;
    public Color buildingColour3;

    public float forceCounter;
    public bool forceCounterUp;
    public float forceCounterSpeed;
    public float angleCounter;
    public bool angleCounterUp;
    public float angleCounterSpeed;
    public bool playerFiring;
    public bool playerAngling;
    private Animator anim;
    private Animator animAngle;

    //Game Settings
    public int numberOfRounds;
    public int roundTimer;

    public bool musicOn;
    public bool soundOn;
    public GameObject musicButtonText;
    public GameObject soundButtonText;



    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.titleCard;
        player1.SetActive(false);
        player2.SetActive(false);
        MainGameGUI.SetActive(false);
        menuCard.SetActive(false);
        optionsCard.SetActive(false);
        titleCard.SetActive(true);
       




    }

 

    // Update is called once per frame
    void Update()
    {
        if (gamePaused == false)
        {
            if (currentState == GameState.player1turn)
            {
                forceBar.transform.position = new Vector2(player1.transform.position.x, player1.transform.position.y - 1.0f);
                angleBar.transform.position = new Vector2(player1.transform.position.x + 0.8f, player1.transform.position.y + 0.6f);
                angleBar.transform.localScale = new Vector2(-0.5f, 0.5f);
                turnTimer.transform.position = new Vector2(player1.transform.position.x, player1.transform.position.y + 1.2f);
                currentState = GameState.player1force;
                
                turnTimer.GetComponent<turnTimer>().StartTimer(roundTimer);
                StartCoroutine(forceGUI());

            }
            if (currentState == GameState.player2turn)
            {
                forceBar.transform.position = new Vector2(player2.transform.position.x, player2.transform.position.y - 1.0f);
                angleBar.transform.position = new Vector2(player2.transform.position.x - 0.8f, player2.transform.position.y + 0.6f);
                angleBar.transform.localScale = new Vector2(0.5f, 0.5f);
                turnTimer.transform.position = new Vector2(player2.transform.position.x, player2.transform.position.y + 1.2f);
                currentState = GameState.player2force;
                
                turnTimer.GetComponent<turnTimer>().StartTimer(roundTimer);
                StartCoroutine(forceGUI());

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerInputs();
            }

            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    
                    if (Input.GetTouch(0).position.y < 800)

                    playerInputs();


                }
            }





            if (currentState == GameState.player1fire || currentState == GameState.player1force)
            {
                forceBar.transform.position = new Vector2(player1.transform.position.x, player1.transform.position.y - 1.0f);
                angleBar.transform.position = new Vector2(player1.transform.position.x + 0.8f, player1.transform.position.y + 0.6f);
                turnTimer.transform.position = new Vector2(player1.transform.position.x, player1.transform.position.y + 1.2f);
            }

            if (currentState == GameState.player2fire || currentState == GameState.player2force)
            {
                forceBar.transform.position = new Vector2(player2.transform.position.x, player2.transform.position.y - 1.0f);
                angleBar.transform.position = new Vector2(player2.transform.position.x - 0.8f, player2.transform.position.y + 0.6f);
                turnTimer.transform.position = new Vector2(player2.transform.position.x, player2.transform.position.y + 1.2f);
            }
        }

    }

    void GameSetup()
    {
        currentState = GameState.gameSetup;
        player1.SetActive(true);
        player2.SetActive(true);
        MainGameGUI.SetActive(true);
        generateCity();

        if (Random.Range(0, 2) == 1) currentState = GameState.player2turn;
        else currentState = GameState.player1turn;
        playerTurn();
        forceBar.SetActive(false);
        angleBar.SetActive(false);
        playerFiring = false;
        playerAngling = false;
        Player1ScoreDisplay.GetComponent<Text>().text = 0.ToString();
        Player2ScoreDisplay.GetComponent<Text>().text = 0.ToString();
        FindObjectOfType<audioManager>().Play("Music");
    }

    public void GameStart()
    {
        currentState = GameState.gameSetup;
        menuCard.SetActive(false);
        setupScreen.SetActive(false);
        numberOfRounds = setupScreen.transform.Find("RoundNumberSelect").GetComponent<arrowSelects>().value;
        roundTimer = setupScreen.transform.Find("TurnTimeSelect").GetComponent<arrowSelects>().value;
        FindObjectOfType<audioManager>().Play("Select");
        GameSetup();

    }

    public void playerInputs()
    {
        switch (currentState)
        {

            case GameState.player1force:
                playerFiring = false;
                currentState = GameState.player1angle;
                StartCoroutine(angleGUI());
                break;

            case GameState.player1angle:
                playerAngling = false;
                currentState = GameState.player1fire;
                playerFire(angleCounter, forceCounter);
                break;

            case GameState.player2force:
                playerFiring = false;
                currentState = GameState.player2angle;
                StartCoroutine(angleGUI());
                break;

            case GameState.player2angle:
                playerAngling = false;
                currentState = GameState.player2fire;
                playerFire(angleCounter, forceCounter);
                break;

            case GameState.titleCard:
                currentState = GameState.mainMenu;
                titleCard.SetActive(false);
                menuCard.SetActive(true);


                break;
                


        }
    }

    public void playerFire(float angleInput, float forceInput)
    {
        if (currentState == GameState.player1fire)
        {

            player1.GetComponent<playerScript>().StartInv();
            GameObject banana = Instantiate(bananaPrefab, player1.transform.position, Quaternion.identity);
           // vertSpeed = Mathf.Sin((angle * Mathf.PI) / 180) * velocity;
           // horzSpeed = Mathf.Cos((angle * Mathf.PI) / 180) * velocity;
            banana.GetComponent<banana>().vertSpeed = Mathf.Sin((angleInput * Mathf.PI) / 180) * forceInput;
            banana.GetComponent<banana>().horzSpeed = Mathf.Cos((angleInput * Mathf.PI) / 180) * forceInput;
            // banana.GetComponent<banana>().angle = angleInput;
            // banana.GetComponent<banana>().velocity = forceInput;
            banana.GetComponent<banana>().targetID = 2;
            turnTimer.GetComponent<turnTimer>().StopTimer();


        }

        if (currentState == GameState.player2fire)
        {
            player2.GetComponent<playerScript>().StartInv();
            GameObject banana = Instantiate(bananaPrefab, player2.transform.position, Quaternion.Euler(0, 180f, 0));
            banana.GetComponent<banana>().vertSpeed = Mathf.Sin((angleInput * Mathf.PI) / 180) * forceInput;
            banana.GetComponent<banana>().horzSpeed = Mathf.Cos((angleInput * Mathf.PI) / 180) * forceInput;
           // banana.GetComponent<banana>().angle = -angleInput;
           // banana.GetComponent<banana>().velocity = -forceInput;
            banana.GetComponent<banana>().targetID = 1;
            turnTimer.GetComponent<turnTimer>().StopTimer();

        }
        FindObjectOfType<audioManager>().Play("BananaThrown");

        // StartCoroutine(turnTimer());


    }

    public void playerHit(int playerID)
    {
        if (playerID == 2)
        {
            
            player2.SetActive(false);
            //Instantiate(explosion, player2.transform.position, transform.rotation);
            PlayerExplosion(player2.transform.position.x, player2.transform.position.y);
           
            
                currentState = GameState.player1win;
                Player1Score++;
           
            
        }
        else
        {
            player1.SetActive(false);
            //Instantiate(explosion, player1.transform.position, transform.rotation);
            PlayerExplosion(player1.transform.position.x, player1.transform.position.y);
           
                currentState = GameState.player2win;
                Player2Score++;
           
        }
        
        Debug.Log("Player Hit");
        TurnOver();
    }

    public void generateCity()
    {
        player1.transform.position = new Vector2(-8.5f, -3.5f);
        player2.transform.position = new Vector2(8.5f, -3.5f);
        buildings = new GameObject[20, maxHeight];
        buildingHeights = new int[20];
        for (int i = 0; i < 20; i++)
        {
            int colourSel = Random.Range(0, 3);
            buildingHeights[i] = 0;


            for (int j = 0; j < Random.Range(5, maxHeight); j++)
            {
                buildings[i, j] = Instantiate(buildingBlock, new Vector2(-9.5f + i, -4.77f + (j / 2.0f)), transform.rotation);

                switch (colourSel)
                {
                    case 0:
                        buildings[i, j].GetComponent<SpriteRenderer>().color = buildingColour1;
                        // set the sprite to sprite1
                        break;
                    case 1:
                        buildings[i, j].GetComponent<SpriteRenderer>().color = buildingColour2;
                        break;
                    case 2:
                        buildings[i, j].GetComponent<SpriteRenderer>().color = buildingColour3;
                        break;


                }
                buildings[i, j].GetComponent<SpriteRenderer>().sprite = buildingBlocks[Random.Range(0, buildingBlocks.Length)];
                buildingHeights[i]++;

            }


        }
        int player1offset = Random.Range(1, 5);
        int player2offset = Random.Range(1, 5);
        Debug.Log(player1offset);
        player1.transform.position = new Vector2(player1.transform.position.x + player1offset, player1.transform.position.y + (buildingHeights[1 + player1offset] * 0.5f - 1.0f));
        player2.transform.position = new Vector2(player2.transform.position.x - player2offset, player2.transform.position.y + (buildingHeights[18 - player2offset] * 0.5f - 1.0f));
    }

    public void resetCity()
    {

        destroyBuildings = GameObject.FindGameObjectsWithTag("Building");

        for (var i = 0; i < destroyBuildings.Length; i++)
        {
            Destroy(destroyBuildings[i]);
        }

        destroyExplosions = GameObject.FindGameObjectsWithTag("Explosion");

        for (var i = 0; i < destroyExplosions.Length; i++)
        {
            Destroy(destroyExplosions[i]);
        }

        buildings = new GameObject[0, 0];
        buildingHeights = new int[0];
        player1.SetActive(true);
        player2.SetActive(true);
        
        generateCity();
        Player1ScoreDisplay.GetComponent<Text>().text = Player1Score.ToString();
        Player2ScoreDisplay.GetComponent<Text>().text = Player2Score.ToString();
        currentState = GameState.player1turn;
        playerTurn();


    }



    public void TurnOver()
    {
        //yield return new WaitForSeconds(3.0f);
        if (currentState != GameState.player1win)
        {
            if (currentState != GameState.player2win)
            {
                Debug.Log("Turn Over CAlled");
                if (currentState == GameState.player1fire) currentState = GameState.player2turn;
                if (currentState == GameState.player2fire) currentState = GameState.player1turn;
                StartCoroutine(TurnEndTimer());

            }
            else
            {
                Debug.Log("Win State P2");
                player2win.SetActive(true);
                StartCoroutine(winTimer());

            }
        }
        else
        {
            Debug.Log("Win State P1");
            player1win.SetActive(true);
            StartCoroutine(winTimer());
        }
    }

    public IEnumerator winTimer()
    {
        yield return new WaitForSeconds(2.0f);
        player1win.SetActive(false);
        player2win.SetActive(false);
        resetLevel();


    }

    public IEnumerator TurnEndTimer()
    {
        yield return new WaitForSeconds(0.75f);
        playerTurn();



    }

    public IEnumerator forceGUI()
    {

        playerFiring = true;
        forceBar.SetActive(true);
        angleBar.SetActive(false);
        forceCounter = 0;
        forceCounterUp = true;
        anim = forceBar.GetComponent<Animator>();
        anim.speed = 0f;

        while (playerFiring)
        {

            
                yield return new WaitForFixedUpdate();

            if (gamePaused == false)
            {
                if (forceCounterUp) forceCounter += forceCounterSpeed;
                else forceCounter -= forceCounterSpeed;
                if (forceCounter > 15) forceCounterUp = false;
                if (forceCounter < 0) forceCounterUp = true;
                anim.Play("forceBarAnimation", 0, (forceCounter / 15f) / 2);
            }
            



        }


    }

    public IEnumerator angleGUI()
    {

        playerAngling = true;
        angleBar.SetActive(true);
        angleCounter = 0;
        angleCounterUp = true;
        animAngle = angleBar.GetComponent<Animator>();
        animAngle.speed = 0f;

        while (playerAngling)
        {

            
                yield return new WaitForFixedUpdate();

            if (gamePaused == false)
            {
                if (angleCounterUp) angleCounter += angleCounterSpeed;
                else angleCounter -= angleCounterSpeed;
                if (angleCounter > 90) angleCounterUp = false;
                if (angleCounter < 0) angleCounterUp = true;
                animAngle.Play("angleBarAnimation", 0, (angleCounter / 90f) / 2);
            }



        }

    }

    public void PlayerExplosion(float positionX, float positionY)
    {
        StartCoroutine(BigExplosion(positionX, positionY));
        
    }

    public IEnumerator BigExplosion(float positionX, float positionY)

    {
        int explosionCount = 0;
        while (explosionCount < 4 )
        {
            
            Instantiate(explosion, new Vector2(positionX, positionY), transform.rotation);
            Instantiate(explosionPart, new Vector2(positionX, positionY), transform.rotation);
            FindObjectOfType<audioManager>().ExplosionSFX(Random.Range(0,4));
            explosionCount++;
            yield return new WaitForSeconds(Random.Range(0.1f,0.5f));

        }



    }




        public void playerTurn()
    {


        if (currentState == GameState.player1turn)
        {
         

        }
        if (currentState == GameState.player2turn)
        {
          
        }
    }

    public void resetLevel()
    {
        //Debug.Log("Restarting level");
        //Application.LoadLevel(Application.loadedLevel);
        resetCity();
    }

    public void exitGame()
    {
        Debug.Log("Game Quit");
        FindObjectOfType<audioManager>().Play("Select");
        Application.Quit();
    }

    public void Pause()
    {
        gamePaused = true;
        GameObject flyingBanana = GameObject.FindWithTag("Banana");
        if (flyingBanana != null)
        {
            flyingBanana.GetComponent<banana>().gamePaused = true;
        }
        pausePanel.SetActive(true);
        FindObjectOfType<audioManager>().Play("Select");
    }

    public void UnPause()
    {
        gamePaused = false;
        GameObject flyingBanana = GameObject.FindWithTag("Banana");
        if (flyingBanana != null)
        {
            flyingBanana.GetComponent<banana>().gamePaused = false;
        }
        pausePanel.SetActive(false);
        bananaPrefab.GetComponent<banana>().gamePaused = false;
        FindObjectOfType<audioManager>().Play("Select");
    }

    public void musicButton()
    {
        if (musicOn == false)
        {
            musicOn = true;
            FindObjectOfType<audioManager>().Play("Music");
            musicButtonText.GetComponent<Text>().text = "MUSIC : ON";

        }
        else
        {
            musicOn = false;
            FindObjectOfType<audioManager>().StopMusic();
            musicButtonText.GetComponent<Text>().text = "MUSIC : OFF";
        }
        FindObjectOfType<audioManager>().Play("Select");
    }

    public void soundButton()
    {
        if (soundOn == false)
        {
            soundOn = true;
            FindObjectOfType<audioManager>().playSFX = true;
           
            soundButtonText.GetComponent<Text>().text = "SOUND : ON";


        }
        else
        {
            soundOn = false;
            FindObjectOfType<audioManager>().playSFX = false;
            soundButtonText.GetComponent<Text>().text = "SOUND : OFF";
            int temp = Random.Range(0, 20);
        }
        FindObjectOfType<audioManager>().Play("Select");
    }

    public void optionsButton()
    {
        menuCard.SetActive(false);
        optionsCard.SetActive(true);
        FindObjectOfType<audioManager>().Play("Select");
    }

    public void optionsBackButton()
    {
        menuCard.SetActive(true);
        optionsCard.SetActive(false);
        FindObjectOfType<audioManager>().Play("Select");
    }

    public void matchSetup()
    {

        setupScreen.SetActive(true);
        menuCard.SetActive(false);
    }

    public void ExitToMainMenuButton()
    {
        currentState = GameState.mainMenu;

        FindObjectOfType<audioManager>().Play("Select");
        player1.transform.position = new Vector2(-8.5f, -3.5f);
        player2.transform.position = new Vector2(8.5f, -3.5f);
        pausePanel.SetActive(false);
        player1.SetActive(false);
        player2.SetActive(false);
        MainGameGUI.SetActive(false);
        menuCard.SetActive(true);
        optionsCard.SetActive(false);
        forceBar.SetActive(false);
        angleBar.SetActive(false);
        forceCounter = 0;
        angleCounter = 0;

        destroyBuildings = GameObject.FindGameObjectsWithTag("Building");

        for (var i = 0; i < destroyBuildings.Length; i++)
        {
            Destroy(destroyBuildings[i]);
        }

        destroyExplosions = GameObject.FindGameObjectsWithTag("Explosion");

        for (var i = 0; i < destroyExplosions.Length; i++)
        {
            Destroy(destroyExplosions[i]);
        }

        buildings = new GameObject[0, 0];
        buildingHeights = new int[0];
        gamePaused = false;


    }

    public void TurnTimerUp()
    {
       // turnTimeLeft = false;
        if (currentState == GameState.player1force || currentState == GameState.player1angle) currentState = GameState.player1fire;
        if (currentState == GameState.player2force || currentState == GameState.player2angle) currentState = GameState.player2fire;
        playerFiring = false;
        TurnOver();


    }

}
