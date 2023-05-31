using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject col;
    public float velocidad = 2;
    public List<GameObject> cols;
    public List<GameObject> obstaculos;
    public Renderer fondo;
    public GameObject nube1;
    public GameObject nube2;
    public bool gameOver = false;
    public bool start = false;
    public bool finish = false;
    public GameObject menuPrincipal;
    public GameObject menuGameOver;
    public GameObject menuGamesucess;

    public int puntos;


    void Start()
    {
        //Crear suelo 
        for (int i = 0; i < 21; i++)
        {
            cols.Add(Instantiate(col, new Vector2(-10 + i, -3), Quaternion.identity));
        }
        //crear nubes 
        obstaculos.Add(Instantiate(nube1, new Vector2(10, 2), Quaternion.identity));
        obstaculos.Add(Instantiate(nube2, new Vector2(14, -2), Quaternion.identity));
    }

    // Update is called once per frameen default y lo añadimos a nuestro cube 
    void Update()
    {

        if(start == false)
        {
            //menuPrincipal.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                start = true;   
            }

        }

        if (start == true && gameOver == true && finish == false)
        {
            menuGameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }

        if (start == true && gameOver == false && finish == true)
        {
            menuGamesucess.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }

        if (start == true && gameOver == false && finish == false)
        {
            menuPrincipal.SetActive(false);
            fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.03f, 0) * Time.deltaTime;
            //mover suelo
            for (int i = 0; i < cols.Count; i++)
            {
                if (cols[i].transform.position.x <= -10)
                {
                    cols[i].transform.position = new Vector3(10, -3, 0);
                }
                cols[i].transform.position = cols[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }

            //mover obstaculos 

            for (int i = 0; i < obstaculos.Count; i++)
            {
                if (obstaculos[i].transform.position.x <= -10)
                {
                    float randomObs = Random.Range(10, 18);
                    float randomObsy = Random.Range(-2, 4);
                    obstaculos[i].transform.position = new Vector3(randomObs, randomObsy, 0);
                }
                obstaculos[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }
            //Contador de puntos 

            for (int i = 0; i < obstaculos.Count; i++)
            {

                if (obstaculos[i].transform.position.x <= -10)
                {
    
                    IncrementarPuntos(100);
                }

            }
            void IncrementarPuntos(int cantidad)
            {
                puntos += cantidad;
                Debug.Log("Puntos: " + puntos);

                // Actualizar el marcador de puntos aquí

                if (puntos >= 1000)
                {
                    finish = true;
                    GanarJuego();

                }
            }

            void GanarJuego()
            {
                Debug.Log("¡Has ganado el juego!");
                
            }
        }
    }
}
