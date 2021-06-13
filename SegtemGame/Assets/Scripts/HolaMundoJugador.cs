using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

namespace HolaMundo{
    public class HolaMundoJugador : NetworkBehaviour
    {
       public NetworkVariableVector3 Posicion = new NetworkVariableVector3(new NetworkVariableSettings
       {
           WritePermission = NetworkVariablePermission.ServerOnly,
           ReadPermission = NetworkVariablePermission.Everyone
       });

       public override void NetworkStart(){
           Mover();
       }
       public void Mover(){
           if(NetworkManager.Singleton.IsServer){
               var posicionAleatorea = GetRandomPositionOnPlane();
               transform.position = posicionAleatorea;
               Posicion.Value = posicionAleatorea;
           }else{
               SubmitPositionRequestServerRpc();
           }
       }
        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Posicion.Value = GetRandomPositionOnPlane();
        }
        static Vector3 GetRandomPositionOnPlane(){
            return new Vector3(Random.Range(-3f,3f),1f,Random.Range(-3f,3f));
        }
        void Update() {
            transform.position = Posicion.Value;
        }
    }
}

