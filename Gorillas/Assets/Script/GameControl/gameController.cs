using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState

{
    titleCard,
    mainMenu,
    gameStep,
    preRound,
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
    gameSetup,
    gameEnded
}



public class gameController : MonoBehaviour
{
    [System.Serializable]
    public class Player
    {

        public string name;
        public int score;
        public GameObject prefab;
    }
    public GameObject AIController;


    [Header("Prefabs")]
    public Player playerOne = new Player();
    public Player playerTwo = new Player();
    public GameObject player1;
    public GameObject player2;
    public GameObject bananaPrefab;
    public GameObject explosion;
    public GameObject explosionPart;

    [Header("Game States")]
    public GameState currentState;
    public bool gamePaused;
    public int Player1Score;
    public int Player2Score;
    public int numberOfRounds;
    public int roundTimer;
    public int currentRound;
    

    [Header("AI")]
    public bool AIMode;
    public float AIAbility;





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
    public GameObject soloSetupScreen;
    public GameObject gameEndedCard;

    public GameObject winPanel;
    public GameObject preRoundCards;
    
    public GameObject pausePanel;
    public GameObject MainGameGUI;
    public GameObject Player1ScoreDisplay;
    public GameObject Player2ScoreDisplay;
    public GameObject Player1NameDisplay;
    public GameObject Player2NameDisplay;

    public GameObject blurPanel;

    [Header("HUD Items")]
    public GameObject forceBar;
    public GameObject angleBar;
    public GameObject turnTimer;
    public bool turnTimeLeft;
    public GameObject roundText;
    

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
   

    public bool musicOn;
    public bool soundOn;
    public GameObject musicButtonText;
    public GameObject soundButtonText;




   public GameObject scrollingCity;


    private GameObject bananaTemp;




    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.titleCard;
        playerOne.prefab.SetActive(false);
        playerTwo.prefab.SetActive(false);
        blurPanel.SetActive(false);
        MainGameGUI.SetActive(false);
        menuCard.SetActive(false);
        optionsCard.SetActive(false);
        titleCard.SetActive(true);
        turnTimer.SetActive(false);
        roundText.SetActive(false);

        Debug.Log(playerOne.score);
        FindObjectOfType<audioManager>().Play("Music");







    }

 

    // Update is called once per frame
    void Update()
    {
        if (gamePaused == false)
        {
            if (currentState == GameState.player1turn)
            {
                
                forceBar.transform.position = new Vector2(playerOne.prefab.transform.position.x, playerOne.prefab.transform.position.y - 1.0f);
                angleBar.transform.position = new Vector2(playerOne.prefab.transform.position.x + 0.8f, playerOne.prefab.transform.position.y + 0.6f);
                angleBar.transform.localScale = new Vector2(-0.5f, 0.5f);
                turnTimer.transform.position = new Vector2(playerOne.prefab.transform.position.x, playerOne.prefab.transform.position.y + 1.2f);
                forceBar.SetActive(false);
                angleBar.SetActive(false);
                if (AIMode == false)
                {
                    currentState = GameState.player1force;

                    if (!AIMode) turnTimer.GetComponent<turnTimer>().StartTimer(roundTimer);
                    
                    playerOne.prefab.GetComponent<animationController>().setAnim("default");
                    playerTwo.prefab.GetComponent<animationController>().setAnim("idle");
                    StartCoroutine(forceGUI());
                }
                else
                {
                     currentState = GameState.player1fire;
                    turnTimer.SetActive(false);
                    float a;
                    float v;
                    AIController.GetComponent<AIController>().AIturn(out a, out v);

                    playerFire(a + (Random.Range(-5.0f, 5.0f) * AIAbility),v + (Random.Range(-2.0f,2.0f) * AIAbility));

                }

            }
            if (currentState == GameState.player2turn)
            {

                forceBar.transform.position = new Vector2(playerTwo.prefab.transform.position.x, playerTwo.prefab.transform.position.y - 1.0f);
                angleBar.transform.position = new Vector2(playerTwo.prefab.transform.position.x - 0.8f, playerTwo.prefab.transform.position.y + 0.6f);
                angleBar.transform.localScale = new Vector2(0.5f, 0.5f);
                turnTimer.transform.position = new Vector2(playerTwo.prefab.transform.position.x, playerTwo.prefab.transform.position.y + 1.2f);
                currentState = GameState.player2force;

                if (AIMode) turnTimer.SetActive(true);
                turnTimer.GetComponent<turnTimer>().StartTimer(roundTimer);

                playerOne.prefab.GetComponent<animationController>().setAnim("idle");
                playerTwo.prefab.GetComponent<animationController>().setAnim("default");

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
                forceBar.transform.position = new Vector2(playerOne.prefab.transform.position.x, playerOne.prefab.transform.position.y - 1.0f);
                angleBar.transform.position = new Vector2(playerOne.prefab.transform.position.x + 0.8f, playerOne.prefab.transform.position.y + 0.6f);
                turnTimer.transform.position = new Vector2(playerOne.prefab.transform.position.x, playerOne.prefab.transform.position.y + 1.2f);
            }

            if (currentState == GameState.player2fire || currentState == GameState.player2force)
            {
                forceBar.transform.position = new Vector2(playerTwo.prefab.transform.position.x, playerTwo.prefab.transform.position.y - 1.0f);
                angleBar.transform.position = new Vector2(playerTwo.prefab.transform.position.x - 0.8f, playerTwo.prefab.transform.position.y + 0.6f);
                turnTimer.transform.position = new Vector2(playerTwo.prefab.transform.position.x, playerTwo.prefab.transform.position.y + 1.2f);
            }
        }

    }

    void GameSetup()
    {
        currentState = GameState.gameSetup;
        playerOne.score = 0;
        playerTwo.score = 0;


        playerOne.prefab.SetActive(true);
        playerTwo.prefab.SetActive(true);
        MainGameGUI.SetActive(true);
        generateCity();
        turnTimer.SetActive(true);

        
       
        forceBar.SetActive(true);
        angleBar.SetActive(false);
        playerFiring = false;
        playerAngling = false;
        Player1ScoreDisplay.GetComponent<Text>().text = 0.ToString();
        Player2ScoreDisplay.GetComponent<Text>().text = 0.ToString();
        
        Player1NameDisplay.GetComponent<Text>().text = playerOne.name;
        Player2NameDisplay.GetComponent<Text>().text = playerTwo.name;
        PreGameEnter();

    }

    public void GameStart()
    {

        currentState = GameState.gameSetup;
        menuCard.SetActive(false);
        setupScreen.SetActive(false);
        blurPanel.SetActive(false);
        scrollingCity.SetActive(false);
        if (AIMode) numberOfRounds = soloSetupScreen.transform.Find("RoundNumberSelectSolo").GetComponent<arrowSelects>().value;
       else numberOfRounds = setupScreen.transform.Find("RoundNumberSelect").GetComponent<arrowSelects>().value;

        roundTimer = setupScreen.transform.Find("TurnTimeSelect").GetComponent<arrowSelects>().value;
        currentRound = 1;
      
        FindObjectOfType<audioManager>().Play("Select");
        
        GameSetup();
        

    }

    public void PreGameEnter()
    {
        
        currentState = GameState.preRound;
        preRoundCards.GetComponent<PreRoundDisplay>().preRoundCalled(playerOne.name, playerTwo.name);
        

    }
    
    public void PreGameExit()
    {
        if (Random.Range(0, 2) == 1) currentState = GameState.player2turn;
        else currentState = GameState.player1turn;
        roundText.SetActive(true);
        roundText.GetComponent<roundText>().StartAnim(currentRound);
        resetAnims();

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
                StartCoroutine(launchDelay());
                break;

            case GameState.player2force:
                playerFiring = false;
                currentState = GameState.player2angle;
                StartCoroutine(angleGUI());
                break;

            case GameState.player2angle:
                playerAngling = false;
                currentState = GameState.player2fire;
                StartCoroutine(launchDelay());
                break;

            case GameState.titleCard:
                currentState = GameState.mainMenu;
                titleCard.SetActive(false);
                menuCard.SetActive(true);
                blurPanel.SetActive(true);


                break;
                


        }
    }

    /*public void playerFire(float angleInput, float forceInput)
    {
        if (currentState == GameState.player1fire)
        {

            playerOne.prefab.GetComponent<playerScript>().StartInv();
            
            GameObject banana = Instantiate(bananaPrefab, playerOne.prefab.transform.position, Quaternion.identity);
           // vertSpeed = Mathf.Sin((angle * Mathf.PI) / 180) * velocity;
           // horzSpeed = Mathf.Cos((angle * Mathf.PI) / 180) * velocity;
            banana.GetComponent<banana>().vertSpeed = Mathf.Sin((angleInput * Mathf.PI) / 180) * forceInput;
            banana.GetComponent<banana>().horzSpeed = Mathf.Cos((angleInput * Mathf.PI) / 180) * forceInput;
            // banana.GetComponent<banana>().angle = angleInput;
            // banana.GetComponent<banana>().velocity = forceInput;
            banana.GetComponent<banana>().targetID = 2;
            turnTimer.GetComponent<turnTimer>().StopTimer();
            playerOne.prefab.GetComponent<animationController>().setAnim("throw");



        }

        if (currentState == GameState.player2fire)
        {
            playerTwo.prefab.GetComponent<playerScript>().StartInv();
            GameObject banana = Instantiate(bananaPrefab, playerTwo.prefab.transform.position, Quaternion.Euler(0, 180f, 0));
            banana.GetComponent<banana>().vertSpeed = Mathf.Sin((angleInput * Mathf.PI) / 180) * forceInput;
            banana.GetComponent<banana>().horzSpeed = Mathf.Cos((angleInput * Mathf.PI) / 180) * forceInput;
           // banana.GetComponent<banana>().angle = -angleInput;
           // banana.GetComponent<banana>().velocity = -forceInput;
            banana.GetComponent<banana>().targetID = 1;
            turnTimer.GetComponent<turnTimer>().StopTimer();
            playerTwo.prefab.GetComponent<animationController>().setAnim("throw");

        }
        FindObjectOfType<audioManager>().Play("BananaThrown");

        // StartCoroutine(turnTimer());


    }*/


    public void playerFire(float angleInput, float forceInput)
    {


        if (currentState == GameState.player1fire && AIMode == false)
        {
            playerOne.prefab.GetComponent<playerScript>().PlayerFire(angleInput, forceInput, 1);

        }

        if (currentState == GameState.player1fire && AIMode == true)
        {
            playerOne.prefab.GetComponent<AI>().AIFire(angleInput, forceInput);

        }


        if (currentState == GameState.player2fire)
        {
            playerTwo.prefab.GetComponent<playerScript>().PlayerFire(angleInput, forceInput, 2);

        }
        turnTimer.GetComponent<turnTimer>().StopTimer();
            



        
        FindObjectOfType<audioManager>().Play("BananaThrown");

        // StartCoroutine(turnTimer());


    }

    public void playerHit(int playerID)
    {
        if (playerID == 2)
        {
            
            playerTwo.prefab.SetActive(false);
            //Instantiate(explosion, player2.transform.position, transform.rotation);
            PlayerExplosion(playerTwo.prefab.transform.position.x, playerTwo.prefab.transform.position.y);
           
            
                currentState = GameState.player1win;
                playerOne.score++;
           
            
        }
        else
        {
            playerOne.prefab.SetActive(false);
            //Instantiate(explosion, player1.transform.position, transform.rotation);
            PlayerExplosion(playerOne.prefab.transform.position.x, playerOne.prefab.transform.position.y);
           
                currentState = GameState.player2win;
                playerTwo.score++;
           
        }
        
        Debug.Log("Player Hit");

        TurnOver();
    }

    public void generateCity()
    {
        playerOne.prefab.transform.position = new Vector2(-8.5f, -3.5f);
        playerTwo.prefab.transform.position = new Vector2(8.5f, -3.5f);
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
                buildings[i, j].GetComponent<SpriteRenderer>().sprite = buildingBlocks[0];

                buildingHeights[i]++;

            }

            // buildings[i,buildingHeights[i]-1].GetComponent<SpriteRenderer>().sprite = buildingBlocks[1];
            for (int k = 0; k < Random.Range(2,(buildingHeights[i] / 2)); k++)
            {
                buildings[i, buildingHeights[i] - 1 - k].GetComponent<SpriteRenderer>().sprite = buildingBlocks[Random.Range(1,4)];
                buildings[i, buildingHeights[i] - 2 - k].GetComponent<SpriteRenderer>().sprite = buildingBlocks[Random.Range(4,6)];
            }


        }
        int player1offset = Random.Range(1, 5);
        int player2offset = Random.Range(1, 5);
        Debug.Log(player1offset);
        playerOne.prefab.transform.position = new Vector2(playerOne.prefab.transform.position.x + player1offset, playerOne.prefab.transform.position.y + (buildingHeights[1 + player1offset] * 0.5f - 1.0f));
        playerTwo.prefab.transform.position = new Vector2(playerTwo.prefab.transform.position.x - player2offset, playerTwo.prefab.transform.position.y + (buildingHeights[18 - player2offset] * 0.5f - 1.0f));
        
        
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
        playerOne.prefab.SetActive(true);
        playerTwo.prefab.SetActive(true);
        
        generateCity();
        Player1ScoreDisplay.GetComponent<Text>().text = playerOne.score.ToString();
        Player2ScoreDisplay.GetComponent<Text>().text = playerTwo.score.ToString();
        roundText.GetComponent<roundText>().StartAnim(currentRound);
        currentState = GameState.player1turn;
        


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
                winPanel.GetComponent<winCaption>().showPanel(playerTwo.name);
                playerTwo.prefab.GetComponent<animationController>().setAnim("celebrate");
                StartCoroutine(winTimer());

            }
        }
        else
        {
            Debug.Log("Win State P1");
            winPanel.GetComponent<winCaption>().showPanel(playerOne.name);
            playerOne.prefab.GetComponent<animationController>().setAnim("celebrate");
            StartCoroutine(winTimer());
        }
    }

    public IEnumerator winTimer()
    {
        yield return new WaitForSeconds(2.0f);
        winPanel.GetComponent<winCaption>().hidePanel();
        currentRound++;
        if (currentRound <= numberOfRounds) resetLevel();

        else callWinPanel();


    }

    public void callWinPanel()
    {
        gameEndedCard.SetActive(true);
        gameEndedCard.GetComponent<endOfGame>().showScores(playerOne.name, playerOne.score, playerTwo.name, playerTwo.score);
        MainGameGUI.SetActive(false);
    }

    public IEnumerator TurnEndTimer()
    {
        yield return new WaitForSeconds(0.75f);
       
       



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





    public void resetLevel()
    {
        //Debug.Log("Restarting level");
        //Application.LoadLevel(Application.loadedLevel);
        resetAnims();
        resetCity();
    }


    public void resetAnims()
    {
        playerOne.prefab.GetComponent<animationController>().setAnim("default");
        playerTwo.prefab.GetComponent<animationController>().setAnim("default");
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

    public void VSmatchSetup()
    {

        setupScreen.SetActive(true);
        menuCard.SetActive(false);
        AIMode = false;
    }

    public void soloMatchSetup()
    {

        soloSetupScreen.SetActive(true);
        menuCard.SetActive(false);
        
    }

    public void StartOnePlayer()
        {
        soloSetupScreen.SetActive(false);
        numberOfRounds = soloSetupScreen.transform.Find("RoundNumberSelectSolo").GetComponent<arrowSelects>().value;
        AIMode = true;
        GameStart();
    }

    public void ExitToMainMenuButton()
    {
        currentState = GameState.mainMenu;
        roundText.SetActive(false);

        FindObjectOfType<audioManager>().Play("Select");
        playerOne.prefab.transform.position = new Vector2(-8.5f, -3.5f);
        playerTwo.prefab.transform.position = new Vector2(8.5f, -3.5f);
        pausePanel.SetActive(false);
        playerOne.prefab.SetActive(false);
        playerTwo.prefab.SetActive(false);
        MainGameGUI.SetActive(false);
        turnTimer.SetActive(false);
        menuCard.SetActive(true);
        optionsCard.SetActive(false);
        forceBar.SetActive(false);
        angleBar.SetActive(false);
        forceCounter = 0;
        angleCounter = 0;
        bananaTemp = GameObject.FindWithTag("Banana");
        Destroy(bananaTemp);
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
        gameEndedCard.SetActive(false);

        scrollingCity.SetActive(true);
        blurPanel.SetActive(true);


    }

    public void TurnTimerUp()
    {
       // turnTimeLeft = false;
        if (currentState == GameState.player1force || currentState == GameState.player1angle) currentState = GameState.player1fire;
        if (currentState == GameState.player2force || currentState == GameState.player2angle) currentState = GameState.player2fire;
        playerFiring = false;
        FindObjectOfType<audioManager>().Play("timesUp");
        StartCoroutine(turnoverDelay());
        


    }

    public IEnumerator launchDelay()
    {

        yield return new WaitForSeconds(0.1f);
        playerFire(angleCounter, forceCounter);


    }

    public IEnumerator turnoverDelay()
    {
        yield return new WaitForSeconds(1.0f);
        TurnOver();
    }

    public float GetZoneHeight(int zone)
    {

        return buildingHeights[zone + 1] / 2;

    }

}
