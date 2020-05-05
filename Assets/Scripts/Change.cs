using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    static bool First = true;
    static GameObject obj_1;
    static GameObject obj_2;
    float dif
    {
        get
        {
           return this.GetComponentInParent<CreateNewCubes>().dif;
        }
    }
    static byte cool_down = 0;
    void OnMouseDown()
    {
        if ((cool_down == 0) || (cool_down == 1))
        {
            if (First) First_();
            else Second_();
            cool_down++;

        }
    }
    IEnumerator One_Seconde()
    {
        yield return new WaitForSeconds(1.1f);
        cool_down = 0;
    }

    void First_()
    {
        obj_1 = this.gameObject;
        First = false;
    }

    void Second_()
    {
        obj_2 = this.gameObject;
        if (((Mathf.Abs(obj_1.transform.position.x - obj_2.transform.position.x) == 1.3f) &&
            (int.Parse(obj_1.name.Split(' ')[1]) == int.Parse(obj_2.name.Split(' ')[1]))) || 
            ((obj_1.transform.parent == obj_2.transform.parent) && 
            ( Mathf.Abs(int.Parse(obj_1.name.Split(' ')[1]) - int.Parse(obj_2.name.Split(' ')[1])) == 1)))
        {
            string name_parent;
            if (obj_1.transform.position.x != obj_2.transform.position.x)
            {
                name_parent = new string(obj_1.transform.parent.name.ToCharArray());
                obj_1.transform.SetParent(obj_2.transform.parent);
                obj_2.transform.SetParent(GameObject.Find("God").transform.Find(name_parent).transform);
            }
            else
            {
                name_parent = obj_1.name;
                obj_1.name = obj_2.name;
                obj_2.name = name_parent;

                int i = obj_1.GetComponent<Down>().number;
                obj_1.GetComponent<Down>().number = obj_2.GetComponent<Down>().number;
                obj_2.GetComponent<Down>().number = i;
            }

            obj_1.transform.SetSiblingIndex(int.Parse(obj_1.name.Split(' ')[1]));
            obj_2.transform.SetSiblingIndex(int.Parse(obj_2.name.Split(' ')[1]));

            obj_1.GetComponent<Down>().enabled = true;
            obj_1.GetComponent<Down>().Change_Start(obj_2.transform.position);

            obj_2.GetComponent<Down>().enabled = true;
            obj_2.GetComponent<Down>().Change_Start(obj_1.transform.position);
        }
        First = true;
        StartCoroutine(One_Seconde());
        StartCoroutine(Destroy_All());

    }

    IEnumerator Destroy_All()
    {
        yield return new WaitForSeconds(1.2f);
        GameObject.Find("God").GetComponent<Destroy_Cubes>().Matrix();
    }
}
