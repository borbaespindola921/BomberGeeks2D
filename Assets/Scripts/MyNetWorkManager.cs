using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
public class MyNetWorkManager : NetworkManager
{
    //Variáveis
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    public List<Transform> coinSpawnPoints;
    public int maxCoinsInGame = 2;
    public static int spawnedCoins = 0;

    //Métodos
    public override void OnStartServer()
    {
        Debug.Log("Iniciando Servidor...");
    }

    public override void OnStopServer()
    {
        Debug.Log("Encerrando Servidor...");
    }

    public override void OnClientConnect()
    {
        Debug.Log("Novo jogador coneectado!");
    }

    public override void OnClientDisconnect()
    {
        Debug.Log("Um jogador saiu da partida...");
    }

    public UnityEvent OnPlayerConnect;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        OnPlayerConnect.Invoke();
        Transform startPoint;
        Color color;

        if(numPlayers == 0)
        {
            startPoint = spawnPoint1;
            color = Color.green;
            InvokeRepeating("SpawnCoin", 2, 2);
        }
        else
        {
            startPoint = spawnPoint2;
            //InvokeRepeating("SpawnCoin", 2, 2);
            color = Color.red;
        }

        GameObject new_player = Instantiate(playerPrefab, startPoint.position, startPoint.rotation);
        new_player.GetComponent<Player>().playerColor = color;
        NetworkServer.AddPlayerForConnection(conn, new_player);
    }
   
    public void SpawnCoin()
    {
        if(spawnedCoins < maxCoinsInGame)
        {
            Vector3 local = coinSpawnPoints[Random.Range(0, coinSpawnPoints.Count)].position;
            
            GameObject new_coin = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Coin"), local, transform.rotation);
                    
            NetworkServer.Spawn(new_coin);
            spawnedCoins++;
        }
    }

}