using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicMoveMent_Paladin : LEUnitBasicMoveMent {

    NavMeshAgent navMeshAngent; 

    protected override void Start()
    {
        base.Start();
        navMeshAngent = navMeshAngent.GetComponent<NavMeshAgent>();
    }

    public override void UpdateBasicMoveMent()
    {
        
    }

}
