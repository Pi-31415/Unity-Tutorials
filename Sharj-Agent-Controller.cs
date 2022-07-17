using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    // All interaction Objects
    public GameObject carrot;

    public GameObject knife;

    public GameObject choppingBoard;

    public GameObject bowl;

    public GameObject milkBottle;

    public GameObject spoonLeft;

    public GameObject spoonRight;

    public GameObject tray;

    public GameObject lid;

    public GameObject ladle;

    public GameObject potato;

    public GameObject peeler;

    // Markers
    public GameObject marker1;

    public GameObject marker2;

    public GameObject marker3;

    public GameObject marker4;

    public GameObject marker5;

    public GameObject marker6;

    public GameObject marker7;

    // Objects on the table
    public GameObject MicrowaveDoorClosed;

    public GameObject MicrowaveDoorOpen;

    public GameObject MicrowaveFoodInside;

    public GameObject OvenDoorClosed;

    public GameObject OvenDoorOpened;

    public GameObject CookieIn;

    public GameObject CookieUp;

    public GameObject LidOutside;

    public GameObject LadleOutside;

    public GameObject PotatoOutside;

    public GameObject PeelerOutside;

    public GameObject Spoon1Outside;

    public GameObject Spoon2Outside;

    public GameObject CarrotOutside;

    public GameObject KnifeOutside;

    public GameObject ChopperOutside;

    public GameObject MilkOutside;

    // UI Text
    public TextMeshProUGUI textmeshPro;

    private Animator animComp;

    Rigidbody m_Rigidbody;

    // Moving Speed
    float m_Speed;

    // Rotation Speed
    float r_Speed;

    // Variables for move and rotation
    private float

            move,
            rotation;

    // Start is called before the first frame update
    void Start()
    {
        // Configure the external objects
        MicrowaveDoorClosed.SetActive(true);
        MicrowaveDoorOpen.SetActive(false);
        MicrowaveFoodInside.SetActive(false);
        CookieUp.SetActive(false);
        OvenDoorClosed.SetActive(true);
        OvenDoorOpened.SetActive(false);

        // Set Up Animation
        HideAllEquipments();
        animComp = this.GetComponent<Animator>();

        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();

        //Set the moving speed of the GameObject
        m_Speed = 1.5f;

        // Set the rotating speedo of the GameObject
        r_Speed = 100.0f;
        textmeshPro.SetText("No Action.");
    }

    // Update is called once per frame
    void Update()
    {
        // Exit
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        // Keep Upright
        transform.rotation =
            Quaternion.Euler(0, transform.rotation.eulerAngles[1], 0);

        // Set Rotation
        // Set Movement and Rotation Speeds
        move = Input.GetAxis("Vertical") * m_Speed * Time.deltaTime;
        rotation = Input.GetAxis("Horizontal") * r_Speed * Time.deltaTime;

        /*---------------------------------------------------*/
        /* Major Actions */
        /*---------------------------------------------------*/
        if (
            (
            (
            Input.GetKeyUp(KeyCode.LeftArrow) ||
            Input.GetKeyUp(KeyCode.RightArrow)
            ) &&
            (Input.GetAxis("Vertical") == 0.0f)
            ) ||
            (Input.GetKeyUp(KeyCode.UpArrow))
        )
        {
            HideAllEquipments();

            // Go back to Idle state
            animComp.SetInteger("AgentState", 0);
            textmeshPro.SetText("No Action.");
        }
        else if (
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow)
        )
        {
            // Forward Animation
            animComp.SetInteger("AgentState", 1);
            textmeshPro.SetText("Walk.");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            hideallmarkers();

            // Microwaving
            transform.position =
                new Vector3(-2.257992f, 0.005001321f, 6.460995f);
            transform.rotation = Quaternion.Euler(0, -84.699f, 0);

            // Activate Objects
            bowl.SetActive(true);

            // Play Animation
            animComp.SetInteger("AgentState", 2);
            textmeshPro.SetText("Microwaving.");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            hideallmarkers();

            // Chopping
            transform.position = new Vector3(0.378f, 0.138f, 8.097f);
            transform.rotation = Quaternion.Euler(0, 1.142f, 0);

            // Activate Objects
            // Play Animation
            animComp.SetInteger("AgentState", 3);
            textmeshPro.SetText("Chopping.");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            hideallmarkers();

            transform.position = new Vector3(3.78f, 0.138f, 3.89f);
            transform.rotation = Quaternion.Euler(0, 52.017f, 0);

            // Play Animation
            animComp.SetInteger("AgentState", 4);
            textmeshPro.SetText("Pouring Milk.");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            hideallmarkers();

            transform.position =
                new Vector3(-0.8689948f, 0.005000159f, 8.194068f);
            transform.rotation = Quaternion.Euler(0, 1.054f, 0);

            // Play Animation
            animComp.SetInteger("AgentState", 5);
            textmeshPro.SetText("Make Salad.");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            // Use Oven
            transform.position =
                new Vector3(-2.286786f, 0.005000234f, 7.291577f);
            transform.rotation = Quaternion.Euler(0, -98.449f, 0);
            CookieUp.SetActive(false);
            OvenDoorClosed.SetActive(true);
            OvenDoorOpened.SetActive(false);
            hideallmarkers();

            // Play Animation
            animComp.SetInteger("AgentState", 6);
            textmeshPro.SetText("Oven Use.");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            hideallmarkers();

            // Update Location and Rotation
            transform.position =
                new Vector3(-2.680021f, 0.005000204f, 7.803108f);
            transform.rotation = Quaternion.Euler(0, -91.403f, 0);

            // Play Animation
            animComp.SetInteger("AgentState", 7);
            textmeshPro.SetText("Cook Soup.");
        }
        else if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            hideallmarkers();

            // Update Location and Rotation
            transform.position = new Vector3(-2.197f, 0.005000204f, 7.89f);
            transform.rotation = Quaternion.Euler(0, 19.516f, 0);

            // Play Animation
            animComp.SetInteger("AgentState", 8);
            textmeshPro.SetText("Peel Potato.");
        }

        // Pause all animations
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            pauseAnimation();
        }

        // Replay all animations
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playAnimation();
        }
    }

    private void hideallmarkers()
    {
        marker1.SetActive(false);
        marker2.SetActive(false);
        marker3.SetActive(false);
        marker4.SetActive(false);
        marker5.SetActive(false);
        marker6.SetActive(false);
        marker7.SetActive(false);
    }

    private void showallmarkers()
    {
        marker1.SetActive(true);
        marker2.SetActive(true);
        marker3.SetActive(true);
        marker4.SetActive(true);
        marker5.SetActive(true);
        marker6.SetActive(true);
        marker7.SetActive(true);
    }

    private void HideAllEquipments()
    {
        carrot.SetActive(false);
        knife.SetActive(false);
        choppingBoard.SetActive(false);
        bowl.SetActive(false);
        milkBottle.SetActive(false);
        spoonLeft.SetActive(false);
        spoonRight.SetActive(false);
        tray.SetActive(false);
        lid.SetActive(false);
        ladle.SetActive(false);
        potato.SetActive(false);
        peeler.SetActive(false);
    }

    // Animation pause play
    private void pauseAnimation()
    {
        animComp.speed = 0.0f;

        //Set the moving speed of the GameObject
        m_Speed = 0.0f;

        // Set the rotating speedo of the GameObject
        r_Speed = 0.0f;
    }

    private void playAnimation()
    {
        animComp.speed = 1.0f;

        //Set the moving speed of the GameObject
        m_Speed = 1.0f;

        // Set the rotating speedo of the GameObject
        r_Speed = 100.0f;
    }

    // Main Movement Function
    private void LateUpdate()
    {
        transform.Translate(0f, 0f, move);
        transform.Rotate(0f, rotation, 0f);
    }

    // Mini Actions
    // -------------------------------------------------
    // Microwaving
    public void MicrowaveReach()
    {
        textmeshPro.SetText("Reach the Microwave.");
        pauseAnimation();
    }

    public void MicrowaveOpen()
    {
        textmeshPro.SetText("Open the Microwave.");
        pauseAnimation();
    }

    public void MicrowavePlace()
    {
        MicrowaveDoorClosed.SetActive(false);
        MicrowaveDoorOpen.SetActive(true);
        textmeshPro.SetText("Place food.");
        pauseAnimation();
    }

    public void MicrowaveClose()
    {
        textmeshPro.SetText("Close microwave.");
        MicrowaveFoodInside.SetActive(true);
        pauseAnimation();
        bowl.SetActive(false);
    }

    public void MicrowavePress()
    {
        MicrowaveDoorClosed.SetActive(true);
        MicrowaveDoorOpen.SetActive(false);
        textmeshPro.SetText("Press the Microwave button.");
        pauseAnimation();
    }

    public void MicrowaveSpin()
    {
        textmeshPro.SetText("Spin the Microwave timer.");
        pauseAnimation();
    }

    public void MicrowavePush2()
    {
        MicrowaveFoodInside.SetActive(false);
        textmeshPro.SetText("Press the Microwave start button.");
        pauseAnimation();
    }

    public void MicrowaveNull()
    {
        textmeshPro.SetText("Wait for food.");
        pauseAnimation();
    }

    public void MicrowaveDone()
    {
        showallmarkers();
        HideAllEquipments();
        textmeshPro.SetText("Microwaving Done.");
        animComp.SetInteger("AgentState", 0);
        pauseAnimation();
    }

    // Chop Vegetables
    public void CutReach()
    {
        textmeshPro.SetText("Reach the Carrot.");
        pauseAnimation();
    }

    public void CutCarrot()
    {
        knife.SetActive(true);
        carrot.SetActive(true);
        choppingBoard.SetActive(true);
        CarrotOutside.SetActive(false);
        KnifeOutside.SetActive(false);
        ChopperOutside.SetActive(false);
        textmeshPro.SetText("Cut the Carrot.");
        pauseAnimation();
    }

    public void CutPlace()
    {
        textmeshPro.SetText("Place the knife on table.");
        pauseAnimation();
    }

    public void ChoppingDone()
    {
        CarrotOutside.SetActive(true);
        KnifeOutside.SetActive(true);
        ChopperOutside.SetActive(true);
        showallmarkers();
        HideAllEquipments();
        textmeshPro.SetText("Chopping Done.");
        animComp.SetInteger("AgentState", 0);
        pauseAnimation();
    }

    // Pour Milk
    public void PourTakeMilkPre()
    {
        textmeshPro.SetText("Reach the Milk Bottle.");
        pauseAnimation();
    }

    public void PourTakeMilk()
    {
        MilkOutside.SetActive(false);
        milkBottle.SetActive(true);
        textmeshPro.SetText("Reach the Milk Bottle.");
    }

    public void PourOpen()
    {
        textmeshPro.SetText("Open the Milk Bottle.");
        pauseAnimation();
    }

    public void PourPour()
    {
        textmeshPro.SetText("Pour the Milk Bottle.");
        pauseAnimation();
    }

    public void PourClose()
    {
        textmeshPro.SetText("Close the Milk Bottle.");
        pauseAnimation();
    }

    public void PourDone()
    {
        showallmarkers();
        MilkOutside.SetActive(true);
        HideAllEquipments();
        textmeshPro.SetText("Pouring Milk Done.");
        animComp.SetInteger("AgentState", 0);
        pauseAnimation();
    }

    // Salad
    public void SaladReachSpoon()
    {
        textmeshPro.SetText("Reach the spoons.");
        pauseAnimation();
    }

    public void SaladStir()
    {
        Spoon1Outside.SetActive(false);
        Spoon2Outside.SetActive(false);
        spoonLeft.SetActive(true);
        spoonRight.SetActive(true);
        textmeshPro.SetText("Stir the salad.");
        pauseAnimation();
    }

    public void SaladPutOffSpoon()
    {
        textmeshPro.SetText("Place Spoons Back.");
        pauseAnimation();
    }

    public void SaladDone()
    {
        Spoon1Outside.SetActive(true);
        Spoon2Outside.SetActive(true);
        showallmarkers();

        HideAllEquipments();

        animComp.SetInteger("AgentState", 0);
        pauseAnimation();
    }

    // Oven Tray
    public void OvenReach()
    {
        textmeshPro.SetText("Reach the oven door.");
        pauseAnimation();
    }

    public void OvenOpen()
    {
        textmeshPro.SetText("Open the oven door.");
        pauseAnimation();
    }

    public void OvenReachTray()
    {
        OvenDoorClosed.SetActive(false);
        OvenDoorOpened.SetActive(true);
        transform.rotation = Quaternion.Euler(0, -98.449f, 0);
        textmeshPro.SetText("Reach the cookie tray.");
        pauseAnimation();
    }

    public void OvenPullTray()
    {
        tray.SetActive(true);
        transform.rotation = Quaternion.Euler(0, -98.449f, 0);
        textmeshPro.SetText("Pull the cookie tray out.");
        pauseAnimation();
    }

    public void OvenPlaceTray()
    {
        transform.rotation = Quaternion.Euler(0, -98.449f, 0);
        textmeshPro.SetText("Place the cookie tray on the top of oven.");
        pauseAnimation();
    }

    public void OvenDropTray()
    {
        textmeshPro.SetText("Release the cookie tray.");
        pauseAnimation();
    }

    public void OvenShakeHand()
    {
        CookieUp.SetActive(true);
        tray.SetActive(false);
        textmeshPro.SetText("Shake your fingers.");
        pauseAnimation();
    }

    public void ReachOvenDoor2()
    {
        textmeshPro.SetText("Reach the Oven door.");
        pauseAnimation();
    }

    public void OvenCloseDoor2()
    {
        textmeshPro.SetText("Close the Oven door.");
        pauseAnimation();
    }

    public void OvenDone()
    {
        OvenDoorClosed.SetActive(true);
        OvenDoorOpened.SetActive(false);
        showallmarkers();
        HideAllEquipments();
        textmeshPro.SetText("Oven Done.");
        animComp.SetInteger("AgentState", 0);
        pauseAnimation();
    }

    // Cooking Soup
    public void CookReachLid()
    {
        textmeshPro.SetText("Reach the pot lid.");
        pauseAnimation();
    }

    public void CookOpenLid()
    {
        textmeshPro.SetText("Open the pot lid.");
        pauseAnimation();
    }

    public void CookPickLadle()
    {
        lid.SetActive(true);
        LidOutside.SetActive(false);
        textmeshPro.SetText("Pick up the ladle.");
        pauseAnimation();
    }

    public void CookLiftLadle()
    {
        textmeshPro.SetText("Lift the ladle.");
        pauseAnimation();
    }

    public void CookStirSoup()
    {
        LadleOutside.SetActive(false);
        ladle.SetActive(true);
        textmeshPro.SetText("Stir the soup.");
        pauseAnimation();
    }

    public void CookReachMouth()
    {
        textmeshPro.SetText("Drink the soup.");
        pauseAnimation();
    }

    public void CookStirSoup2()
    {
        textmeshPro.SetText("Stir the soup");
        pauseAnimation();
    }

    public void CookPlaceLadle()
    {
        textmeshPro.SetText("Place the ladle down.");
        pauseAnimation();
    }

    public void CookReleaseLadle()
    {
        LadleOutside.SetActive(true);
        ladle.SetActive(false);
        textmeshPro.SetText("Release the ladle.");
        pauseAnimation();
    }

    public void CookCloseLid()
    {
        textmeshPro.SetText("Close the lid.");
        pauseAnimation();
    }

    public void CookingDone()
    {
        LidOutside.SetActive(true);
        showallmarkers();
        HideAllEquipments();
        textmeshPro.SetText("Cooking done.");
        animComp.SetInteger("AgentState", 0);
        pauseAnimation();
    }

    // Peeling Potato
    public void PeelReachPotato()
    {
        textmeshPro.SetText("Reach the potato.");
        pauseAnimation();
    }

    public void PeelReachKnife()
    {
        PotatoOutside.SetActive(false);
        potato.SetActive(true);
        textmeshPro.SetText("Reach the peeler.");
        pauseAnimation();
    }

    public void PeelPeel()
    {
        peeler.SetActive(true);
        PeelerOutside.SetActive(false);
        textmeshPro.SetText("Peel the potato.");
        pauseAnimation();
    }

    public void PeelFlip()
    {
        textmeshPro.SetText("Flip the potato.");
        pauseAnimation();
    }

    public void PeelPeel2()
    {
        textmeshPro.SetText("Peel the potato.");
        pauseAnimation();
    }

    public void PeelFlip2()
    {
        textmeshPro.SetText("Flip the potato.");
        pauseAnimation();
    }

    public void PeelPeel3()
    {
        textmeshPro.SetText("Peel the potato.");
        pauseAnimation();
    }

    public void PeelFlip4()
    {
        textmeshPro.SetText("Flip the potato.");
        pauseAnimation();
    }

    public void PeelPeel5()
    {
        textmeshPro.SetText("Peel the potato.");
        pauseAnimation();
    }

    public void PeelRelease()
    {
        PeelerOutside.SetActive(true);
        PotatoOutside.SetActive(true);
        showallmarkers();
        HideAllEquipments();
        textmeshPro.SetText("Peeling Done.");
        animComp.SetInteger("AgentState", 0);
        pauseAnimation();
    }
}
