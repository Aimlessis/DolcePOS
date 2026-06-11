using System;
public class Ventas
{
    public int id {get; set;}
    public int cliente_id {get; set;}
    public int metodo_pago_id {get; set;}
    public DateTime fecha {get; set;}
    public float total {get; set;}
    public float impuesto {get; set;}
    public float descuento {get; set;}

}