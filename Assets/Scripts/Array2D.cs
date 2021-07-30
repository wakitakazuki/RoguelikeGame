
public class Array2D
{
    [UnityEngine.SerializeField] public int width;
    [UnityEngine.SerializeField] public int height;
    //private int[,] data;

    [UnityEngine.SerializeField] private int[] data;
    
    public Array2D(int w,int h)
    {
        width = w;
        height = h;
        //data = new int[width, height];

        data = new int[width * height];
       
    }

    public int Get(int x,int z)
    {
        if(x >= 0 && z >= 0 && x < width && z < height)
        {
            //return data[x, z];
            return data[x + z * width];
        }
        return -1;
    }
    public int Set(int x, int z, int v)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            //data[x, z]=v;
            data[x + z * width] = v;
            return v;
        }
        return -1;
    }

    //public override string ToString()
    //{
        
    //}
}