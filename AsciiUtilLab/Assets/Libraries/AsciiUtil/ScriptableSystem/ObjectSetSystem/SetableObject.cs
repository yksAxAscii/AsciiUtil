using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetableObject : MonoBehaviour
{
   [SerializeField]
   private string objectName;
   public string ObjectName => objectName;
}
