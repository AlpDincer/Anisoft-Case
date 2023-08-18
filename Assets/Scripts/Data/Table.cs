using System;
using UnityEngine;

namespace Scripts.Data
{
    [Serializable]
    public class Table
    {
        public int tableNumber;
        public string currentCustomerName;
        public bool isUnlocked;
        public bool isFree;

        public GameObject tableModel;

        public Table(int tableNumber)
        {
            this.tableNumber = tableNumber;
        }
    }
}