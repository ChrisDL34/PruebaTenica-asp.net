este proyecto implementa el clásico juego de Piedra, Papel o Tijera en ASP.NET MVC, siguiendo una arquitectura simple de Modelo-Vista-Controlador (MVC). Las vistas se crean en Razor, y los datos se almacenan en SQL Server.

Funcionalidades: 
Permite la Creacion de jugadores, Luego Proporciona los nombres de los jugadores y Permite la realizacion de movimientos por cada jugador.
Seleccion entre "Rock", "Paper" o "Scissors".
el juego sigue la siguiente logica:
● Papel le gana a la Piedra
● Piedra le gana a Tijera
● Tijera le gana a Papel
Luego que el segundo jugador realiza su movimiento muestra los resultados, permitiendo ver, la sumatoria de puntos, quien ganó la ronda y que movimiento hizo cada jugador.

El sistema determina el ganador y actualiza los puntajes.
Si algún jugador alcanza una puntuación de 3 o más victorias, el juego termina y se declara un ganador final y el juego permite volver a iniciar desde el principio registrando nuevos jugadores.

------------------------------------------
------------------------------------------
Estructura del Proyecto
PlayersController: Controlador principal que gestiona la lógica del juego y las interacciones con la base de datos.
ApplicationDbContext: Contexto de la base de datos para la persistencia de jugadores, movimientos y puntuaciones.
Modelos: Player, Move, ScoreHistory representan entidades en la base de datos.
Arquitectura MVC Simple
Modelo (Model): Representa las entidades de datos y la lógica de negocio.
Vista (View): Utiliza Razor para definir la presentación y la interfaz de usuario.
Controlador (Controller): Gestiona las interacciones del usuario, procesa la lógica del juego y actualiza el modelo.







