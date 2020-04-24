using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPurchase : MonoBehaviour
{
    public int purchaseNumber;
    public FakeMicrotransactions FMTS;

    public void SelectPurchaseNumber()
    {
        FakeMicrotransactions.whichPurchaseOne = purchaseNumber;
    }
}
