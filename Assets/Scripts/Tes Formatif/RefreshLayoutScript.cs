using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshLayoutScript : MonoBehaviour
{
  // Update is called once per frame
  void Update()
  {
    LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
  }
}
