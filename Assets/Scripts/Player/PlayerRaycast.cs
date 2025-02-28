using UnityEngine;

namespace OscilloscopeSimulation.Player
{
    [System.Serializable]
    internal sealed class PlayerRaycast
    {
        private static Vector3 lastRaycastPointPosition;
        private static RaycastHit lastRaycasHit;

        [SerializeField] private Camera techCamera;

        internal void Update()
        {
            ThrowRayFromMouseAndTryToInteract();
        }

        private void ThrowRayFromMouseAndTryToInteract()
        {
            Ray ray = techCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit,
                float.PositiveInfinity, ~0, QueryTriggerInteraction.Ignore))
            {
                return;
            }
            if (hit.transform.TryGetComponent(out Interactable hitInteractable))
            {
                //���� ������ ����� ������ ����
                if (Input.GetMouseButtonDown(0))
                {
                    hitInteractable.Interact();
                }
            }
            lastRaycastPointPosition = hit.point;
            lastRaycasHit = hit;
        }

        internal static Vector3 GetLastHitPosition()
        {
            return lastRaycastPointPosition;
        }

        internal static RaycastHit GetLastRaycastHit()
        {
            return lastRaycasHit;
        }
    }
}