using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    private ContainerScriptable _type;
    private bool[,] slotsFilled;

    private void Start()
    {
        slotsFilled = new bool[_type._height, _type._width]; //NOTICE ROW FIRST NOTATION
    }

    //Check to see if there is enough space in the backpack
    public bool spaceExists(int width, int height)
    {
        if (width < 1 || height < 1) return false;

        int[] histogram = new int[slotsFilled.Length];

        int j = 0;
        while(j < slotsFilled.Length)
        {
            for (int i = 0; i < slotsFilled.GetLength(j); i++)
            {
                histogram[i] = slotsFilled[j, i] == true ? histogram[i] + 1 : 0;
            }

            for (int i = 0; i < slotsFilled.GetLength(j); i++)
            {
                if(histogram[i] >= height)
                {
                    for(int k = i; k < width; k++)
                    {
                        if (histogram[k] < height || k >= slotsFilled.GetLength(0))
                        {
                            i = k;
                        }

                        else return true;
                    }
                }

                if (histogram[i] >= width)
                {
                    for (int k = i; k < height; k++)
                    {
                        if (histogram[k] < width || k >= slotsFilled.GetLength(0))
                        {
                            i = k;
                        }

                        else return true;
                    }
                }
            }
        }
        

        return false;
    }

    //TODO: Implement the free space vector based on the position in the container that can hold the object. refine the search logic
    public Vector3 freeSpace(int width, int height)
    {
        return Vector3.zero;
    }
}
