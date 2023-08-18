using Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBubbleScript : MonoBehaviour
{

    public static List<string> bubbleVegetableList;
    public List<GameObject> orderedVegetableList;

    private int mealChoice;
    private int tempInt;
    private string tempVegetable;
    private Vector3 tempVec;
    void Start()
    {
        
    }

    public void DecideMeal()
    {
        if (bubbleVegetableList.Count > 1)
        {
            mealChoice = Random.Range(1, 3); // daha cesitli yemek icin degistirilebilir

            if (mealChoice == 2)
            {
                doubleMeal();
            }

            else { singleMeal(); }
        }

        else { singleMeal(); }
    }
    public void singleMeal()
    {
        string singleMealChoice = bubbleVegetableList[Random.Range(0, bubbleVegetableList.Count)];
        
        orderedVegetableList.Add(SpawnManager.Instance.Spawn(this.transform, singleMealChoice, 1.5f));
        //GameManager.Instance.SendOrder(singleMealChoice);
        //SpawnManager.Instance.Spawn(cuttingBoard.transform, singleMealChoice);
    }

    public void doubleMeal()
    {
        tempVec = new Vector3(0.5f, 0, 0);
        tempInt = Random.Range(0, bubbleVegetableList.Count);

        
        orderedVegetableList.Add(SpawnManager.Instance.Spawn(this.transform, bubbleVegetableList[tempInt], 1.5f, tempVec));

        tempVegetable = bubbleVegetableList[tempInt];
        bubbleVegetableList.RemoveAt(tempInt); //cektigin sebzeyi cikar

        tempVec = new Vector3(-0.5f, 0, 0);
        tempInt = Random.Range(0, bubbleVegetableList.Count);

        
        orderedVegetableList.Add(SpawnManager.Instance.Spawn(this.transform, bubbleVegetableList[tempInt], 1.5f, tempVec));

        bubbleVegetableList.Add(tempVegetable); //sebzeyi geri ekle
    }

    public List<GameObject> getCustomerOrderList()
    {
        return orderedVegetableList;
    }

    public void deleteCustomerOrderList()
    {
        this.orderedVegetableList.Clear();
        for (int i = this.transform.childCount-1; i >= 0; i--) 
        {           
            this.transform.GetChild(i).localScale /= 1.5f; //first get scales back to default
            this.transform.GetChild(i).GetComponent<PoolObject>().GoToPool(); // then return objects to pool
        }
    }
}
