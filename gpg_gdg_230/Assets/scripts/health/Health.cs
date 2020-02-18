using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //health universal health script so everything can die
    public int health;

	public bool defending = false;

    //calling will be
    //entity.Getcomponent<Health>().health=value
}
