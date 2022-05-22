using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [Header("BrickWall")]
    public TileManager leftB;
    public TileManager leftTopB;
    public TileManager topB;
    public TileManager rightTopB;
    public TileManager rightB;

    [Header("SteelWall")]
    public TileManager leftS;
    public TileManager leftTopS;
    public TileManager topS;
    public TileManager rightTopS;
    public TileManager rightS;

    [Header("Position")]
    public TileManager left;
    public TileManager leftTop;
    public TileManager top;
    public TileManager rightTop;
    public TileManager right;

    private WaitForSeconds delay1;
    private WaitForSeconds delay2;
    private Coroutine timerCoroutine;

    private void Awake() {
        delay1 = new WaitForSeconds(10.0f);
        delay2 = new WaitForSeconds(5.0f);
    }
    private void Start()
    {
        if (left.Blank() || leftTop.Blank() || top.Blank() || rightTop.Blank() || right.Blank())
        {
            ReplaceBrick();
        }
    }
    public void ReplaceBrick()
    {
        left.Replace(leftB);
        right.Replace(rightB);
        leftTop.Replace(leftTopB);
        rightTop.Replace(rightTopB);
        top.Replace(topB);
    }

    public void Renforce()
    {
        ReplaceSteel();
        timerCoroutine = StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return delay1;
        left.BroadcastMessage("Flash",SendMessageOptions.DontRequireReceiver);
        leftTop.BroadcastMessage("Flash",SendMessageOptions.DontRequireReceiver);
        top.BroadcastMessage("Flash",SendMessageOptions.DontRequireReceiver);
        rightTop.BroadcastMessage("Flash",SendMessageOptions.DontRequireReceiver);
        right.BroadcastMessage("Flash",SendMessageOptions.DontRequireReceiver);
        yield return delay2;
        ReplaceBrick();
    }


    public void ReplaceSteel()
    {
        left.Replace(leftS);
        right.Replace(rightS);
        leftTop.Replace(leftTopS);
        rightTop.Replace(rightTopS);
        top.Replace(topS);
    }
}
