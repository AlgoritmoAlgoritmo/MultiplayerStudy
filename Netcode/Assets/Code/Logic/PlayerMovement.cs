/*
* Author: Iris Bermudez
* GitHub: https://github.com/AlgoritmoAlgoritmo
* Date: 06/02/2025 (DD/MM/YYYY)
*/


using Unity.Netcode;
using UnityEngine;


namespace JMonkeyNetcodeTutorial {
    public class PlayerMovement : NetworkBehaviour {
        #region Variables
        [SerializeField]
        private float speed = .1f;

        private Vector3 auxPosition;
        #endregion

        #region MonoBehaviour methods
        private void Update() {
            if( !IsOwner ) {
                return;
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
    }
}