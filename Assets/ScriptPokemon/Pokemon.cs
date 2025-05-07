using UnityEngine;

public class Pokemon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string name;
    [SerializeField] private string type;
    [SerializeField] private int level;
    [SerializeField] private float wweight;
    [SerializeField] private float height;
    [SerializeField] private float category;
    [SerializeField] private string ability;
    [SerializeField] private PuntosPokemon puntosPokemon;
    [SerializeField] private TypesPokemon typesPokemon;
    public PuntosPokemon PuntosPokemon { get => puntosPokemon; set => puntosPokemon = value; }
    public TypesPokemon TypesPokemon { get => typesPokemon; set => typesPokemon = value; }

    public string Name { get => name; set => name = value; }
    public string Type { get => type; set => type = value; }

}
