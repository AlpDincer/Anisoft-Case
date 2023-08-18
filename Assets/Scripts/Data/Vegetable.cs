using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Scripts.Data
{
    [Serializable]
    public class Vegetable
    {
        public string vegetableName;
        public GameObject rawModel;
        public bool isUnlocked;


        public Vegetable(string vegetableName/*, GameObject rawModel,GameObject slicedModel*/)
        {
            this.vegetableName = vegetableName;
            //this.rawModel = rawModel;
            //this.slicedModel = slicedModel;
        }

    }
}