using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : MonoBehaviour
{

    public float speed;
    public float dif;
    public int number;
    public int color;
    public void Start_(int number)
    {
        this.number = number;
        StartCoroutine(Down_Timer());
    }
    Vector3 xyz;
    IEnumerator Down_Timer()
    {
        xyz = this.transform.position;
        if (xyz.y <= number * dif + 0.001f - this.GetComponentInParent<CreateNewCubes>().y )
        {
            xyz = this.transform.position;
            xyz.x = Mathf.Round(this.transform.position.x  *100)/100;
            xyz.y = Mathf.Round(this.transform.position.y * 100) /100;
            this.transform.position = xyz;
            this.GetComponent<Down>().enabled = false;
            yield break;
        }
        xyz.y -= speed / 1000;
        this.transform.position = xyz;
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(Down_Timer());
    }

     Vector3 dif_vector;
    public float speed_change;
    public void Change_Start(Vector3 xyz_)
    {
        dif_vector = (this.transform.position - xyz_) /50;

        StartCoroutine(Change(xyz_));
    }
    IEnumerator Change(Vector3 xyz_vers)
    {
        if (this.transform.position == xyz_vers)
        {
            if (dif_vector.x != 0)
            {
                this.transform.localPosition = new Vector3(0,this.transform.localPosition.y,this.transform.localPosition.z);
            }
            if (dif_vector.y != 0)
            {

                this.transform.localPosition = new Vector3(this.transform.localPosition.x, number * dif, this.transform.localPosition.z);
            }
            this.GetComponent<Down>().enabled = false;
            yield break;
        }
        this.transform.position -= dif_vector;
        yield return new WaitForSeconds(0.02f);
        StartCoroutine(Change(xyz_vers));
    }
}
