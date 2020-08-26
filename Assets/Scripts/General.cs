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


// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles y selecciona el circuito inicial.
*/
    void Start () {

        lastCircuitIndex = 0;
        OrientText.text = TextOrient(0);
    }
// =====================================================================================================
/// Sirve para actualizar el texto \a Orientaciones del panel inferior según el circuito activo.
/**
  \param index indice del circuito activo
  \return texto asociado al circuito activo
*/
    string TextOrient( int index) {

        string textO;


        switch (index)
        {
            case 0:
                textO = " \n" +
                "Ubique el cursor sobre la letra roja, luego arrastre hacia arriba o hacia abajo, el tamaño de la letra representa la magnitud de la variable\n" +
                " \n" +
                "Use el menú de la izquierda para cambiar de circuito";
                break;
            case 1:
				textO = "① Note que para este circuito la entrada es el voltaje de la fuente y la salida es el voltaje en el capacitor\n" +
				"② Realice cambios en el voltaje de entrada, note que el voltaje en el capacitor tiende a tomar el mismo tamaño que el voltaje de la fuente, pero tarda cierto tiempo en responder, a ese tiempo se le llama periodo transitorio\n" +
				"③ Note que cuando la resistencia es baja el periodo transitorio toma menos tiempo, mientras que, si la resistencia es alta el transitorio es mucho más demorado\n" +
				"④ Cuando el periodo transitorio acaba se dice que el sistema está en estado estacionario. Note que en estado estacionario la corriente tiende a 0\n" +
				"⑤ Note que al hacer variaciones pronunciadas en la fuente el voltaje en el condensador cambia de forma continua mientras que la corriente cambia de forma pronunciada. Esto se debe a que cuando hay presencia de capacitores el voltaje solo cambia de forma continua";
				break;
            case 2:
				textO = "① Note que para este circuito la entrada es el voltaje de la fuente y la salida es el voltaje en el inductor\n" +
				"② Realice cambios en el voltaje de entrada, note que el voltaje en el inductor tiende a volverse cero, pero tarda cierto tiempo en responder, a ese tiempo se le llama periodo transitorio\n" +
				"③ Note que cuando la resistencia es alta el periodo transitorio toma menos tiempo, mientras que si la resistencia es baja el transitorio es mucho más demorado\n" +
				"④ Cuando el periodo transitorio acaba se dice que el sistema está en estado estacionario. Note que en estado estacionario la corriente tiende a un tamaño constante\n" +
				"⑤ Note que al hacer variaciones pronunciadas en la fuente la corriente cambia de forma continua mientras que el voltaje de salida cambia de forma pronunciada. Esto se debe a que cuando hay presencia de inductores la corriente solo cambia de forma continua";
				break;
            case 3:
				textO = "① Note que en este circuito la entrada es la corriente de la fuente y la salida es la corriente en la resistencia\n" +
				"② Realice cambios en la corriente de entrada, note que la corriente en la resistencia tiende a tomar el mismo tamaño que la corriente de la fuente, pero tarda cierto tiempo en responder, ese será el periodo transitorio asociado a este circuito\n" +
				"③ Note que cuando la resistencia es baja el periodo transitorio toma menos tiempo, mientras que, si la resistencia es alta el transitorio es mucho más demorado\n" +
				"④ Cuando el periodo transitorio acaba se dice que el sistema está en estado estacionario. Note que en estado estacionario la corriente tiende a un valor constante";
				break;
            case 4:
				textO = "① Note que en este circuito la entrada es la corriente de la fuente y la salida es la corriente en la resistencia\n" +
				"② Realice cambios en la corriente de entrada, note que la corriente en la resistencia tiende volverse cero, pero tarda cierto tiempo en responder, ese será el periodo transitorio asociado a este circuito\n" +
				"③ Note que cuando la resistencia es alta el periodo transitorio toma menos tiempo, mientras que, si la resistencia es baja el transitorio es mucho más demorado\n" +
				"④ Cuando el periodo transitorio acaba se dice que el sistema está en estado estacionario. Note que en estado estacionario la corriente tiende a un valor constante";
				break;
            case 5:
				textO = "① Note que en este circuito la entrada es el voltaje de la fuente y la salida es el voltaje en el condensador, además cuenta con dos resistencias variables\n" +
				"② Analizando el comportamiento en estado estacionario: el capacitor se comportará como un circuito abierto dejando un circuito con un divisor de voltaje, ahora, como el capacitor está en paralelo con una de las resistencias estos dos elementos tienen el mismo voltaje, por lo cual la salida en estado estacionario corresponde con el voltaje de la resistencia ubicada de forma vertical\n" +
				"③ Deje las dos resistencias del mismo tamaño y varíe el voltaje de la fuente, note que el voltaje del condensador tiende a la mitad del voltaje de la fuente en estado estacionario\n" +
				"④ Deje las dos resistencias en el tamaño mínimo posible y varíe el voltaje de la fuente, note que el periodo transitorio tiene una duración mucho menor, mientras que si las dos resistencias están al máximo el periodo transitorio dura mucho más";
				break;
            case 6:
				textO = "① Note que en este circuito tiene los tres componentes básicos, la entrada es el voltaje de la fuente y la salida es el voltaje en el condensador, además la resistencia es variable\n" +
				"② Realice cambios en el voltaje de entrada, note que el voltaje en el capacitor tiende a tomar el mismo tamaño que el voltaje de la fuente una vez transcurre el periodo transitorio\n" +
				"③ En este circuito el tamaño de la resistencia cambia la forma de la respuesta del sistema\n" +
				"④ Deje al mínimo posible el tamaño de la resistencia, note que al variar la entrada en forma escalón la forma de la respuesta tiene un sobrepico, es decir el valor de la salida supera por unos segundos el valor de la entrada. A esto se le conoce como un comportamiento sobreamortiguado\n" +
				"⑤ Deje al máximo posible el tamaño de la resistencia, note que al variar la entrada en forma escalón la forma de la respuesta es exponencial. A esto se le conoce como un comportamiento sub amortiguado\n" +
				"⑥ Cuando hay sobrepicos se excede el voltaje de la fuente, esto es posible debido a los elementos que almacenan energía. Sin embargo, la sumatoria de los voltajes de la malla debe ser cero, por lo tanto alguno de los demás componentes debe tener durante los sobrepicos un valor negativo de voltaje";
				break;
            case 7:
				textO = " ";
				break;
            case 8:
				textO = " ";
				break;
            case 9:
				textO = " ";
				break;
            case 10:
				textO = " ";
				break;
            default:
                textO = "";
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


        switch (index){
            case 0:
                textD = "";
                break;
			case 1:
				textD = "① ¿Por qué en estado estacionario la variación de la resistencia no genera ningún efecto en el voltaje del capacitor? \n" +
				"② Se dice que en estado estacionario los condensadores se comportan como circuitos abiertos si la fuente de alimentación es constante ¿este circuito permite verificar esa afirmación?\n" +
				"③ Si le pidieran que grafique en los mismos ejes el comportamiento del voltaje en la resistencia. ¿cuál sería la imagen aproximada que obtendría?";
				break;
			case 2:
				textD = "① ¿Cuál es el efecto que produce la variación de la resistencia cuando el circuito está en estado estacionario?\n" +
				"② El voltaje en el inductor en algunos casos es negativo. ¿En este circuito es posible que el voltaje de la resistencia sea negativo?\n" +
				"③ Se dice que en estado estacionario los inductores se comportan como corto circuito si la fuente de alimentación es constante ¿este circuito permite verificar esa afirmación?\n" +
				"④ Si le pidieran que grafique en los mismos ejes el comportamiento del voltaje en la resistencia. ¿cuál sería la imagen aproximada que obtendría? ";
				break;
            case 3:
				textD = "① Se dice que en estado estacionario los condensadores se comportan como circuitos abiertos si la fuente de alimentación es constante ¿este circuito permite verificar esa afirmación?\n" +
				"② ¿Por qué en estado estacionario la variación de la resistencia no genera ningún efecto en la corriente de la resistencia?\n" +
				"③ Si le pidieran que grafique en los mismos ejes el comportamiento de la corriente en el capacitor. ¿cuál sería la imagen aproximada que obtendría?\n" +
				"④ Note que al hacer variaciones pronunciadas en la fuente el sentido de la corriente en el capacitor cambia ¿Cuáles son las condiciones para que esto ocurra? ¿Cómo se podría explicar eso teniendo en cuenta que el capacitor es un elemento que almacena energía?\n" +
				"⑤ Si el valor de la resistencia tendiera a infinito, ¿cuál sería el valor de voltaje asociado a ésta?\n" +
				"⑥ Si tuviera una tercera resistencia conectada siguiendo el patrón en paralelo ¿cuáles efectos causaría en las variables del circuito?\n" +
				"⑦ Existe una gran similitud en el comportamiento de este circuito con respecto al circuito RC alimentado con fuente de voltaje, ¿desde el análisis de las funciones de transferencia se podría estudiar esta similitud?";
				break;
            case 4:
				textD = "① Se dice que en estado estacionario los inductores se comportan como corto circuito si la fuente de alimentación es constante ¿este circuito permite verificar esa afirmación?\n" +
				"② ¿Por qué en estado estacionario la variación de la resistencia no genera ningún efecto en la corriente de la resistencia? \n" +
				"③ Si le pidieran que grafique en los mismos ejes el comportamiento de la corriente en el inductor. ¿cuál sería la imagen aproximada que obtendría?\n" +
				"④ Note que al hacer variaciones pronunciadas en la fuente el sentido de la corriente en la resistencia cambia ¿Cuáles son las condiciones para que esto ocurra? ¿Cómo se podría explicar eso teniendo en cuenta que el inductor es un elemento que almacena energía?\n" +
				"⑤ Si el valor de la resistencia tendiera a infinito, ¿cuál sería el valor de corriente asociado a ésta?\n" +
				"⑥ Si tuviera una tercera resistencia conectada siguiendo el patrón en paralelo ¿cuáles efectos causaría en las variables del circuito?\n" +
				"⑦ Existe una gran similitud en el comportamiento de este circuito con respecto al circuito RL alimentado con fuente de voltaje, ¿desde el análisis de las funciones de transferencia se podría estudiar esta similitud?";
                break;
			case 5:
				textD = "① ¿Cuáles condiciones deben cumplir las resistencias para que la salida en estado estacionario sea la máxima posible?\n" +
				"② ¿Cuáles condiciones deben cumplir las resistencias para que la salida en estado estacionario sea la mínima posible?\n" +
				"③ ¿Cuáles condiciones deben cumplir las resistencias para que el voltaje de salida tienda al mismo voltaje de la fuente?\n" +
				"④ ¿Cuál es el circuito equivalente de Thévenin? ¿Como se relaciona la resistencia equivalente de Thévenin con respecto al periodo transitorio del circuito? ¿Cómo se relaciona el voltaje equivalente de Thévenin con respecto al valor en estado estacionario de la salida?\n" +
				"⑤ ¿Con la información conocida es posible determinar la forma de las corrientes en los componentes del circuito?\n" +
				"⑥ Note que, a diferencia de los circuitos anteriores, al variar las resistencias en estado estacionario sí se generan cambios en la salida, ¿cómo se puede explicar esto?";
				break;
            case 6:
				textD = "① Teniendo en cuenta solo las posibilidades de variación del circuito, ¿es posible que el voltaje de salida tome voltajes negativos?\n" +
				"② Si el valor de la resistencia tendiera a cero, ¿Cómo sería el comportamiento del voltaje en el capacitor?, ¿cómo sería el sobrepico?\n" +
				"③ Si el valor de la resistencia tendiera a infinito, ¿Cómo sería el comportamiento del voltaje en el capacitor?\n" +
				"④ ¿Con la información presente es posible determinar la corriente en el circuito?\n" +
				"⑤ Se dice que si la fuente de alimentación es constante en estado estacionario los inductores se comportan como corto circuito mientras que los capacitores se comportan como circuitos abiertos ¿la respuesta de este circuito permite verificar esa afirmación?";
				break;
            case 7:
				textD = " ";
				break;
            case 8:
				textD = " ";
				break;
            case 9:
				textD = " ";
				break;
            case 10:
				textD = " ";
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

        OrientText.text = TextOrient(index);
        OriScroll.value = 1;

        DiscusText.text = TextDiscu(index);
        DisScroll.value = 1;

        axes.GetComponent<AxesBehaviour>().ResetLineAxes();

    }
}
