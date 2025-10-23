namespace Parqueadero_Back.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }

        public required string Placa { get; set; }

        public int UsuarioId { get; set; }

        public int Cilindraje { get; set; }

        public Usuario? Usuario { get; set; }

    }
}
