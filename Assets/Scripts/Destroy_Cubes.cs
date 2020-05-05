using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy_Cubes : MonoBehaviour
{

    public  int[,] matrix;
    private void Start()
    {
        int i = this.transform.childCount;
        int max = -1;
        for (int j = 0; j < i; j++)
            if (this.transform.GetChild(j).transform.childCount >= max)
                max = this.transform.GetChild(j).transform.childCount;
        matrix = new int[max, i];
    }
    public void Matrix()
    {
        
        for (int k = 0; k < matrix.GetLength(0); k++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                try
                {
                    if (this.transform.GetChild(j).transform.GetChild(k) != null) 
                    matrix[k, j] = this.transform.GetChild(j).transform.GetChild(k).GetComponent<Down>().color;
                }
                catch
                {
                    matrix[k, j] = 0;
                }
            }
        }
        Destroy_();
    }

    public int[,] penetration;
    private void Destroy_()
    {
        penetration = new int[matrix.GetLength(0),matrix.GetLength(1)];
        for (int k = 0; k < matrix.GetLength(0); k++)
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                /*up and down*/
                for (int p = 2;p < matrix.GetLength(0); p++) 
                    if ((matrix[p,j] == matrix[p-1,j]) && (matrix[p, j] == matrix[p - 2, j]) && (matrix[p,j]!=0))
                    {
                        penetration[p, j] = 1;
                        penetration[p - 1, j] = 1;
                        penetration[p - 2, j] = 1;
                    }
                    else
                    {
                        if (penetration[p, j] != 1) penetration[p, j] = 0;
                        if (penetration[p - 1, j] != 1) penetration[p - 1, j] = 0;
                        if (penetration[p - 2, j] != 1) penetration[p - 2, j] = 0;
                    }

                /*left and right */
                for (int p = 2; p < matrix.GetLength(1); p++)
                    if ((matrix[k, p] == matrix[k, p-1]) && (matrix[k, p] == matrix[k, p-2]) && (matrix[k, p] != 0))
                    {
                        penetration[k, p] = 1;
                        penetration[k, p-1] = 1;
                        penetration[k, p-2] = 1;
                    }
                    else
                    {
                        if (penetration[k, p] != 1) penetration[k, p] = 0;
                        if (penetration[k, p-1] != 1) penetration[k, p - 1] = 0;
                        if (penetration[k, p-2] != 1) penetration[k, p - 2] = 0;
                    }
            }

        Str();
        StartCoroutine(Destroy_Now(penetration));


    }
    IEnumerator Destroy_Now(int[,]i)
    {
        yield return new WaitForSeconds(1f);
        bool again = false;
        for (int k = 0; k < matrix.GetLength(0); k++)
            for (int j = 0; j < matrix.GetLength(1); j++)
                if (i[k, j] == 1)
                {
                    Destroy(this.transform.GetChild(j).GetChild(k).gameObject);
                    again = true;
                }
        if (again)
        {
            for (int k = 0; k < this.transform.childCount; k++)
                StartCoroutine(Start_(k));
        }
    }

    IEnumerator Start_(int i)
    {
        yield return new WaitForSeconds(0);
        this.transform.GetChild(i).GetComponent<CreateNewCubes>().Down();
    }
    void Str()
    {
        string txt = "";
        for (int j = 0; j < matrix.GetLength(0); j++)
        {
            txt += j + ": ";
            for (int k = 0; k < matrix.GetLength(1); k++)
            {
                txt += penetration[j, k] + "\t";
            }
            txt += "\n";
        }
        GameObject.Find("Text").GetComponent<Text>().text = txt;
    }
}
