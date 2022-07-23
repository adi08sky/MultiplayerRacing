using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarView : MonoBehaviour
{
    //Vector3 basePosition = new Vector3(-0.15f, 2.16f, -6.57f);
    Vector3 basePosition;
    bool isFirst = true;
    float zRot = 0, yRot = 0;
    Vector3 startPos;
    Vector3 panel = new Vector3 (5f, 5f, 5f);
    Transform camTrans;
    private void OnEnable()
    {
        //camTrans.localPosition = basePosition;
        camTrans = transform.GetChild(0);
        basePosition = camTrans.localPosition;
        isFirst = true;
    }
    void Update()
    {

        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl))
        {
            if (isFirst)
            {
                isFirst = false;
                startPos = Input.mousePosition;
                return;
            }
            else
            {
                zRot += (Input.mousePosition.y - startPos.y) * 0.1f;
                zRot = Mathf.Clamp(zRot, -25, 45);
                yRot += (Input.mousePosition.x - startPos.x) * 0.3f;
                //yRot = Mathf.Clamp(yRot, -55, 55);
                startPos = Input.mousePosition;
            }
            ScaleUp();
        }
        else
        {
            isFirst = true;
            zRot = Mathf.Lerp(zRot, 0, 0.2f);
            yRot = Mathf.Lerp(yRot, 0, 0.2f);
            ScaleDown();
        }
        transform.eulerAngles = new Vector3(5 + zRot, -65 + yRot, -3);

    }
    void ScaleUp()
    {
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, new Vector3(-0.35f, 2.16f, -5.77f), 0.075f);
    }
    void ScaleDown()
    {
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, basePosition, 0.075f);
    }
}
