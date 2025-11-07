using System;
using UnityEngine;

namespace ExtensionsForReflexDI.Sample.MonoBehaviourBinding.Prefabs
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class SpawnableItem : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;

        private Vector3 _center;
        private readonly float _sineSpeedFactor = 4f;
        private readonly float _speed = 40;
        private float _time;

        private void Update()
        {
            _time += Time.deltaTime;
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            var degrees = _time * _speed;
            if (degrees / 360 > 1)
            {
                var rest = degrees % 360;
                _time = rest / _speed;
            }
            
            Quaternion rotation = Quaternion.AngleAxis(degrees, Vector3.up);
            transform.position = _center + rotation * (Vector3.forward + Vector3.forward * Mathf.Lerp(.5f, 1,
                Mathf.InverseLerp(-1, 1, Convert.ToSingle(Math.Cos(Mathf.Deg2Rad * degrees * _sineSpeedFactor)))));
        }

        public void SetColor(Color color)
        {
            _meshRenderer.material.color = color;
        }

        public void SetPosition(Vector3 position)
        {
            _center = position;
        }

        private void OnValidate()
        {
            if (_meshRenderer == null)
            {
                TryGetComponent<MeshRenderer>(out var meshRenderer);
                _meshRenderer = meshRenderer;
            }
        }
    }
}