/*
* Author: Iris Bermudez
* GitHub: https://github.com/AlgoritmoAlgoritmo
* Date: 05/02/2025 (DD/MM/YYYY)
*/


using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;



namespace JMonkeyNetcodeTutorial {
    public class NetworkUIController : MonoBehaviour {
        #region Variables
        [SerializeField]
        private Button serverButton;
        [SerializeField]
        private Button hostButton;
        [SerializeField]
        private Button clientButton;
        #endregion


        #region MonoBehaviour methods
        private void Awake() {
            serverButton.onClick.AddListener( () => {
                NetworkManager.Singleton.StartServer();
            } );

            clientButton.onClick.AddListener( () => {
                NetworkManager.Singleton.StartClient();
            } );

            hostButton.onClick.AddListener( () => {
                NetworkManager.Singleton.StartHost();
            } );
        }
        #endregion
    }
}