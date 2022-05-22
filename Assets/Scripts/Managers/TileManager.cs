using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    private BaseManager baseManager;

    private void Awake() {
        baseManager = GameObject.FindObjectOfType<BaseManager>();
        resetPosion();
    }

    private void Update() {
        resetPosion();
    }

    private void resetPosion()
    {
        switch((transform.position.x,transform.position.y))
        {
            case (< -0.31f and > -0.33f, < -3.4f and > -3.6f):
                baseManager.left = this;// left
                break;
            case (< -0.31f and > -0.33f, < -2.7f and > -2.9f):
                baseManager.leftTop = this;// lefttop
                break;
            case (< 0.33f and > 0.31f, < -2.7f and > -2.9f):
                baseManager.top = this;// top
                break;
            case (< 0.97f and > 0.95f, < -2.7f and > -2.9f):
                baseManager.rightTop = this;// righttop
                break;
            case (< 0.97f and > 0.95f, < -3.4f and > -3.6f):
                baseManager.right = this;// right
                break;
                
        }
    }

    public bool Blank()
    {
        switch (transform.childCount)
        {
            case < 1:
                return true;
            default:
                return false;
        }
    }

    public void Replace(TileManager tileManager)
    {
        Instantiate(tileManager, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
