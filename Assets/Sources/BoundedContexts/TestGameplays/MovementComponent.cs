using UnityEngine;

namespace Sources.BoundedContexts.TestGameplays
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        public void Move(Vector3 direction, float deltaTime)
        {
            transform.position += direction * (_speed * deltaTime);
        }
    }
}