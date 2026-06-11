using Avalonia.Remote.Protocol.Viewport;

public class Producto
{
    public int id {get; set;}
    public string nombre {get; set;} = string.Empty;
    public float costo {get; set;}
    public float precio {get; set;}
    public int cantidad {get; set;}
    public float descuento_max {get; set;}
    public float beneficio {get; set;}
    public string descripcion {get; set;} = string.Empty;

}