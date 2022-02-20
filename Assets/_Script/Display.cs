using UnityEngine;

using System.Collections;
using System.IO;


public class Display : MonoBehaviour
{//https://docs.unity3d.com/ScriptReference/Material.SetTexture.html
    public string imagePrefix = "Kitchen_";
    public string file = "C:/Work/AugmentedPerceptionLab/frames";
    public string fileExtention = ".png";

    [HideInInspector]
    public int imageAmount;

    Renderer renderer;

    //Pads numbers to fit the image numbering standard
    string padNumbers(int i, int length)
    {
        string number = i.ToString();
        int numberToPad = length - number.Length;
        string result = "";
        for (int x = 0; x < numberToPad; x++)
        {
            result += "0";
        }
        result += number;
        print(result);
        return result;
    }

    //reads slide from frame
    void setSlide(int i)
    {
        //Get texture
        byte[] bytes = File.ReadAllBytes(file + "/" + imagePrefix + padNumbers(i, 4) + fileExtention);

        //Fetch the Renderer from the GameObject
        renderer = GetComponent<Renderer>();
        if (i >= imageAmount || i < 0)
        {
            print("slider attempted to access an element out of bounds");
            return;
        }

        Texture2D frame = new Texture2D(2, 2);
        frame.LoadImage(bytes);

        //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
        renderer.material.SetTexture("_MainTex", frame);
    }


    // Start is called before the first frame update
    void Start()
    {
        //figure out how many images in directory.
        imageAmount = Directory.GetFiles(file, "*", SearchOption.TopDirectoryOnly).Length;
        //just show a texture

        setSlide(1);
    }


}