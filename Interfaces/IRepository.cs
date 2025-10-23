namespace Parqueadero_Back.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task Insertar(T entidad);

        public Task Eliminar(T entidad);

        public Task Actualizar(T entidad);

        public Task<T?> Buscar(int id);

        public Task<IEnumerable<T>> ObtenerTodos();
    }
}
