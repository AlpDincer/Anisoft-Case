using Scripts.Data;
using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    public ScriptableTemplateData gameData;
    public ScriptableTemplateData testData;

    public GameObject customerSpawnPoint;
    public GameObject cuttingBoard;

    private List<string> unlockedVegetableList;
    private List<GameObject> orderedVegetableList;
    private List<GameObject> slicedVegetableList;
    public List<GameObject> tempCustomerList;
    private GameObject customer;

    void Start()
    {
        //gameData = testData;
        for (int i = 0; i < gameData.TableList.Count; i++) //onceden kalan current customerlari resetle
        {
            gameData.TableList[i].currentCustomerName = "";
        }

        unlockedVegetableList = new List<string>();
        orderedVegetableList = new List<GameObject>();
        slicedVegetableList = new List<GameObject>();

        Vector3 vec = new Vector3(0,0.15f,0);
        
        //SpawnManager.Instance.Spawn(vec += customerSpawnPoint.transform.localPosition, "Customer");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                CharacterManager.Instance.HandleMovement(hit.collider.name);
            }

        }

        if(FindEmptyTable() == true)
        {
            SpawnCustomer();
        }
    }

    [ContextMenu("SpawnCustomer")]
    public void SpawnCustomer()
    {
        Vector3 vec = new Vector3(0, 0.15f, 0);
        customer = SpawnManager.Instance.Spawn(vec += customerSpawnPoint.transform.localPosition, "Customer");
        tempCustomerList.Add(customer);

        customer.GetComponent<CustomerScript>().tempTable = FindEmptyTable(); // customer her spawnda yapilicaklar
        TableTaken(customer.GetComponent<CustomerScript>().tempTable, customer.name);
        customer.GetComponent<CustomerScript>().GoToTable();

        if (CustomerBubbleScript.bubbleVegetableList == null)
        { 
            CustomerBubbleScript.bubbleVegetableList = GetUnlockedVegetableNames(); 
        }
        customer.transform.GetChild(0).GetComponent<CustomerBubbleScript>().orderedVegetableList = new List<GameObject>(); // customer bubble her spawnda yapilicaklar
        customer.transform.GetChild(0).GetComponent<CustomerBubbleScript>().DecideMeal(); 
    }

    [ContextMenu("ResetData")]
    public void ResetData()
    {
        for (int i = 0; i < gameData.TableList.Count; i++)
        {
            gameData.TableList[i].isFree = true;
        }

        for (int i = 1; i < gameData.TableList.Count; i++)
        {
            gameData.TableList[i].isUnlocked = false;
        }

        for (int i = 1; i < gameData.VegetableList.Count; i++)
        {
            gameData.VegetableList[i].isUnlocked = false;
        }

        gameData.currentCoin = 80;
        gameData.isPlayed = false;
    }

    public int GetCoin() //UIManager'a at
    {
        return gameData.currentCoin;
    }

    public void AddCoin(int gainedCoin)
    {
        gameData.currentCoin += gainedCoin;
    }

    public bool isTableFree(GameObject tableModel)
    {
        for (int i = 0; i < gameData.TableList.Count; i++)
        {
            if(gameData.TableList[i].tableModel == tableModel)
            {
                return gameData.TableList[i].isFree;
            }
        }

        return false;
    }

    public GameObject FindEmptyTable()
    {
        for (int i = 0; i < gameData.TableList.Count; i++)
        {
            if (gameData.TableList[i].isUnlocked == true && gameData.TableList[i].isFree == true)
            {
                return gameData.TableList[i].tableModel;
            }

            else { }
                
        }

        return null;
    }

    public void FreeTable(GameObject tableModel)
    {
        for (int i = 0; i < gameData.TableList.Count; i++)
        {
            if(gameData.TableList[i].tableModel == tableModel)
            {
                gameData.TableList[i].isFree = true;
            }
        }
    }

    public void TableTaken(GameObject table, string customerName)
    {
        for (int i = 0; i < gameData.TableList.Count; i++)
        {
            if (gameData.TableList[i].tableModel == table)
            {
                gameData.TableList[i].currentCustomerName = customerName;
                gameData.TableList[i].isFree = false;
            }
        }
    }

    public List<string> GetUnlockedVegetableNames()
    {
        for (int i = 0; i < gameData.VegetableList.Count; i++)
        {
            if (gameData.VegetableList[i].isUnlocked == true)
            {
                unlockedVegetableList.Add(gameData.VegetableList[i].vegetableName);
            }

            else { }
        }

        return unlockedVegetableList;
    }

    public Vegetable GetVegetable(string name)
    {
        for (int i = 0; i < gameData.VegetableList.Count; i++)
        {
            if(gameData.VegetableList[i].vegetableName == name)
            {
                return gameData.VegetableList[i];
            }
        }

        return null;
    }

    public Table GetTable(int number)
    {
        for (int i = 0; i < gameData.TableList.Count; i++)
        {
            if (gameData.TableList[i].tableNumber == number)
            {
                return gameData.TableList[i];
            }
        }

        return null;
    }

    #region Game Mechanics
    public void GetIngredients(GameObject table)
    {
        Vector3 tempVec = new Vector3(0.1f, 0, 0);

        for (int i = 0; i < gameData.TableList.Count; i++)
        {
            if(gameData.TableList[i].tableModel == table)
            {
                for (int j = 0; j < tempCustomerList.Count; j++)
                {
                    if(tempCustomerList[j].GetComponent<CustomerScript>().tempTable == table)
                    {
                        customer = tempCustomerList[j].GetComponent<CustomerScript>().GetCustomerByName(gameData.TableList[i].currentCustomerName);
                        orderedVegetableList = customer.transform.GetChild(0).transform.GetComponent<CustomerBubbleScript>().getCustomerOrderList();

                        UIManager.Instance.startText.SetActive(false);
                    }
                }
                
            }
        }

        if(orderedVegetableList.Count > 1)
        {
            for (int i = 0; i < orderedVegetableList.Count; i++)
            {
                orderedVegetableList[i] = SpawnManager.Instance.Spawn(cuttingBoard.transform, orderedVegetableList[i].name.Substring(0, orderedVegetableList[i].name.Length-2), tempVec);
                tempVec = new Vector3(-0.1f, 0, 0);
            }

            //orderedVegetableList.Clear();
        }

        else 
        {
            orderedVegetableList[0] = SpawnManager.Instance.Spawn(cuttingBoard.transform, orderedVegetableList[0].name.Substring(0, orderedVegetableList[0].name.Length-2));
            //orderedVegetableList.Clear();
        }
    }
    
    public void CutVegetable()
    {
        Vector3 tempVec = new Vector3(0.1f, 0, 0);

        if (orderedVegetableList.Count > 1)
        {
            for (int i = 0; i < orderedVegetableList.Count; i++)
            {
                slicedVegetableList.Add(SpawnManager.Instance.Spawn(cuttingBoard.transform, orderedVegetableList[i].name.Substring(0, orderedVegetableList[i].name.Length - 2) + "Slices", tempVec));
                tempVec = new Vector3(-0.1f, 0, 0);
                orderedVegetableList[i].GetComponent<PoolObject>().GoToPool();
            }

            //orderedVegetableList.Clear();
        }

        else
        {
            slicedVegetableList.Add(SpawnManager.Instance.Spawn(cuttingBoard.transform, orderedVegetableList[0].name.Substring(0, orderedVegetableList[0].name.Length - 2) + "Slices"));
            orderedVegetableList[0].GetComponent<PoolObject>().GoToPool();
            //orderedVegetableList.Clear();
        }
    }

    public void CookVegetable(GameObject playerChar)
    {
        if (slicedVegetableList.Count > 1)
        {
            for (int i = 0; i < slicedVegetableList.Count; i++)
            {
                slicedVegetableList[i].GetComponent<PoolObject>().GoToPool();
            }
        }

        else
        {
            slicedVegetableList[0].GetComponent<PoolObject>().GoToPool();
        }

        playerChar.transform.GetChild(4).gameObject.SetActive(true);
    }

    public void DeliverOrder(GameObject playerChar, GameObject table)
    {
        playerChar.transform.GetChild(4).gameObject.SetActive(false);

        for (int i = 0; i < tempCustomerList.Count; i++)
        {
            if(tempCustomerList[i].GetComponent<CustomerScript>().tempTable == table)
            {
                tempCustomerList[i].GetComponent<CustomerScript>().ExitCafe();
            }
        }
        //customer ve baloncuk pool'a don
        //para spawnla
        //parayi ekle
    }

    #endregion
}
