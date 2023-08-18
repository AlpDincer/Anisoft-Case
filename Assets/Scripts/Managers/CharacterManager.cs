using Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    public GameObject playerChar;
    public GameObject cuttingBoardMarker;
    public GameObject panMarker;
    public GameObject table0Marker;
    public GameObject table1Marker;
    public GameObject table2Marker;
    public GameObject table3Marker;
    public GameObject table4Marker;
    public GameObject table5Marker;

    public GameObject cuttingBoard;
    public GameObject pan;

    private GameObject moveTarget;
    private GameObject lookTarget;
    public GameObject currentMarker;
    public GameObject playerHiddenMarker;

    private bool orderReceived = false;
    private bool slicesReady = false;
    private bool orderReady = false;

    private bool isHiddenCrossed = false;
    private bool needCross = false;

    bool isWalking = false;
    public float speed = 1.0f;

    Animator myAnimator;

    void Start()
    {
        playerChar.transform.position = panMarker.transform.position;
        myAnimator = playerChar.GetComponent<Animator>();
    }

    public bool isCrossNeeded()
    {
        if(currentMarker == panMarker || currentMarker == cuttingBoardMarker) 
        {
            if (moveTarget == panMarker)
            { return false; }

            else if(moveTarget == cuttingBoardMarker)
            { return false; }

            else { return true; }
        }
        else if(currentMarker != panMarker || currentMarker != cuttingBoardMarker)
        {
            if (moveTarget == panMarker)
            { return true; }
            
            else if (moveTarget == cuttingBoardMarker)
            { return true; }

            else { return false; }
        }
        else { return false; }
    }
    public void HandleMovement(string name)
    {
        switch (name)
        {
            case "CuttingBoardMarker":               
                moveTarget = cuttingBoardMarker;
                needCross = isCrossNeeded();
                lookTarget = cuttingBoard;
                playerChar.transform.LookAt(moveTarget.transform);
                isWalking = true;
                myAnimator.SetBool("Walk", true);
                //GameManager.Instance.SpawnVegetable();
                break;
            case "PanMarker":
                moveTarget = panMarker;
                needCross = isCrossNeeded();
                lookTarget = pan;
                playerChar.transform.LookAt(moveTarget.transform);
                isWalking = true;
                myAnimator.SetBool("Walk", true);
                //GameManager.Instance.SpawnVegetable();
                break;
            case "Table0Marker":
                moveTarget = table0Marker;
                needCross = isCrossNeeded();
                lookTarget = GameManager.Instance.gameData.TableList[0].tableModel;
                playerChar.transform.LookAt(moveTarget.transform);
                if (GameManager.Instance.isTableFree(lookTarget) == false)
                {
                    isWalking = true;
                    myAnimator.SetBool("Walk", true);
                }
                break;
            case "Table1Marker":
                moveTarget = table1Marker;
                needCross = isCrossNeeded();
                lookTarget = GameManager.Instance.gameData.TableList[1].tableModel;
                playerChar.transform.LookAt(moveTarget.transform);
                if (GameManager.Instance.isTableFree(lookTarget) == false)
                {
                    isWalking = true;
                    myAnimator.SetBool("Walk", true);
                }
                break;
            case "Table2Marker":
                moveTarget = table2Marker;
                needCross = isCrossNeeded();
                lookTarget = GameManager.Instance.gameData.TableList[2].tableModel;
                playerChar.transform.LookAt(moveTarget.transform);
                if (GameManager.Instance.isTableFree(lookTarget) == false)
                {
                    isWalking = true;
                    myAnimator.SetBool("Walk", true);
                }
                break;
            case "Table3Marker":
                moveTarget = table3Marker;
                needCross = isCrossNeeded();
                lookTarget = GameManager.Instance.gameData.TableList[3].tableModel;
                playerChar.transform.LookAt(moveTarget.transform);
                if (GameManager.Instance.isTableFree(lookTarget) == false)
                {
                    isWalking = true;
                    myAnimator.SetBool("Walk", true);
                }
                break;
            case "Table4Marker":
                moveTarget = table4Marker;
                needCross = isCrossNeeded();
                lookTarget = GameManager.Instance.gameData.TableList[4].tableModel;
                playerChar.transform.LookAt(moveTarget.transform);
                if (GameManager.Instance.isTableFree(lookTarget) == false)
                {
                    isWalking = true;
                    myAnimator.SetBool("Walk", true);
                }
                break;
            case "Table5Marker":
                moveTarget = table5Marker;
                needCross = isCrossNeeded();
                lookTarget = GameManager.Instance.gameData.TableList[5].tableModel;
                playerChar.transform.LookAt(moveTarget.transform);
                if (GameManager.Instance.isTableFree(lookTarget) == false)
                {
                    isWalking = true;
                    myAnimator.SetBool("Walk", true);
                }
                break;
            
        }
    }
    void Update()
    {
        if(isWalking == true)
        {
            var step = speed * Time.deltaTime;
            if(playerChar.transform.position != playerHiddenMarker.transform.position && isHiddenCrossed == false && needCross == true)
            {
                playerChar.transform.LookAt(playerHiddenMarker.transform);
                playerChar.transform.position = Vector3.MoveTowards(playerChar.transform.position, playerHiddenMarker.transform.position, step);
            }

            if(playerChar.transform.position == playerHiddenMarker.transform.position)
            {
                isHiddenCrossed = true;
            }

            if (isHiddenCrossed == true || needCross == false)
            {
                playerChar.transform.LookAt(moveTarget.transform);
                playerChar.transform.position = Vector3.MoveTowards(playerChar.transform.position, moveTarget.transform.position, step); 
            }
           
            if(playerChar.transform.position == moveTarget.transform.position)
            {
                currentMarker = moveTarget;
                isHiddenCrossed = false;
                needCross = false;
                isWalking = false;
                myAnimator.SetBool("Walk", false);
                Vector3 targetPostition = new Vector3(lookTarget.transform.position.x,
                                       playerChar.transform.position.y,
                                       lookTarget.transform.position.z);
                playerChar.transform.LookAt(targetPostition);
                currentMarker = moveTarget;

                if(moveTarget.name != "CuttingBoardMarker" && moveTarget.name != "PanMarker" && orderReceived == false)
                {
                    GameManager.Instance.GetIngredients(lookTarget);
                    orderReceived = true;
                }

                else if(moveTarget.name == "CuttingBoardMarker" && orderReceived == true && slicesReady == false)
                {
                    GameManager.Instance.CutVegetable();
                    slicesReady = true;
                }

                else if(moveTarget.name == "PanMarker" && slicesReady == true && orderReady == false)
                {
                    GameManager.Instance.CookVegetable(playerChar);
                    orderReady = true;
                }

                else if (moveTarget.name != "CuttingBoardMarker" && moveTarget.name != "PanMarker" && orderReady == true)
                {
                    GameManager.Instance.DeliverOrder(playerChar, lookTarget);
                    orderReceived = false;
                    slicesReady = false;
                    orderReady = false;
                }

                else { }
            }
        }
    }
}
