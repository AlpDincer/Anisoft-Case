using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerScript : MonoBehaviour
{
    private GameObject moveTarget;
    private GameObject coin;
    public GameObject tempTable;

    public GameObject customerHiddenMarker;

    bool isWalking = false;
    public float speed = 1.0f;
    void Start()
    {
             
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking == true)
        {
            var step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, moveTarget.transform.position, step);
            //GameManager.Instance.TableTaken(moveTarget, this.gameObject.name);

            if (this.transform.position == moveTarget.transform.position && moveTarget != GameManager.Instance.customerSpawnPoint)
            {
                isWalking = false;

                Vector3 lookAtPosition = moveTarget.transform.GetChild(0).position;
                lookAtPosition.y = transform.position.y;

                this.transform.LookAt(lookAtPosition);/*moveTarget.transform.GetChild(0).transform*/
                this.transform.GetChild(0).gameObject.SetActive(true);
                if (moveTarget.name == "Table 0")
                {
                    this.transform.GetChild(0).localPosition += new Vector3(0, 0, 1.5f);
                }

                if(moveTarget.name == "Table 3" || moveTarget.name == "Table 4")
                {
                    this.transform.GetChild(0).localPosition += new Vector3(-1f, 0, 1.5f);
                }

                this.transform.GetChild(0).transform.LookAt(Camera.main.transform);
            }

            else if(this.transform.position == moveTarget.transform.position && moveTarget == GameManager.Instance.customerSpawnPoint) //customer ve baloncuk pool'a don
            {
                isWalking = false;

                Vector3 lookAtPosition = moveTarget.transform.position;
                lookAtPosition.y = transform.position.y;

                this.transform.LookAt(lookAtPosition);

                GameManager.Instance.FreeTable(tempTable);

                this.transform.GetChild(0).GetComponent<CustomerBubbleScript>().deleteCustomerOrderList(); //get customer order models back into pool
                this.GetComponent<PoolObject>().GoToPool();
                GameManager.Instance.tempCustomerList.Remove(this.gameObject);
            }

            else { }
        }
    }

    public GameObject GetCustomerByName(string name)
    {
        if(this.gameObject.name == name)
        {
            return this.gameObject;
        }

        else
        {
            return null;
        }
    }

    public GameObject GetCustomerByTable(GameObject table)
    {
        if (this.tempTable == table)
        {
            return this.gameObject;
        }

        else
        {
            return null;
        }
    }

    public void GoToTable()
    {
        if (tempTable != null)
        {
            moveTarget = tempTable;
            this.transform.LookAt(moveTarget.transform);
            isWalking = true;
        }
    }
    public void ExitCafe()
    {       
        this.transform.GetChild(0).gameObject.SetActive(false);

        Vector3 tempVec = new Vector3(0, 0.01f, 0);
        coin = SpawnManager.Instance.Spawn(tempTable.transform.GetChild(0).position, "Coin", tempVec); //para spawnla
        GameManager.Instance.AddCoin(10); //parayi ekle
        coin.GetComponent<PoolObject>().GoToPool(); //pool'a geri at
        //StartCoroutine(CollectCoin());
        moveTarget = GameManager.Instance.customerSpawnPoint;
        isWalking = true; //kafeden cikisa git             
        
    }

    IEnumerator CollectCoin()
    {
        new WaitForSecondsRealtime(5f);
        coin.GetComponent<PoolObject>().GoToPool();
        GameManager.Instance.AddCoin(10);

        yield return null;
    }
}
