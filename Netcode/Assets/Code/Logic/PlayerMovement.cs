/*
* Author: Iris Bermudez
* GitHub: https://github.com/AlgoritmoAlgoritmo
* Date: 06/02/2025 (DD/MM/YYYY)
*/


using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;


namespace JMonkeyNetcodeTutorial {
    public class PlayerMovement : NetworkBehaviour {
        #region Variables
        [SerializeField]
        private float speed = .1f;

        private NetworkVariable<int> randomNumber = new NetworkVariable<int>(0, 
                                                            NetworkVariableReadPermission.Everyone,
                                                            NetworkVariableWritePermission.Owner);
        private NetworkVariable<MyCustomData> customData = new NetworkVariable<MyCustomData>(
                                                        new MyCustomData { _int = 0, _bool = false },
                                                        NetworkVariableReadPermission.Everyone,
                                                        NetworkVariableWritePermission.Owner );

        private Vector3 auxPosition;

        public struct MyCustomData : INetworkSerializable {
            public int _int;
            public bool _bool;
            public FixedString128Bytes _message;

            public void NetworkSerialize<T>( BufferSerializer<T> serializer ) where T : IReaderWriter {
                serializer.SerializeValue( ref _int );
                serializer.SerializeValue( ref _bool );
                serializer.SerializeValue( ref _message );
            }
        }
        #endregion

        #region MonoBehaviour methods
        private void Update() {
            
            if( !IsOwner ) {
                return;
            }


            if( Input.GetKeyUp( KeyCode.T ) ) {
                // randomNumber.Value = Random.Range( 0, 100 );

                /*
                customData.Value = new MyCustomData {
                    _int = customData.Value._int + 1,
                    _bool = !customData.Value._bool,
                    _message = "This is a message"
                };
                */

                // TestServerRpc( new ServerRpcParams() );
                TestClientRpc( new ClientRpcParams {
                    Send = new ClientRpcSendParams {
                        TargetClientIds = new List<ulong> { 1 }
                    }
                } );
            }


            auxPosition = Vector3.zero;

            if( Input.GetKey( KeyCode.W ) ) {
                auxPosition.z -= speed;

            } else if( Input.GetKey( KeyCode.S ) ) {
                auxPosition.z += speed;
            }

            if( Input.GetKey( KeyCode.A ) ) {
                auxPosition.x += speed;

            } else if( Input.GetKey( KeyCode.D ) ) {
                auxPosition.x -= speed;
            }

            transform.position += auxPosition;
        }
        #endregion

        public override void OnNetworkSpawn() {
            customData.OnValueChanged += ( MyCustomData previousValue, MyCustomData newValue ) => {
                    Debug.Log( "------------------------------------------" );
                    Debug.Log( OwnerClientId + " : " + customData.Value );
                    Debug.Log( customData.Value._int + " : " + customData.Value._bool );
                    Debug.Log( customData.Value._message );
                    Debug.Log( "------------------------------------------" );
                };


            /*
            randomNumber.OnValueChanged += ( int previousValue, int newValue ) => {
                Debug.Log( "------------------------------------------" );
                Debug.Log( OwnerClientId + " : " + randomNumber.Value );
                Debug.Log( customData.Value._int + " : " + customData.Value._bool );
                Debug.Log( "------------------------------------------" );
            };*/
        }


        #region *** RPC ***

        [ServerRpc] // Sends message from client to server. The function always has to end with "ServerRpc".
        private void TestServerRpc( ServerRpcParams _serverRpcParams ) {
            Debug.Log("TestServerRpc " + OwnerClientId + "," + _serverRpcParams.Receive.SenderClientId );
        }

        [ClientRpc] // Sends message from server to client. The function always has to end with "ClientRpc".
        private void TestClientRpc( ClientRpcParams _clientRpcParams ) {
            Debug.Log( "TestServerRpc " + OwnerClientId + "," + _clientRpcParams.Send.TargetClientIds );
        }
        #endregion
    }
}