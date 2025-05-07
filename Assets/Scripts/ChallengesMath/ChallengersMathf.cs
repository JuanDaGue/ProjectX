using UnityEngine;

public class ChallengersMathf : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float myFloat = 5.2f;
    public int myInt = 5;
    public int myInt2 = 6;
    public int myInt3 = 7;
    void Start()
    {
        
    }


    void Update()
    {
        int numAleatorio = Random.Range(-10, 11);
        //int numAleatorio = myInt;
        if (numAleatorio < -10)
        {
            Debug.Log("El número aleatorio es menor que 10 " + numAleatorio);
            return;
        }
        else if (numAleatorio > 10)
        {
            Debug.Log("El número aleatorio es mayor que 10 " + numAleatorio);
            return;
        }

        int valorAbsoluto = Mathf.Abs(numAleatorio);
        Debug.Log("El valor absoluto de " + numAleatorio + " es: " + valorAbsoluto);
    }

        void mymatF()
    {
        Debug.Log("Some examples");
        Mathf.Abs(-5); // 5
        Mathf.Ceil(5.2f); // 6  
        Mathf.CeilToInt(5.2f); // 6
        Mathf.Floor(5.2f); // 5
    }

    public void keyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string entradaUsuario = GUIUtility.systemCopyBuffer; 
            if (int.TryParse(entradaUsuario, out int numero))
            {
                Debug.Log($"Número ingresado: {numero}");
            }
            else
            {
                Debug.Log("Entrada inválida. Ingresa un número.");
            }
        }

     }


}
