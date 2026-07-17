using System;

public class Ingredientes
{
    public int id {get; set;}
    public string nombre {get; set;} = string.Empty;
    public double cantidad {get; set;}

    public double costo {get; set;}
    public DateTime? fecha_vencimiento {get; set;}

}