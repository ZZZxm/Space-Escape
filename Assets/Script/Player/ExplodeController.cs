using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeController : MonoBehaviour
{
    public int attack;

    private void AniFinish()
    {
        Destroy(gameObject);
    }
}
