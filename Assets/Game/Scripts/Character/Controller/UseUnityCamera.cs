using UnityEngine;

namespace Game.Character
{
    public class UseUnityCamera : MonoBehaviour
    {
        [SerializeField] private bool useMainCamera = true;
        [SerializeField] private Camera assignedCamera;
        [SerializeField] private Transform cameraSocket;

        private const string DEFAULT_CAMERA_SOCKET_NAME = "camera_socket";

        private void Awake()
        {
            if (!cameraSocket)
                throw new MissingComponentException("Camera Socket is not assigned");
            
            if (useMainCamera)
            {
                var c = Camera.main;
                if (c)
                {
                    assignedCamera = c;
                    AttachCameraToSocket();
                }
                else Debug.LogError("Can not use Main Camera - not found", this);
            }
            else if (assignedCamera)
            {
                AttachCameraToSocket();
            }
            else
            {
                Debug.LogError("Please assign a camera", this);
            }
        }

        private bool TryFindCameraSocket(out GameObject socket) => gameObject.TryGetGameObjectInChildrenWithName(DEFAULT_CAMERA_SOCKET_NAME, out socket);

        private void AttachCameraToSocket()
        {
            var t = assignedCamera.transform;
            t.parent = cameraSocket.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!cameraSocket && TryFindCameraSocket(out var socket)) cameraSocket = socket.transform;
        }
#endif
    }
}