using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateNewCubes : MonoBehaviour
{
    public int count;
    public int number_column;
    public float dif;
    public float y;
    public float first_time;
    private void Start()
    {
        number_column = int.Parse(this.gameObject.name.Split(' ')[1]);
        first_time = 1;
        Down();
    }

    public void Down()
    {
        GameObject obj;
        for (int i = 0; i < count-1 ; i++)
        {
            if (!this.transform.Find("Cube " + i))
            {
                int j = i;
                while (!this.transform.Find("Cube " + ++j)) if (j == count) break;
                if (j == count) break;
                obj =  this.transform.Find("Cube " + j).gameObject;
                obj.name = "Cube " + i;
                obj.GetComponent<Down>().enabled = true;
                obj.GetComponent<Down>().Start_(i);
            }
        }
        int save_cubes = 0;
        for (int i = 0; i < count; i++)
        {
            if (!this.transform.Find("Cube " + i)) break;
            save_cubes++;
        }
        if (save_cubes != count) 
        Create(save_cubes);
    }
    int color;
    void Create(int save_cubes)
    {
        GameObject obj_parent  =  this.gameObject;
        float dif =1.3f;
        var cube = Resources.Load("Cube") ;
        GameObject obj;
        for ( int i = save_cubes; i < count; i++)
        {
            obj = Instantiate((GameObject)cube, new Vector3(this.dif*number_column,(count*1.3f + dif) - y,0), Quaternion.identity,obj_parent.transform);
            dif += 1.3f;
            switch(  color = Random.Range(1, 5))
            {
                case 1:
                    obj.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                case 2:
                    obj.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case 3:
                    obj.GetComponent<SpriteRenderer>().color = Color.green;
                    break;
                case 4:
                    obj.GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;

            }
            obj.name = "Cube " + i;
            obj.GetComponent<Down>().enabled = true;
            obj.GetComponent<Down>().color = this.color;
            obj.GetComponent<Down>().Start_(i);
            
        }
        StartCoroutine(Down_());

    }
    IEnumerator Down_()
    {
        yield return new WaitForSeconds(first_time);
        first_time = 0.5f;
        this.GetComponentInParent<Destroy_Cubes>().Matrix();
    }
}
