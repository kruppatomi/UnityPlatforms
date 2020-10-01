using UnityEngine;

public class Map : MonoBehaviour
{
    private static readonly System.Random random = new System.Random(); 
    private static readonly object syncLock = new object(); 
     public GameObject top;
    // Start is called before the first frame update
    void Start()
    {
        // generateMap();
        createRandomMap();
    }

    void generateMap(){
        for (int i = -10; i < 10; i++){
            for (int j = 10 ; j > -10; j--){
            GameObject GroundClone = Instantiate(top, new Vector3(i*2f+(float)0.1, -5, j*2f+(float)0.1), top.transform.rotation);
            } 
        }
    }
    
    public static int RandomNumber(int min, int max)
    {
        lock(syncLock) { // synchronize
            return random.Next(min, max);
        }
    }

    void createRandomMap(){
        GameObject GroundClone = Instantiate(top, new Vector3(0,-5,0), top.transform.rotation);
        float lastX = 0;
        float lastY = -5;
        float lastZ = 0;
        for (int i = 0; i < 100; i++ ){
            switch(RandomNumber(0,4)){
                case 0:
                GameObject GroundClone1 = Instantiate(top, new Vector3(lastX -(2f), lastY, lastZ), top.transform.rotation);
                lastX -= (float)2;
                break;
                case 1:
                GameObject GroundClone2 = Instantiate(top, new Vector3(lastX, lastY, lastZ-(2f)), top.transform.rotation);
                lastZ -= (float)2;
                break;
                case 2:
                GameObject GroundClone3 = Instantiate(top, new Vector3(lastX -(2f), lastY-2, lastZ), top.transform.rotation);
                lastX -= (float)2;
                lastY -= 2;
                break;
                case 3:
                GameObject GroundClone4 = Instantiate(top, new Vector3(lastX, lastY-2, lastZ-(2f)), top.transform.rotation);
                lastZ -= (float)2;
                lastY -= 2;
                break;
            }
        }
    }
}
