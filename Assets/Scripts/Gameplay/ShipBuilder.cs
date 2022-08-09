using System;
using UnityEngine;

namespace Gameplay
{
    // TODO: implement all methods & use
    // It'll be a monobehaviour since I'd like to serialize them as prefabs to later have
    // a factory of ShipBuilders which I'd use directly to get the preconfigured builder (?)

    // [CreateAssetMenu(fileName = "ShipBuilder", menuName = "ScriptableObjects/ShipBuilder", order = 1)]
    public class ShipBuilder // : MonoBehaviour
    {
        private Vector3 _position;
        private Quaternion _rotation;
        private ProjectileFactory _projectileFactory;
        private ProjectileEnumId _projectileEnumId;
        private IInputReceiver _inputReceiver;
        private IMovementConstrainer _movementConstrainer;
        private float _speed;
        private Vector2 _direction;
        private Ship _prototypeShip;

        // private void Awake()
        // {
        //     this.WithPosition(Vector3.zero)
        //         .WithRotation(Quaternion.identity);
        // }
        
        public ShipBuilder(ProjectileFactory projectileFactory)
        {
            this.WithPosition(Vector3.zero)
                .WithRotation(Quaternion.identity)
                .WithProjectileFactory(projectileFactory);
        }
        
        public ShipBuilder WithPosition(Vector3 position)
        {
            _position = position;
            return this;
        }
        
        public ShipBuilder WithRotation(Quaternion rotation)
        {
            _rotation = rotation;
            return this;
        }

        public ShipBuilder WithSpeed(float speed)
        {
            _speed = speed;
            return this;
        }

        public ShipBuilder WithModel(int id)
        {
            throw new NotImplementedException("TODO: use different models");
            // return this;
        }

        // Todo: what was I supposed to do with this one?
        // I'd rather build the entire ship not from a prefab or have a prefab skeleton?
        public ShipBuilder FromPrefab(Ship ship)
        {
            _prototypeShip = ship;
            return this;
        }

        public ShipBuilder WithProjectileType(ProjectileEnumId projectileEnumId)
        {
            _projectileEnumId = projectileEnumId;
            return this;
        }

        public ShipBuilder WithProjectileFactory(ProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;            
            return this;
        }

        public ShipBuilder WithInputReceiver(IInputReceiver inputReceiver)
        {
            _inputReceiver = inputReceiver;
            return this;
        }

        public ShipBuilder WithMovementConstrainer(IMovementConstrainer movementConstrainer)
        {
            _movementConstrainer = movementConstrainer;
            return this;
        }

        public Ship Build()
        {
            // TODO: it works but pls clean this mess
            GameObject generatedShipObject = GameObject.CreatePrimitive(PrimitiveType.Capsule); // new GameObject();
            
            Ship generatedShip = generatedShipObject.AddComponent<Ship>();
            generatedShip.speed = _speed;
            generatedShip.Configure(new FreeMovementStrategy());
            generatedShip.Configure(new UnityInputAdapter());
            // generatedShip.Configure(new AIInputAdapter(generatedShip));
            
            ShipFiringMediator shipFiringMediator = generatedShipObject.AddComponent<ShipFiringMediator>();
            shipFiringMediator.ProjectileEnumId = _projectileEnumId;
            shipFiringMediator.Configure(_projectileFactory);
            shipFiringMediator.Configure(generatedShip);
            
            // shipFiringMediator.Configure(proje);
            // shipFiringMediator.FiringCooldownSeconds

            return generatedShip;
        }
    }
}