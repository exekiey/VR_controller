# Collectable

Este script maneja el comportamiento de un objeto coleccionable en la escena.

## Funcionalidades
- Hace que el coleccionable se encoja al interactuar con él.
- Notifica al singleton `Collectables` cuando se recoge.
- Desactiva el objeto después de ser recogido para evitar recogidas repetidas.

## Uso
1. Adjuntar este script a un GameObject coleccionable con un Collider.
2. Asegurarse de que existe un singleton `Collectables` en la escena.
3. El objeto se encogerá durante `timeToShrink` segundos al llamar a `OnPointerEnter()` y luego se desactivará.

## Campos del Inspector
- `timeToShrink` (float): Duración de la animación de encogimiento. Por defecto 0.1 segundos.

## Métodos
- `OnPointerEnter()`: Dispara el encogimiento y notifica al singleton.
- `OnPointerExit()`: Actualmente vacío, puede usarse para lógica al salir del hover.
- `Update()`: Maneja la animación de encogimiento y la desactivación.

---

# Collectables

Este script gestiona todos los objetos coleccionables en la escena como un singleton.

## Funcionalidades
- Acceso singleton mediante `Collectables.instance`.
- Mantiene una lista de objetos recogidos.
- Proporciona un evento `OnCollectableGrabbed` para notificaciones cuando se recoge un coleccionable.

## Uso
1. Adjuntar a un GameObject vacío en la escena.
2. Otros scripts llaman a `Collectables.instance.NotifyCollectableGrabbed(gameObject)` para notificar la recogida.
3. El script añade automáticamente el coleccionable a la lista interna y registra la cantidad actual.

## Propiedades
- `CollectablesList` (LinkedList<GameObject>): Acceso de solo lectura a la lista de objetos recogidos.

## Eventos
- `OnCollectableGrabbed`: Se dispara al recoger un coleccionable; suscrito internamente al método `Collect`.

## Métodos
- `NotifyCollectableGrabbed(GameObject collectable)`: Dispara el evento de recogida.
- `Collect(GameObject collectable)`: Añade el coleccionable a la lista interna y registra la cantidad.

---

# CollectablesPickup

Este script maneja la recogida de todos los objetos coleccionables y los lanza hacia el jugador.

## Funcionalidades
- Activa y mueve todos los objetos recogidos hacia el jugador.
- Aplica fuerza de impulso a los Rigidbodies para movimiento dinámico.
- Limpia la lista de objetos recogidos tras la recogida.

## Uso
1. Adjuntar a un GameObject que actúe como trigger de recogida.
2. Asignar el GameObject `player` en el inspector.
3. Ajustar `speed` para controlar la fuerza aplicada a los objetos.
4. Llamar a `OnPointerEnter()` para lanzar todos los objetos hacia el jugador.

## Campos del Inspector
- `player` (GameObject): Objeto objetivo hacia el que se lanzan los coleccionables.
- `speed` (float): Magnitud de la fuerza de impulso aplicada.

## Métodos
- `OnPointerEnter()`: Activa, mueve y lanza los objetos hacia el jugador, luego limpia la lista.
- `OnPointerExit()`: Actualmente vacío; puede usarse para lógica al salir del hover.

---

# DashedLine

Este script crea un efecto de línea discontinua animada usando LineRenderer.

## Funcionalidades
- Anima el desplazamiento de la textura para simular líneas discontinuas en movimiento.
- Permite personalizar velocidad y color del trazo.

## Uso
1. Adjuntar a un GameObject con un LineRenderer.
2. Configurar `dashSpeed` para controlar la velocidad de movimiento de los guiones.
3. Configurar `dashColor` para cambiar el color de la línea.

## Campos del Inspector
- `dashSpeed` (float): Velocidad a la que se mueven los guiones.
- `dashColor` (Color): Color de la línea discontinua.

## Métodos
- `Start()`: Crea una instancia única del material para el LineRenderer.
- `Update()`: Anima los guiones ajustando el offset de la textura cada frame.

---

# DebugPlayerController

Este script permite rotar manualmente un GameObject usando las teclas de flecha para depuración.

## Funcionalidades
- Rota el objeto alrededor de los ejes X e Y usando el teclado.

## Uso
1. Adjuntar a un GameObject de jugador o cámara.
2. Usar las flechas para rotar:
   - Derecha/Izquierda: rotación en el eje Y.
   - Arriba/Abajo: rotación en el eje X.

## Métodos
- `Update()`: Detecta la entrada del teclado y aplica la rotación cada frame.

---

# FadeScript

Este script maneja un efecto de desvanecimiento a negro y de vuelta a transparente en un RawImage.

## Funcionalidades
- Desvanece la pantalla a negro, dispara un evento, y luego desvanecimiento de regreso.
- Configurable velocidad de desvanecimiento y espera entre pasos.

## Uso
1. Adjuntar a un GameObject con un RawImage (normalmente pantalla completa).
2. Ajustar `increaseFactor` para controlar el tamaño de paso del alfa.
3. Ajustar `secondsWaited` para definir el tiempo entre pasos de desvanecimiento.
4. Llamar a `StartFade()` para iniciar el efecto.
5. Suscribirse a `FadeFinished` para reaccionar cuando la pantalla está completamente negra.

## Campos del Inspector
- `increaseFactor` (float): Incremento de alfa por paso.
- `secondsWaited` (float): Tiempo de espera entre pasos.

## Métodos
- `StartFade()`: Inicia la coroutine de desvanecimiento.
- `TeleportCoroutine()`: Realiza el fade-in, dispara `FadeFinished` y luego fade-out.

---

# LoadingCircle

Este script dibuja un indicador circular de progreso usando LineRenderer.

## Funcionalidades
- Precalcula puntos para un círculo según radio y cantidad de puntos.
- Actualiza dinámicamente el círculo según progreso.
- Permite reiniciar el círculo para un nuevo ciclo de progreso.

## Uso
1. Adjuntar a un GameObject con LineRenderer.
2. Configurar `radius` y `numberOfPoints`.
3. Llamar a `UpdateCircle(currentCount, counterLimit)` para mostrar progreso.
4. Llamar a `ResetCircle()` para limpiar el círculo.

## Campos del Inspector
- `radius` (float): Radio del círculo.
- `numberOfPoints` (int): Número total de puntos del círculo.
- `lineRenderer` (LineRenderer): LineRenderer que dibuja el círculo.

## Métodos
- `UpdateCircle(float currentCount, int counterLimit)`: Añade puntos según progreso.
- `ResetCircle()`: Limpia el LineRenderer y reinicia progreso.
- `AddNewPoint()`: Añade el siguiente punto al LineRenderer.

---

# MovementScript

Este script maneja teletransportación basada en la mirada con feedback visual en VR o setups en primera persona.

## Funcionalidades
- Detecta la mirada sobre superficies "teleportable".
- Dibuja un puntero y línea cuadrática desde el jugador hasta el objetivo.
- Implementa un temporizador de teletransporte con círculo de carga.
- Dispara efecto de fade antes de teletransportar, preservando la altura del jugador.
- Muestra la cuenta regresiva usando TextMeshProUGUI.

## Uso
1. Adjuntar a un GameObject de jugador o cámara.
2. Asignar referencias en el inspector:
   - `pointer`: GameObject mostrado en el objetivo de la mirada.
   - `pointerLine`: LineRenderer para la línea de teletransporte.
   - `textMeshProUGUI`: Texto UI para la cuenta regresiva.
   - `fadeScript`: Maneja efecto de fade.
   - `loadingCircle`: Muestra progreso de teletransporte.
   - `playerFeet`: Transform de los pies del jugador a mover.
3. Configurar parámetros de teletransporte:
   - `teleportCounterLimit`: Tiempo requerido para activar teletransporte.
   - `numberOfPoints`: Puntos para resolución de línea cuadrática.
   - `landSlope`: Pendiente inicial de la línea cuadrática.
   - `lineOriginOffset`: Offset de inicio de línea desde cámara.

## Métodos
- `Update()`: Rastrea mirada, actualiza puntero, línea, círculo de carga y lógica de teletransporte.
- `GetGazePosition(out Vector3)`: Devuelve primer punto golpeado en superficies "Teleportable".
- `GetQuadraticSpline(...)`: Calcula coeficientes cuadráticos para la línea.
- `DrawPointer(Vector3)`: Posiciona y muestra el puntero.
- `DrawLine(Vector3)`: Dibuja la curva cuadrática desde cámara hasta mirada.
- `OnFadeFinished()`: Mueve al jugador al punto de mirada tras el fade.
- `OnPointerEnter() / OnPointerExit()`: Métodos de evento de puntero, actualmente placeholders.


![Demo](1.gif)<br>
![Demo](2.gif)<br>
