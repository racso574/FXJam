using UnityEngine;

public class ImageToMatrix : MonoBehaviour
{
    public Texture2D inputImage;

    void Start()
    {
        if (inputImage.width != 50 || inputImage.height != 50)
        {
            Debug.LogError("The input image must be 50x50 pixels.");
            return;
        }

        int[,] matrix = ConvertImageToMatrix(inputImage);
        PrintMatrix(matrix);
    }

    int[,] ConvertImageToMatrix(Texture2D image)
    {
        int[,] matrix = new int[50, 50];

        for (int y = 0; y < 50; y++)
        {
            for (int x = 0; x < 50; x++)
            {
                Color pixelColor = image.GetPixel(x, y);
                if (pixelColor == Color.white)
                {
                    matrix[y, x] = 1;
                }
                else if (pixelColor == Color.black)
                {
                    matrix[y, x] = 0;
                }
                else
                {
                    Debug.LogWarning("The image contains colors other than black and white.");
                    matrix[y, x] = 0;
                }
            }
        }

        return matrix;
    }

    void PrintMatrix(int[,] matrix)
    {
        string matrixString = "{\n";
        for (int y = 0; y < 50; y++)
        {
            matrixString += "    { ";
            for (int x = 0; x < 50; x++)
            {
                matrixString += matrix[y, x] + (x < 49 ? ", " : "");
            }
            matrixString += " }" + (y < 49 ? ",\n" : "\n");
        }
        matrixString += "};";
        Debug.Log(matrixString);
    }
}
