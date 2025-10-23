USE ParqueaderoBack;
use GestionUniversidad;

SELECT * FROM Cupo; 
SELECT * FROM Vehiculo;
SELECT * FROM Usuario;
SELECT * FROM Reserva

SELECT * FROM Reserva 
JOIN Vehiculo ON Reserva.VehiculoId = Vehiculo.Id
JOIN Usuario ON Usuario.Id = Vehiculo.UsuarioId
WHERE Reserva.Estado = 1;

 

DELETE FROM Usuario WHERE Id = 4;
SELECT * FROM Rol;

DROP DATABASE ParqueaderoBack;

