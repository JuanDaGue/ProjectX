using UnityEngine;
using System;

public class Challenge2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5.2f;
    public int amplitud = 3;

    public Transform puntoA;
    public Transform puntoB;


    public Vector3 destino = new Vector3(5f, 0f, 0f);
    // Duración total del movimiento en segundos.
    public float duracion = 3f;
    
    // Variable para almacenar la posición inicial del objeto.
    private Vector3 posicionInicial;
    // Variable para acumular el tiempo transcurrido.
    private float tiempoTranscurrido = 0f;



    void Start()
    {
        posicionInicial = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // float posicionX = Mathf.PingPong(Time.time * speed, amplitud * 2) - amplitud;
        // transform.position = new Vector3(posicionX, transform.position.y, transform.position.z);
        // Debug.Log("Posición X: " + Mathf.Abs(posicionX));
        // Debug.Log("Posición 2: " + Mathf.Abs(Mathf.PingPong(Time.time * speed, amplitud * 2)));


        ////////////////////////////////////###Ejemplo de Mathf. #3
        /// ////////////////////////////////////////
        /// 


        // Vector2 posA = new Vector2(puntoA.position.x, puntoA.position.y);
        // Vector2 posB = new Vector2(puntoB.position.x, puntoB.position.y);

        // Vector2 direccion = posB - posA; //  Vector de dirección (ejemplo)
        // float angulo = MathF.Atan2(direccion.y, direccion.x)* (180f / MathF.PI); // Angulo en radianes
        

        // Debug.Log("El ángulo entre A y B es: " + angulo + " grados");


        
        ////////////////////////////////////###Ejemplo de Mathf. #4
        /// ////////////////////////////////////////
        /// 
        /// 
        // if (tiempoTranscurrido < duracion)
        // {
        //     // Incrementa el tiempo transcurrido.
        //     tiempoTranscurrido += Time.deltaTime;
            
        //     // Calcula el factor de interpolación (de 0 a 1).
        //     float t = tiempoTranscurrido / duracion;
            
        //     // Calcula la posición en el eje X entre posInicial y posFinal.
        //     float nuevaPosX = Mathf.Lerp(posInicial, posFinal, t);
            
        //     // Actualiza la posición del objeto.
        //     transform.position = new Vector3(nuevaPosX, transform.position.y, transform.position.z);
        // }

               
        ////////////////////////////////////###Ejemplo de Mathf. #5
        /// ////////////////////////////////////////
        /// 
        /// 
        /// 
        speed = MathF.Round(Mathf.Clamp(speed, 0f, 5f));

        Debug.Log("Velocidad limitada: " + speed);



    }

        
    

}
