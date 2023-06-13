using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenBird : Bird
{
    public override void showSkill()
    {
        base.showSkill();
        Vector3 speed = rg.velocity;
        speed.x *= -1;
        rg.velocity = speed;
        // transform.RotateAround(Vector3.zero, Vector3.forward, 30f);

    }
}
