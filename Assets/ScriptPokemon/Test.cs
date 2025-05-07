using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Pokemon pokemon;
    
    void Start()
    {
        Pokemon Bulbasaur = new Pokemon();
        Bulbasaur.Name = "Bulbasaur";

        PuntosPokemon puntosPokemon = new PuntosPokemon();
        puntosPokemon.PS= 45;
        puntosPokemon.PuntosAtaque = 49;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
