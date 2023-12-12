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

        for(int i=0; i<slotsFilled.Length - height;i++)
        {
            for(int j=0; j<slotsFilled.GetLength(i) - width;j++)
            {
                bool sectionIsClear = true;

                for(int y=0; y<height; y++)
                {
                    for(int x=0;x<width;x++)
                    {
                        if (slotsFilled[y, x] == true) sectionIsClear = false;
                    }
                }

                if (sectionIsClear) return true;
                sectionIsClear = true;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (slotsFilled[x, y] == true) sectionIsClear = false;
                    }
                }

                if (sectionIsClear) return true;
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
