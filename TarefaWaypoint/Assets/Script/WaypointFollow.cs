using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    public GameObject[] waypoints; //Pontos em que o objeto irá se dirigir 
    public int currentWP= 0; //Ponto atual

    float speed= 1.0f; //Velocidade do objeto
    float accuracy= 1.0f; //Usado para calcular a distancia até o ponto e para fazer a rotação
    float rotSpeed= 0.4f; //Velocidade que o objeto rotaciona

    private void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");//procura automatica feita por tag caso não haja objects no public GameObject[] waypoints
    }

    private void LateUpdate()
    {
        if (waypoints.Length == 0) return; //Reiniciar ao passar por todos waypoints

        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z);
        //Faz o calculo para olhar em direção ao objeto

        Vector3 direction = lookAtGoal - this.transform.position; this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        //Usa o lookAtGoal para calcular o caminho que vai seguir

        if (direction.magnitude < accuracy)
        {
            currentWP++;//Passar para o proximo waypoint

            if (currentWP >= waypoints.Length)
            {
                currentWP = 0; //Zera o ponto ao terminar de passar por todos waypoints

            }
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);//Faz a movimentação, utiliza o var speed como velocidade e normaliza o tempo com o timedeltatime
    }
}
