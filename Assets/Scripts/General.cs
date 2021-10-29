using UnityEngine;
using System.Collections;
//using UnityEditor;
using UnityEngine.UI;

/// Controla el comportamiento de la Interfaz gráfica de usuario y la visualización de Circuitos 
/**
	Maneja la interfaz gráfica de usuario y sus contenidos, además activa o desactiva cada Circuito y sus componentes
*/
public class General : MonoBehaviour {

	/// Variable para la imagen del cursor
    public  Texture2D   cursorTexture;

	/// Variable para el texto asociado a las \a orientaciones
    public  Text OrientText;
	/// Variable para el texto asociado a las \a discusiones
    public  Text DiscusText;

	/// Variable para la barra de desplazamiento del las \a orientaciones
    public Scrollbar OriScroll;
	/// Variable para la barra de desplazamiento del las \a discusiones
    public Scrollbar DisScroll;

	/// Objeto de Unity que contiene todos los \a circuitos
    public  Transform   Circuits;
	/// Objeto de Unity que contiene los \a ejes para la gráfica
	public  Transform   axes;
	/// varible que indica cual fue el circuito activo anterior
    int     lastCircuitIndex;

	/// varible para indicar si el panel izquierdo de la UI será visible o no
    public bool leftVisible;
	/// varible para indicar si el panel inferior de la UI será visible o no
	public bool downVisible;
	/// varible para que el movimiento del panel izquierdo de la UI sean continuo cuando aparece o desaparece
    public float   timeCameraX;
	/// varible para que el movimiento del panel inferior de la UI sean continuo cuando aparece o desaparece
	public float   timeCameraY;
	/// varibles para la coordenada a las que se moverá el panel izquierdo de la UI
    float   xTarget;
	/// varibles para la coordenada a las que se moverá el panel inferior de la UI
	float   yTarget;
	// variable para el tamaño del contenido de la Orientacion
	public GameObject _contenOrientacion;
	// variable para el tamaño del contenido de la discucion
	public GameObject _contenDiscucion;

// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles y selecciona el circuito inicial.
*/
    void Start () {

        lastCircuitIndex = 0;
        OrientText.text = TextOrient(0);
        _contenOrientacion.GetComponent<RectTransform>().sizeDelta = new Vector2(0,sizeOrientaciones(0));
        _contenDiscucion.GetComponent<RectTransform>().sizeDelta = new Vector2(0,sizeDiscuciones(0));
    }
// =====================================================================================================
/// Sirve para actualizar el texto \a Orientaciones del panel inferior según el circuito activo.
/**
  \param index indice del circuito activo
  \return texto asociado al circuito activo
*/
    string TextOrient( int index) {

        string textO = "";


        switch (index)
        {
            case 0:
                textO = " \n" +
                "Ubique el cursor sobre la letra roja, mantenga presionado en el ratón el pulsador izquierdo y luego desplace hacia arriba o abajo. El tamaño de la letra representará la magnitud deseada.\n" +
                " \n" +
                "En la parte inferior de la variación de la magnitud se ubica un potenciómetro que permite la variación de la frecuencia y en otros casos el ciclo útil de la señal. Ubique el cursor sobre el potenciómetro, mantenga presionado y varíe horizontalmente según sea indicado.\n" +
                " \n"+
                "En la parte derecha del circuito se ubicó un osciloscopio. Este presentará las señales que interactúan en el circuito. En la parte vertical se visualizará la magnitud de la función y en la horizontal la variación con respecto al tiempo. Aparecerán dos señales diferenciadas con dos tonos, uno rojo y otro azul. Active la opción de dos señales en el recuadro ubicado en la parte inferior de la imagen. Como notará, la señal azul se encuentra adelantada en relación con la señal roja. Con el slider podrá aumentar o disminuir la frecuencia de las señales\n" +
                " \n" +
                "Use el menú en la parte izquierda para cambiar de circuito.\n"+
                " \n" +
                "En la parte inferior derecha se indicarán las “Orientaciones” a realizar. Posteriormente al seguimiento de las orientaciones, acceda a la pestaña “Discusión” para resolver las preguntas complementarias formuladas. ";
                break;
            case 1:
				textO = "1. Antes de iniciar es importante mencionar que la señal de entrada será la generada por la fuente, es una señal cuadrada y estará con tono rojo. La señal de salida será la obtenida en el condensador (capacitor) y estará con tono azul. Ahora bien, \n" + 
				" \n" +
				"2. Mantenga los valores de Frecuencia y Resistencia predeterminada. Es decir, no varíe la magnitud V ni R. Ubique el ciclo útil de la señal en 50%. Observe la señal de voltaje en el capacitor. Compare con la señal con la del generador de frecuencia. ¿Qué forma de onda es? ¿Por qué se produce? \n"+
				" \n" +
				"3. Centre ahora la atención en la corriente en el circuito. Esta se identifica con la flecha en la parte superior. Note que mientras el voltaje crece la corriente disminuye y viceversa. Además, cambia de sentido. ¿Por qué sucede esto? \n" +
				"\n" +
				"4. Ahora, mantenga la frecuencia y resistencia en su valor, pero cambie el ciclo útil de la señal a 10%. Observe la señal de voltaje a la salida del condensador. ¿Qué forma de onda es? ¿Por qué se produce? \n" +
				"\n" +
				"5. Ahora, mantenga la frecuencia y resistencia en su valor, pero cambie el ciclo útil de la señal a 90%. Observe la señal de voltaje a la salida del condensador. ¿Qué forma de onda es? ¿Por qué se produce? \n" +
				"\n" +
				"6. Luego de esto, mantenga el valor de frecuencia y varíe la Resistencia al valor mínimo. Observe la señal de voltaje en el capacitor. En comparación con la señal obtenida en el primer ejercicio, ¿por qué sucede esto? \n" +
				"\n" +
				"7. Ahora, mantenga el valor de frecuencia y varíe la Resistencia al valor máximo. Observe la señal de voltaje en el capacitor. En comparación con la señal obtenida en el ejercicio anterior, ¿qué diferencias nota? ¿a qué se debe esto? \n" +
				"\n" +
				"8. Vamos a centrar la atención en la frecuencia. Ubique el tamaño de la resistencia en un valor intermedio. Luego, varíe la magnitud del generador (V) al valor mínimo manteniendo el ciclo útil de la señal en 50%. Observe la salida de voltaje en el condensador y compare con las señales anteriormente obtenidas. \n" +
				"\n" +
				"9. Ahora, manteniendo el tamaño de la resistencia, varíe la magnitud del generador (V) al valor máximo manteniendo el ciclo útil de la señal en 50%. Observe la salida de voltaje en el condensador y compare con las señales obtenidas anteriormente. Varíe la magnitud del generador al máximo y observe el comportamiento. \n" +
				"\n" +
				"10. Culminados los ejercicios, alterne variaciones del circuito entre la frecuencia y el ciclo útil y posteriormente con la Resistencia. Observe los comportamientos obtenidos en cada variación. ";
				break;
            case 2:
				textO = "1.Recordamos que la señal de entrada será la generada por la fuente, es una señal cuadrada y estará con tono rojo. La señal de salida será la obtenida en la bobina (inductor) y estará con tono azul. Ahora bien, \n" + 
				"\n" +
				"2. Mantenga los valores de Frecuencia y Resistencia predeterminada. Ubique el ciclo útil de la señal en 50%. Observe la señal de voltaje en el inductor. Observe la polaridad del valor de voltaje en la bobina que se indica con el signo + y – al costado del inductor.  Compare con la señal con la del generador de frecuencia. ¿Qué forma de onda es? ¿Por qué se produce?\n" +
				"\n" +
				"3. Centre ahora la atención en la corriente en el circuito. Esta se identifica con la flecha en la parte superior. Centre la atención en el sentido de la corriente en relación con el signo + y – al costado del inductor. ¿Qué sucede en el sentido positivo de la corriente (sale de la fuente por la parte superior)? ¿Qué sucede en el sentido negativo de la corriente (sale de la fuente por la parte superior)? ¿Por qué sucede esto?\n" +
				"\n" +
				"4. Ahora, mantenga la frecuencia y resistencia en su valor, pero cambie el ciclo útil de la señal a 10%. Observe la señal de voltaje a la salida del inductor. ¿Qué forma de onda es? ¿Por qué se produce?\n" +
				"\n" +
				"5. Ahora, mantenga la frecuencia y resistencia en su valor, pero cambie el ciclo útil de la señal a 90%. Observe la señal de voltaje a la salida de la bobina. ¿Qué forma de onda es? ¿Por qué se produce?\n" +
				"\n" +
				"6. Luego de esto, mantenga el valor de frecuencia y varíe la Resistencia al valor mínimo. Observe la señal de voltaje en el inductor. En comparación con la señal obtenida en el primer ejercicio, ¿por qué sucede esto?\n" +
				"\n" +
				"7. Ahora, mantenga el valor de frecuencia y varíe la Resistencia al valor máximo. Observe la señal de voltaje en la bobina. En comparación con la señal obtenida en el ejercicio anterior, ¿qué diferencias nota? ¿a qué se debe esto?\n" +
				"\n" +
				"8. Luego, disminuya el ciclo útil de la señal a 10% y mantenga el valor de frecuencia. Varíe la Resistencia inicialmente al valor mínimo. Observe la señal de voltaje en el inductor. Posteriormente, varíe la resistencia al valor máximo. Observe la señal de voltaje en la bobina. En comparación con la señal obtenida en el ejercicio anterior, ¿qué diferencias observa? ¿a qué se debe esto?\n" +
				"\n" +
				"9. Vamos a centrar la atención en la frecuencia. Ubique el tamaño de la resistencia en un valor intermedio. Luego, varíe la magnitud del generador (V) al valor mínimo manteniendo el ciclo útil de la señal en 50%. Observe la salida de voltaje en el inductor y compare con las señales obtenidas anteriormente. Varíe la magnitud del generador al máximo y observe el comportamiento.\n" +
				"\n" +
				"10. Culminados los ejercicios, alterne variaciones del circuito entre la frecuencia y el ciclo útil y posteriormente con la Resistencia. Observe los comportamientos obtenidos en cada variación.\n";
				break;
            case 3:
				textO = "1. Nuevamente, la señal de entrada será la generada por la fuente, es una señal cuadrada y estará con tono rojo. La señal de salida será la obtenida en el condensador (capacitor) y estará con tono azul. Ahora bien, \n" +
				"\n" +
				"2. Mantenga los valores de Frecuencia y Resistencia predeterminada. Ubique el ciclo útil de la señal en 50%. Observe la señal de voltaje en el condensador. Observe el voltaje en la bobina y en la resistencia que aparece con la letra V. Note que la sigla se invierte indicando que el valor del voltaje es negativo. Compare con la señal con la del generador de frecuencia. ¿Qué forma de onda es? ¿Por qué se produce? \n" +
				"\n" +
				"3. Ahora, mantenga la frecuencia y resistencia en su valor, pero cambie el ciclo útil de la señal a 10%. Observe la señal de voltaje a la salida del condensador. Note que la caída de voltaje no es instantánea ¿Por qué se produce esto?\n" +
				"\n" +
				"4. Ahora, mantenga la frecuencia y resistencia en su valor, pero cambie el ciclo útil de la señal a 90%. Observe la señal de voltaje a la salida del capacitor. Note un leve valor superior del voltaje en el condensador al valor de la fuente ¿Por qué se produce esto?\n" +
				"\n" + 
				"5. Luego de esto, mantenga el valor de frecuencia y varíe la Resistencia al valor mínimo. Observe la señal de voltaje en el condensador. Note que ahora el voltaje en el condensador produce una oscilación sobre el valor de voltaje de la fuente ¿por qué sucede esto?\n" +
				"\n" +
				"6. Luego, disminuya el ciclo útil de la señal a 10% y mantenga el valor de frecuencia. Varíe la Resistencia inicialmente al valor mínimo. Observe la señal de voltaje en el condensador. Posteriormente, varíe la resistencia al valor máximo. Observe la señal de voltaje en el capacitor. En comparación con la señal obtenida en el ejercicio anterior, ¿qué diferencias observa? ¿a qué se debe esto?\n" +
				"\n" +
				"7. Vamos a centrar la atención en la frecuencia. Ubique el tamaño de la resistencia en un valor intermedio. Luego, varíe la magnitud del generador (V) al valor mínimo manteniendo el ciclo útil de la señal en 50%.\n"+
				"\n" +
				"8. Observe la salida de voltaje en el condensador y compare con las señales obtenidas anteriormente. Varíe la magnitud del generador al máximo y observe el comportamiento. \n"+
				"\n" +
				"9. Culminados los ejercicios, alterne variaciones del circuito entre la frecuencia y el ciclo útil y posteriormente con la Resistencia. Observe los comportamientos obtenidos en cada variación.\n";
				break;
            case 4:
				textO = "1. Antes de iniciar es importante mencionar que la fuente de alimentación es de voltaje y es sinusoidal en la que se puede variar la frecuencia más no la amplitud. \n" +
				"\n" +
				"2. Empleando los valores que vienen por defecto en el circuito, observe la amplitud y la frecuencia de la señal de salida de voltaje (de tono azul) en el condensador y compare este resultado con la señal de entrada, es decir, con la señal de voltaje sinusoidal de tono rojo. Note dos comportamientos. La primera, que la amplitud en el condensador es menor y segundo, que existe un desfase de la señal. ¿Esta señal se encuentra adelantada o retrasada con respecto a la señal de entrada? ¿Qué puede decir de la corriente que circula por el circuito? Esta se encuentra simbolizada con una flecha en la parte superior del circuito. \n" +
				"\n" +
				"3. Varíe la frecuencia del generador al mínimo posible. Esto lo hace con la variable V. Observe la señal de salida del voltaje en el condensador. Note que la amplitud de la señal de salida aumentó en relación con el anterior paso. ¿Qué puede decir con respecto al desfase de la señal?\n" +
				"\n" +
				"4. Ahora, varíe la frecuencia del generador al máximo posible. Esto lo hace con la variable V. Observe la señal de salida del voltaje en el condensador. Note que la amplitud de la señal de salida disminuyó en relación con el anterior paso. ¿Qué puede decir con respecto al desfase de la señal?\n" +
				"\n" + 
				"5. Luego, varié la frecuencia del generador a un valor intermedio, es decir, a una magnitud de la letra V intermedia. Varíe la magnitud de la resistencia al mínimo y observe la señal de voltaje en el condensador. Note que la magnitud aumentó con relación al ejercicio anterior. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio? \n" +
				"\n" +
				"6. Ahora, manteniendo la frecuencia del generador a un valor intermedio, varíe la magnitud de la resistencia al máximo y observe la señal de voltaje en el condensador. Note que la magnitud disminuyó con relación al ejercicio anterior. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio? ¿Qué puede decir del comportamiento de la corriente al variar la resistencia?\n" +
				"\n" +
				"7. Culminados los ejercicios, alterne variaciones del circuito entre la frecuencia posteriormente con la Resistencia. Observe los comportamientos obtenidos en cada variación.\n";
				break;
            case 5:
	            textO = "1. Recordamos que la fuente de alimentación de este circuito es de voltaje y es sinusoidal en la que se puede variar la frecuencia más no la amplitud. \n" +
		        "\n" +
		        "2. Empleando los valores que vienen por defecto en el circuito, observe la amplitud y la frecuencia de la señal de salida de voltaje en el inductor. Esta es de tono azul. Compare este resultado con la señal de entrada, es decir, con la señal de voltaje sinusoidal de tono rojo. Note dos comportamientos. La primera, que la amplitud en la bobina es menor y segundo, que existe un desfase de la señal. ¿Esta señal se encuentra adelantada o retrasada con respecto a la señal de entrada? ¿Qué puede decir de la corriente que circula por el circuito? Esta se encuentra simbolizada con una flecha en la parte superior del circuito. \n" +
		        "\n" +
		        "3. Varíe la frecuencia del generador al mínimo posible. Observe la señal de salida del voltaje en la bobina. Note que la amplitud de la señal de salida disminuyó en relación con el anterior paso. ¿Qué puede decir con respecto al desfase de la señal? \n" +
		        "\n" +
		        "4. Ahora, varíe la frecuencia del generador al máximo posible. Observe la señal de salida del voltaje en el inductor. Note que la amplitud de la señal de salida aumentó en relación con el anterior paso. ¿Qué puede decir con respecto al desfase de la señal? \n" +
		        "\n" +
		        "5. Luego, varié la frecuencia del generador a un valor intermedio, es decir, a una magnitud de la letra V intermedia. Varíe la magnitud de la resistencia al mínimo y observe la señal de voltaje en la bobina. Note que la magnitud aumentó con relación al ejercicio anterior. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio? \n" +
		        "\n" +
		        "6. Ahora, manteniendo la frecuencia del generador a un valor intermedio, varíe la magnitud de la resistencia al máximo y observe la señal de voltaje en el inductor. Note que la magnitud disminuyó con relación al ejercicio anterior. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio? ¿Qué puede decir del comportamiento de la corriente al variar la resistencia? \n" +
		        "\n" +
		        "7. Culminados los ejercicios, alterne variaciones del circuito entre la frecuencia posteriormente con la Resistencia. Observe los comportamientos obtenidos en cada variación. \n";
				break;
            case 6:
	            textO =
				"1. Nuevamente, la fuente de alimentación de este circuito es de voltaje y es sinusoidal en la que se puede variar la frecuencia más no la amplitud. \n" +
				"\n" +
				"2. Empleando los valores que vienen por defecto en el circuito, observe la amplitud y la frecuencia de la señal de salida de voltaje en el resistor. Esta es de tono azul. Compare este resultado con la señal de entrada, es decir, con la señal de voltaje sinusoidal de tono rojo. Note dos comportamientos. La primera, que la amplitud en la resistencia es menor y segundo, que existe un desfase de la señal. ¿Esta señal se encuentra adelantada o retrasada con respecto a la señal de entrada? ¿Qué puede decir de la corriente que circula por el circuito? Esta se encuentra simbolizada con una flecha en la parte superior del circuito. \n" +
				"\n" +
				"3. Varíe la frecuencia del generador al mínimo posible. Observe la señal de salida del voltaje en la resistencia. Note que la amplitud de la señal de salida disminuyó en relación con el anterior paso. ¿Qué puede decir con respecto al desfase de la señal? \n" +
				"\n" +
				"4. Ahora, varíe la frecuencia del generador al máximo posible. Observe la señal de salida del voltaje en el resistor. Note que la amplitud de la señal de salida aumentó en relación con el anterior paso. ¿Qué puede decir con respecto al desfase de la señal?\n" +
				"\n" +
				"5. Luego, varié la frecuencia del generador a un valor intermedio, es decir, a una magnitud de la letra V intermedia. Varíe la magnitud de la resistencia al mínimo y observe la señal de voltaje en la bobina. Note que la magnitud aumentó con relación al ejercicio anterior. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio? \n" +
				"\n" +
				"6. Ahora, manteniendo la frecuencia del generador a un valor intermedio, varíe la magnitud del resistor al máximo y observe la señal de voltaje en la resistencia. Note que la magnitud disminuyó con relación al ejercicio anterior. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio? ¿Qué puede decir del comportamiento de la corriente al variar la resistencia?\n" +
				"\n" +
				"7. Culminados los ejercicios, alterne variaciones del circuito entre la frecuencia posteriormente con la Resistencia. Observe los comportamientos obtenidos en cada variación. \n";
				break;
            case 7:
				textO = "1. En este último circuito de esta secuencia la fuente de alimentación de este circuito es de voltaje y es sinusoidal en la que se puede variar la frecuencia más no la amplitud. \n" +
		        "\n" +
		        "2. Empleando los valores que vienen por defecto en el circuito, observe la amplitud y la frecuencia de la señal de salida de voltaje (de tono azul) en resistencia y compare este resultado con la señal de entrada, es decir, con la señal de voltaje sinusoidal de tono rojo. Note dos comportamientos. La primera, que la amplitud en el resistor es menor y segundo, que existe un desfase de la señal. ¿Esta señal se encuentra adelantada o retrasada con respecto a la señal de entrada? ¿Qué puede decir de la corriente que circula por el circuito? Esta se encuentra simbolizada con una flecha en la parte superior del circuito. \n" +
		        "\n" +
		        "3. Varíe la frecuencia del generador al mínimo posible. Observe la señal de salida del voltaje en la resistencia. Note que la amplitud de la señal de salida aumentó en relación con el anterior paso. ¿Qué puede decir con respecto al desfase de la señal? \n" +
		        "\n" +
		        "4. Ahora, varíe la frecuencia del generador al máximo posible. Observe la señal de salida del voltaje en la bobina. Note que la amplitud de la señal de salida disminuyó en relación con el anterior paso. ¿Qué puede decir con respecto al desfase de la señal? \n" +
		        "\n" +
		        "5. Luego, varié la frecuencia del generador a un valor intermedio, es decir, a una magnitud de la letra V intermedia. Varíe la magnitud de la resistencia al mínimo y observe la señal de voltaje en ésta. Note que la magnitud aumentó con relación al ejercicio anterior. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio?  \n" +
		        "\n" +
		        "6. Ahora, manteniendo la frecuencia del generador a un valor intermedio, varíe la magnitud del resistor al máximo y observe la señal de voltaje en la resistencia. Note que la magnitud disminuyó con relación al ejercicio anterior. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio? ¿Qué puede decir del comportamiento de la corriente al variar la resistencia? \n" +
		        "\n" +
		        "7. Culminados los ejercicios, alterne variaciones del circuito entre la frecuencia posteriormente con la Resistencia. Observe los comportamientos obtenidos en cada variación.\n";
				break;
            case 8:
	            textO =
				"1. Es importante mencionar que la fuente de alimentación de este circuito es de corriente y es sinusoidal en la que se puede variar la frecuencia más no la amplitud.  \n" +
				"\n" +
				"2. Empleando los valores que vienen por defecto en el circuito, observe la amplitud y la frecuencia de la señal de salida de corriente en el condensador. Esta es de tono azul. Compare este resultado con la señal de entrada, es decir, con la señal de corriente sinusoidal de tono rojo. Note dos comportamientos. La primera, que la amplitud en el condensador es levemente menor y segundo, que existe un desfase de la señal. ¿Esta señal se encuentra adelantada o retrasada con respecto a la señal de entrada? ¿Qué puede decir de la corriente que circula por la resistencia? Esta se encuentra simbolizada con una flecha en la parte superior del circuito. \n" +
				"\n" +
				"3. Varíe la frecuencia del generador al mínimo posible. Observe la señal de salida de corriente en el condensador. Note que la amplitud de la señal de salida disminuyó en relación con el anterior paso y el desfase es más notorio. ¿Qué puede decir con respecto al desfase de la señal? ¿Qué puede decir de la corriente por la resistencia? \n" +
				"\n" +
				"4. Ahora, varíe la frecuencia del generador al máximo posible. Observe la señal de salida de corriente. Note que la amplitud de la señal de salida aumentó en relación con el anterior paso. ¿Qué puede decir con respecto al desfase de la señal? ¿Qué puede decir de la corriente por la resistencia? \n" +
				"\n" +
				"5. Luego, varié la frecuencia del generador a un valor intermedio. Varíe la magnitud de la resistencia al mínimo y observe la señal de voltaje en el condensador. Note que la magnitud de corriente disminuyó. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio? \n" +
				"\n" +
				"6. Ahora, manteniendo la frecuencia del generador a un valor intermedio, varíe la magnitud de la resistencia al máximo y observe la señal de corriente en el capacitor. Note que la magnitud aumentó con relación al ejercicio anterior. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio? ¿Qué puede decir del comportamiento de la corriente al variar la resistencia? \n" +
				"\n" +
				"7. Culminados los ejercicios, alterne variaciones del circuito entre la frecuencia posteriormente con la Resistencia. Observe los comportamientos obtenidos en cada variación.\n";
				break;
            case 9:
				textO = "1. Tenga en cuenta que la fuente de alimentación de este circuito es de corriente y es sinusoidal en la que se puede variar la frecuencia más no la amplitud. \n" +
		        "\n" +
		        "2. Empleando los valores que vienen por defecto en el circuito, observe la amplitud y la frecuencia de la señal de salida de corriente Vs la corriente de la fuente. Note también dos comportamientos. Lo primero, que la amplitud en el inductor es menor y segundo, que existe un desfase de la señal. ¿Esta señal se encuentra adelantada o retrasada con respecto a la señal de entrada? ¿Qué puede decir de la corriente que circula por el circuito? ¿De la corriente en la resistencia en relación con la corriente en la bobina? \n" +
		        "\n" +
		        "3. Varíe la frecuencia del generador al mínimo posible. Observe la señal de salida en la bobina. Note que la amplitud de la señal de salida aumentó en relación con el anterior paso. ¿Qué puede decir con respecto al desfase de la señal? \n" +
		        "\n" +
		        "4. Ahora, varíe la frecuencia del generador al máximo posible. Observe la señal de salida. Note que la amplitud de la señal de salida disminuyó en relación con el anterior paso. ¿Qué puede decir con respecto al desfase de la señal?¿Qué pasa con la corriente en la resistencia Vs la corriente en el inductor? \n" +
		        "\n" +
		        "5. Luego, varié la frecuencia del generador a un valor intermedio y cambie la magnitud de la resistencia al mínimo. Observe la señal de corriente en el inductor. Note que la magnitud disminuyó con relación al ejercicio anterior. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio?  \n" +
		        "\n" +
		        "6. Ahora, manteniendo la frecuencia del generador, varíe la magnitud de la resistencia al máximo y observe la señal de salida nuevamente. Note que la magnitud aumentó con relación al ejercicio anterior. ¿Qué puede decir del desfase de la señal en relación con el anterior ejercicio? ¿Qué puede decir del comportamiento de la corriente al variar la resistencia? \n" +
		        "\n" +
		        "7. Culminados los ejercicios, alterne variaciones del circuito entre la frecuencia posteriormente con la Resistencia. Observe los comportamientos obtenidos en cada variación.\n";
				break;
            case 10:
				textO = "1. En este circuito cambiamos la fuente de corriente de los circuitos previos, por una fuente de voltaje. Nuevamente, se puede variar la frecuencia más no la amplitud. El circuito es una variación del circuito RC en paralelo, pero en este caso hemos ubicado un divisor de voltaje del cual es posible variar sus parámetros. \n" +
		        "\n" +
		        "2. Sin realizar variaciones en los parámetros, observe el voltaje en el condensador. Note que la amplitud es menor que la de la fuente. \n" +
		        "\n" +
		        "3. Varíe la frecuencia de la fuente al mínimo y observe. Luego, varíe al máximo y observe. ¿Qué puede decir de la amplitud y de la fase en los dos casos? \n" +
		        "\n" +
		        "4. Modifique la magnitud de la frecuencia en un valor intermedio. Varíe la magnitud de la resistencia conectada al generador al mínimo. Observe la salida en el condensador. Ahora, varíe la resistencia al máximo y observe. ¿Qué puede decir de la amplitud y fase en el condensador? \n" +
		        "\n" +
		        "5. Mantenga la frecuencia y la resistencia conectada al generador en un valor intermedio. Varíe la magnitud al mínimo de la resistencia conectada en paralelo al condensador. Observe la salida en el capacitor. Ahora, varíe la resistencia al máximo y observe. ¿Qué puede decir de la amplitud y fase en el condensador? \n";
				break;
            case 11:
	            textO =
				"1. Este circuito se encuentra alimentado por una de voltaje sinusoidal. Se puede variar la frecuencia más no la amplitud. La señal visualizada será el voltaje en el condensador. \n" +
				"\n" +
				"3. Mantenga los valores de Frecuencia y Resistencia predeterminada. Note que el voltaje en el condensador es levemente inferior que la fuente, pero desfasado. En este caso ¿el voltaje está adelantado o retrasado?   Observe el voltaje en la bobina y en la resistencia que aparece con la letra V. Compare con la señal con la del generador de frecuencia. Ahora, compare el voltaje del inductor y el resistor en relación con el voltaje en el condensador ¿Qué puede afirmar de las señales comparadas?\n" +
				"\n" +
				"4. Luego de esto, mantenga el valor de frecuencia y varíe la Resistencia al valor mínimo. Observe la señal de voltaje en el condensador. Note que ahora el voltaje en el condensador es mayor que el de la fuente ¿por qué sucede esto? Ahora, aumente el valor de la resistencia al máximo y observe. Note que el voltaje en el condensador disminuye y aumenta el desfase. ¿Qué puede decir del voltaje en la bobina? \n" +
				"\n" +
				"5. Ahora, varíe la magnitud de la resistencia en el punto en el que el voltaje en el condensador sea el mismo de la fuente. Luego varíe la frecuencia al mínimo posible y observe ¿Qué puede indicar del cambio obtenido? Ahora aumente la frecuencia al máximo posible ¿Qué puede decir de la magnitud y frecuencia en el condensador? ¿Qué puede decir del voltaje en la bobina? \n" +
				"\n" +
				"6. Culminados los ejercicios, alterne variaciones del circuito entre la frecuencia y el ciclo útil y posteriormente con la Resistencia. Observe los comportamientos obtenidos en cada variación.\n";
	            break;
            default:
	            break;
        }

        return textO;

    }
// =====================================================================================================
/// Sirve para actualizar el texto \a Discuciones del panel inferior según el circuito activo.
/**
  \param index indice del circuito activo
  \return texto asociado al circuito activo
*/
    string TextDiscu(int index)
    {

        string textD;

        switch (index)
        {
	        case 0:
		        textD = "";
			        
                break;
			case 1:
				textD = "1. En la primera condición del ejercicio, qué sucedería en la señal de salida si la frecuencia del generador fuera mucho menor manteniendo el ciclo útil.  \n" +
				        " \n" +
				        "2. Cuando el ciclo útil de la señal es de 100%, ¿qué pasaría en un tiempo prolongado? \n" +
				        "\n" +
				        "3. Pasado este tiempo, ¿qué sucedería si pasa el ciclo útil de la señal a cero y lo deja por mucho tiempo? \n" +
				        "\n" +
				        "4. Si volvemos al caso de tener un c¿Qué sucedería si el valor de la resistencia se acerca a un valor muy pequeño (casi cero), manteniendo el voltaje sin modificarlo? ¿Qué sucedería si quitamos la resistencia? ¿Qué sucedería si la resistencia tiende a infinito \n" +
				        "\n" +
				        "5. Supongamos que el generador, manteniendo un ciclo útil de 50%, tiene ahora valores positivos y negativos. Es como tener la misma señal generada, pero en el punto cero del osciloscopio. ¿Qué señal de voltaje esperaría obtener a la salida del capacitor? \n" +
				        " \n" +
				        "6. En esta misma condición, ¿Qué señal espera obtener en la resistencia? \n" +
				        " \n" +
				        "7. ¿Por qué se da un cambio en el sentido de la corriente en el condensador? \n" +
				        " \n" +
				        "8. Note que las corrientes cambian tanto de magnitud como de sentido. ¿Qué podría decir de la corriente que circula por el condensador? \n" +
				        " \n" +
				        "9. Luego de alternar variaciones entre el generador de funciones, el ciclo útil de la señal y la resistencia en el sistema, establezca conclusiones para cada variable modificada. \n" +
				        " \n" +
				        "10. ¿Es posible establecer una relación de proporcionalidad de lo que observa?  \n";
				break;
			case 2:
				textD = "1. ¿Hay alguna relación de la resistencia con la corriente que circulará en el circuito? \n" +
				        " \n" +
				        "2. ¿Qué función cumple entonces la resistencia en el circuito? \n" +
				        "\n" +
				        "3. En la primera condición del ejercicio, qué sucedería en la señal de salida si la frecuencia del generador fuera mucho menor manteniendo el ciclo útil.  \n" +
				        "\n" +
				        "4. Cuando el ciclo útil de la señal es de 100%, ¿qué pasaría en un tiempo prolongado? \n" +
				        "\n" +
				        "5. Pasado este tiempo, ¿qué sucedería si pasa el ciclo útil de la señal a cero y lo deja por mucho tiempo? \n" +
				        "\n" +
				        "6. Si volvemos al caso de tener un ciclo útil de 50 ¿Qué sucedería si el valor de la resistencia se acerca a un valor muy pequeño (casi cero), manteniendo el voltaje sin modificarlo? ¿Qué sucedería si quitamos la resistencia? ¿Qué sucedería si la resistencia tiende a infinito? \n" +
				        "\n" +
				        "7. Supongamos que el generador, manteniendo un ciclo útil de 50%, tiene ahora valores positivos y negativos. Es como tener la misma señal generada, pero en el punto cero del osciloscopio. ¿Qué señal de voltaje esperaría obtener a la salida del capacitor? \n" +
				        "\n" +
				        "8. En esta misma condición, ¿Qué señal espera obtener en la resistencia?\n" +
				        "\n" +
				        "9. ¿Por qué se da un cambio en el sentido de la corriente en el inductor? \n" +
				        "\n" +
				        "10. Note que las corrientes cambian tanto de magnitud como de sentido. ¿Qué podría decir de la corriente que circula por el inductor? \n" +
				        "\n" +
				        "11. Luego de alternar variaciones entre el generador de funciones, el ciclo útil de la señal y la resistencia en el sistema, establezca conclusiones para cada variable modificada. \n" +
						"\n" +
						"12. ¿Es posible establecer una relación de proporcionalidad de lo que observa? \n";
				break;
            case 3:
				textD = "1. ¿Qué señales se estarían produciendo en el inductor y la resistencia? \n" +
				        " \n" +
				        "2. ¿Hay alguna relación de la resistencia con la corriente que circulará en el circuito? \n" +
				        "\n" +
				        "3. ¿Qué sucedería si el circuito no tuviera conectado una resistencia? ¿Qué función cumple entonces la resistencia en el circuito?  \n" +
				        "\n" +
				        "4. En la primera condición del ejercicio, qué sucedería en la señal de salida si la frecuencia del generador fuera mucho menor manteniendo el ciclo útil. \n" +
				        "\n" +
				        "5. Cuando el ciclo útil de la señal es de 100%, ¿qué pasaría en un tiempo prolongado?  \n" +
				        "\n" +
				        "6. Pasado este tiempo, ¿qué sucedería si pasa el ciclo útil de la señal a cero y lo deja por mucho tiempo? \n" +
				        "\n" +
				        "7. Note que las corrientes cambian tanto de magnitud como de sentido. ¿Qué podría decir de la corriente que circula por el inductor? \n" +
				        "\n" +
				        "8. Si volvemos al caso de tener un ciclo útil de 50% ¿Qué sucedería si el valor de la resistencia se acerca a un valor muy pequeño (casi cero), manteniendo el voltaje sin modificarlo? ¿Qué sucedería si quitamos la resistencia? ¿Qué sucedería si la resistencia tiende a infinito? \n" +
				        "\n" +
				        "9. Supongamos que el generador, manteniendo un ciclo útil de 50%, tiene ahora valores positivos y negativos. Es como tener la misma señal generada, pero en el punto cero del osciloscopio. ¿Qué señal de voltaje esperaría obtener a la salida del capacitor? \n" +
				        "\n" +
				        "10. En esta misma condición, ¿Qué señal espera obtener en la resistencia? \n" +
				        "\n" +
				        "11. Luego de alternar variaciones entre el generador de funciones, el ciclo útil de la señal y la resistencia en el sistema, establezca conclusiones para cada variable modificada. \n" +
				        "\n" +
						"12. ¿Es posible establecer una relación de proporcionalidad de lo que observa? \n";
				break;
            case 4:
				textD = "1. Las señales obtenidas nos muestran que a medida que varía la frecuencia y mantengo la amplitud del generador, la salida de voltaje en el condensador se ve afectada. ¿Qué puede decir en relación del aumento de la frecuencia y el comportamiento del voltaje en el condensador? ¿Qué sucedería si la frecuencia aumenta hacia infinito? Y en caso contrario, ¿qué sucedería si la frecuencia disminuye hacia cero? \n" +
				        " \n" +
				        "2. ¿Por qué se experimenta un desfase en la señal? ¿De qué depende que este desfase sea mayor o menor? \n" +
				        "\n" +
				        "3. ¿Qué función cumple la resistencia en el circuito? \n" +
				        "\n" +
				        "4. ¿Qué sucedería cuando la resistencia tiende a cero en relación con el voltaje en el condensador?  \n" +
				        "\n" +
				        "5. ¿Qué sucedería cuando la resistencia tiende a cero en relación con el desfase?  \n" +
				        " \n" +
				        "6. ¿Qué sucedería cuando la resistencia tiende a infinito en relación con el voltaje en el condensador?  \n" +
				        "\n" +
				        "7. ¿Qué sucedería cuando la resistencia tiende a infinito en relación con el desfase?  \n" +
				        "\n" +
				        "8. ¿Es posible establecer una relación de proporcionalidad de lo que observa?\n";
                break;
			case 5:
				textD = "1. Las señales obtenidas nos muestran que a medida que varía la frecuencia y mantengo la amplitud del generador, la salida de voltaje en la bobina se ve afectada. ¿Qué puede decir en relación del aumento de la frecuencia y el comportamiento del voltaje en el inductor? ¿Qué sucedería si la frecuencia aumenta hacia infinito? Y en caso contrario, ¿qué sucedería si la frecuencia disminuye hacia cero? \n" +
				        " \n" +
				        "2. ¿Por qué se experimenta un desfase en la señal? ¿De qué depende que este desfase sea mayor o menor?\n" +
				        "\n" +
				        "3. ¿Qué función cumple la resistencia en el circuito?\n" +
				        "\n" +
				        "4. ¿Qué sucedería cuando la resistencia tiende a cero en relación con el voltaje en el inductor? \n" +
				        "\n" +
				        "5. ¿Qué sucedería cuando la resistencia tiende a cero en relación con el desfase?  \n" +
				        " \n" +
				        "6. ¿Qué sucedería cuando la resistencia tiende a infinito en relación con el voltaje en la bobina? \n" +
				        "\n" +
				        "7. ¿Qué sucedería cuando la resistencia tiende a infinito en relación con el desfase?  \n" +
				        "\n" +
				        "8. ¿Es posible establecer una relación de proporcionalidad de lo que observa? \n";
				break;
            case 6:
				textD = "1. Las señales obtenidas nos muestran que a medida que varía la frecuencia y mantengo la amplitud del generador, la salida de voltaje en la resistencia se ve afectada. ¿Qué puede decir en relación del aumento de la frecuencia y el comportamiento del voltaje en la resistencia? ¿Qué sucedería si la frecuencia aumenta hacia infinito? Y en caso contrario, ¿qué sucedería si la frecuencia disminuye hacia cero? \n" +
				        " \n" +
				        "2. ¿Por qué se experimenta un desfase en la señal? ¿De qué depende que este desfase sea mayor o menor? \n" +
				        "\n" +
				        "3. ¿Qué función cumple la resistencia en el circuito? \n" +
				        "\n" +
				        "4. ¿Qué sucedería cuando la resistencia tiende a cero en relación con el voltaje en el condensador?  \n" +
				        "\n" +
				        "5. ¿Qué sucedería cuando la resistencia tiende a cero en relación con el desfase? \n" +
				        "\n" +
				        "6. ¿Qué sucedería cuando la resistencia tiende a infinito en relación con el voltaje en el condensador?  \n" +
				        "\n" +
				        "7. ¿Qué sucedería cuando la resistencia tiende a infinito en relación con el desfase? \n" +
				        "\n" +
				        "8. ¿Qué sucedería si mantengo el valor de la resistencia en un valor intermedio y aumento el valor del condensador? \n" +
				        "\n" +
				        "9. ¿Es posible establecer una relación de proporcionalidad de lo que observa? \n";
				break;
            case 7:
				textD = "1. Las señales obtenidas nos muestran que a medida que varía la frecuencia y mantengo la amplitud del generador, la salida de voltaje en la resistencia se ve afectada. ¿Qué puede decir en relación del aumento de la frecuencia y el comportamiento del voltaje en el resistor? ¿Qué sucedería si la frecuencia aumenta hacia infinito? Y en caso contrario, ¿qué sucedería si la frecuencia disminuye hacia cero? \n" +
		                " \n" +
		                "2. ¿Por qué se experimenta un desfase en la señal? ¿De qué depende que este desfase sea mayor o menor? \n" +
		                "\n" +
		                "3. ¿Qué función cumple la resistencia en el circuito? \n" +
		                "\n" +
		                "4. ¿Qué sucedería cuando la resistencia tiende a cero en relación con el voltaje en la bobina?  \n" +
		                "\n" +
		                "5. ¿Qué sucedería cuando la resistencia tiende a cero en relación con el desfase? \n" +
		                "\n" +
		                "6. ¿Qué sucedería cuando la resistencia tiende a infinito en relación con el voltaje en la bobina?  \n" +
		                "\n" +
		                "7. ¿Qué sucedería cuando la resistencia tiende a infinito en relación con el desfase? \n" +
		                "\n" +
		                "8. ¿Qué sucedería si mantengo el valor de la resistencia en un valor intermedio y aumento el valor de la bobina? \n" +
		                "\n" +
		                "9. ¿Es posible establecer una relación de proporcionalidad de lo que observa?\n";
				break;
            case 8:
				textD = "1. Las señales obtenidas nos muestran que a medida que varía la frecuencia y mantengo la amplitud del generador, la salida de corriente en el condensador se ve afectada. ¿Qué puede decir en relación del aumento de la frecuencia y el comportamiento de corriente en el condensador? ¿Qué sucedería si la frecuencia aumenta hacia infinito? Y en caso contrario, ¿qué sucedería si la frecuencia disminuye hacia cero? \n" +
	                	"\n" +
	                	"2. ¿Qué sucedería cuando la resistencia tiende a cero en relación con la corriente en el capacitor? \n" +
	                	"\n" +
	                	"3. ¿Qué sucedería cuando la resistencia tiende a cero en relación con el desfase? \n" +
	                	"\n" +
	                	"4. ¿Qué sucedería cuando la resistencia tiende a infinito en relación con la corriente en el condensador? \n" +
	                	"\n" +
	                	"5. ¿Qué sucedería cuando la capacitancia tiende a infinito en relación con el desfase?  \n" +
	                	"\n" +
	                	"6. ¿Qué puede decir del desfase si lo compara con un circuito RC en serie, pero con fuente de voltaje? \n" +
	                	"\n" +
	                	"7. ¿Es posible establecer una relación de proporcionalidad de lo que observa? \n";
				break;
            case 9:
				textD = "1. Las señales obtenidas nos muestran que a medida que varía la frecuencia y mantengo la amplitud del generador, la salida de voltaje en el inductor se ve afectada. ¿Qué puede decir en relación del aumento de la frecuencia y el comportamiento de corriente en el inductor? \n" +
	                	" \n" +
	                	"2. ¿Qué sucedería si la frecuencia aumenta hacia infinito? Y en caso contrario, ¿qué sucedería si la frecuencia disminuye hacia cero? \n" +
	                	"\n" +
	                	"3. ¿Por qué se experimenta un desfase en la señal? ¿De qué depende que este desfase sea mayor o menor? \n" +
	                	"\n" +
	                	"4. ¿Qué función cumple la resistencia en el circuito? \n" +
	                	"\n" +
	                	"5. ¿Qué sucedería cuando la resistencia tiende a cero en relación con la corriente en la bobina?  \n" +
	                	"\n" +
	                	"6. ¿Qué sucedería cuando la resistencia tiende a cero en relación con el desfase? \n" +
	                	"\n" +
	                	"7. ¿Qué sucedería cuando la resistencia tiende a infinito en relación con la corriente en la bobina? \n" +
	                	"\n" +
	                	"8. ¿Qué sucedería cuando la resistencia tiende a infinito en relación con el desfase? \n" +
	                	" \n" +
	                	"9. ¿Qué puede decir del desfase si lo compara con un circuito RL en serie, pero con fuente de voltaje? \n" +
	                	"\n" +
	                	"10. ¿Qué sucedería cuando la inductancia tiende a infinito en relación con el desfase?  \n" +
	                	"\n" +
	                	"11. ¿Es posible establecer una relación de proporcionalidad de lo que observa?\n";
				break;
            case 10:
				textD = "1. Si comparamos el comportamiento de este circuito con el del circuito en paralelo RC con alimentación de una fuente de corriente, ¿qué puede indicar con respecto a la amplitud de salida y al desfase generado? \n" +
	                	" \n" +
	                	"2. Qué función cumple la resistencia conectada al generador en el comportamiento del circuito en general. \n" +
	                	"\n" +
	                	"3. ¿Qué sucedería si la frecuencia aumenta hacia infinito? Y en caso contrario, ¿qué sucedería si la frecuencia disminuye hacia cero? \n" +
	                	"\n" +
	                	"4. ¿Qué sucedería cuando la capacitancia tiende a infinito en el comportamiento del circuito? \n" +
	                	"\n" +
	                	"5. ¿Es posible establecer una relación de proporcionalidad de lo que observa?\n";
				break;
            case 11:
	            textD = "1. ¿Qué señales se estarían produciendo en el inductor y la resistencia? \n" +
	                	" \n" +
	                	"2. ¿Qué sucedería si el circuito no tuviera conectado una resistencia? \n" +
	                	"\n" +
	                	"3. ¿Qué función cumple entonces la resistencia en el circuito? \n" +
	                	"\n" +
	                	"4. ¿Qué podría decir de la corriente que circula por el inductor? \n" +
	                	"\n" +
	                	"5. Si compara el comportamiento de este circuito alimentado con fuente sinusoidal en relación al mismo circuito pero alimentado con una señal pulso ¿Qué comportamientos semejantes o contrarios encontró? \n";
	            break;
            default:
	            textD = "";
                break;
        }

        return textD;

    }
// =====================================================================================================
/// Se utiliza para activar e inicializar el \a circuito que ha sido selecionado desde el menú
/**
  \param indice del circuito activo
*/
    public void CircuitsVisualization( int index ) {

        Circuits.GetChild(lastCircuitIndex).gameObject.SetActive(false);
        Circuits.GetChild(index).gameObject.SetActive(true);

        lastCircuitIndex = index;
		// aca se puede cambiar el tamaño del container del texto 
        OrientText.text = TextOrient(index);
        OriScroll.value = 1;

        DiscusText.text = TextDiscu(index);
        DisScroll.value = 1;

        axes.GetComponent<AxisSin>().ResetLineAxes();
        _contenOrientacion.GetComponent<RectTransform>().sizeDelta = new Vector2(0,sizeOrientaciones(index));
        _contenDiscucion.GetComponent<RectTransform>().sizeDelta = new Vector2(0,sizeDiscuciones(index));
}

//=======================================================================================================
//=================== tamaño de los contenidos ==========================================================
//=======================================================================================================

	public float sizeOrientaciones(int index)
	{
		float sizeContent = 0;
		switch (index)
		{
			case 0:
				sizeContent = 180f;
				break;
			case 1:
				sizeContent = 425f;
				break;
			case 2:
				sizeContent = 420f;
				break;
			case 3:
				sizeContent = 330f;
				break;
			case 4:
				sizeContent = 330f;
				break;
			case 5:
				sizeContent = 330f;
				break;
			case 6:
				sizeContent = 330f;
				break;
			case 7:
				sizeContent = 330f;
				break;
			case 8:
				sizeContent = 330f;
				break;
			case 9:
				sizeContent = 320f;
				break;
			case 10:
				sizeContent = 200f;
				break;
			case 11:
				sizeContent = 230f;
				break;
		}
		return sizeContent;
	}
	
	public float sizeDiscuciones(int index)
	{
		float sizeContent = 0;
		switch (index)
		{
			case 0:
				sizeContent = 0;
				break;
			case 1:
				sizeContent = 350f;
				break;
			case 2:
				sizeContent = 380f;
				break;
			case 3:
				sizeContent = 380f;
				break;
			case 4:
				sizeContent = 280f;
				break;
			case 5:
				sizeContent = 270f;
				break;
			case 6:
				sizeContent = 300f;
				break;
			case 7:
				sizeContent = 300f;
				break;
			case 8:
				sizeContent = 230f;
				break;
			case 9:
				sizeContent = 360f;
				break;
			case 10:
				sizeContent = 170f;
				break;
			case 11:
				sizeContent = 150f;
				break;
		}
		return sizeContent;
	}
}
