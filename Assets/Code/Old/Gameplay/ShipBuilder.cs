using System;
using Helpers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay
{
    public class ShipBuilder
    {
        // Transform values
        private Vector3 _position = Vector3.zero;
        private Quaternion _rotation = Quaternion.identity;
        
        // Projectiles factory info
        private ProjectileFactory _projectileFactory;
        private ProjectileEnumId _projectileEnumId;

        // Ship movement
        private IInputReceiver _inputReceiver;
        private IMovementConstrainer _movementConstrainer;
        private float _speed;
        private Vector2 _direction;
        
        // Ship construction
        private Ship _prototypeShip;
        private ShipModelFactory _shipModelFactory;
        private ShipModelEnumId _shipModelEnumId;

        // I should really use the latter constructor
        public ShipBuilder() { }

        public ShipBuilder(ProjectileFactory projectileFactory, ShipModelFactory shipModelFactory)
        {
            _projectileFactory = projectileFactory;
            _shipModelFactory = shipModelFactory;
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

        // public ShipBuilder WithModel(ShipModelEnumId id)
        // {
        //     throw new NotImplementedException("TODO: use different models");
        //     // return this;
        // }

        // Todo: what was I supposed to do with this one?
        // I'd rather build the entire ship not from a prefab or have a prefab skeleton?
        public ShipBuilder WithPrototypePrefab(Ship ship)
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

        public ShipBuilder WithShipModelFactory(ShipModelFactory factory)
        {
            _shipModelFactory = factory;
            return this;
        }
        
        public ShipBuilder WithShipModelEnumId(ShipModelEnumId id)
        {
            _shipModelEnumId = id;
            return this;
        }

        // TODO: these cannot be passed so easily, I'd need to map id's to interfaces with a switch or something during building
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
            if (_prototypeShip)
            {
                return BuildUsingPrototype();
            }
            else
            {
                return BuildFromScratch();
            }
        }

        private Ship BuildUsingPrototype()
        {
            Ship generatedShip = Object.Instantiate(_prototypeShip, _position, _rotation);
            ShipFiringMediator shipFiringMediator = generatedShip.GetComponent<ShipFiringMediator>();
            shipFiringMediator.ProjectileEnumId = _projectileEnumId;
            
            ConfigureShip(generatedShip, shipFiringMediator);

            Transform shipTransform = generatedShip.transform;

            if (shipTransform.childCount == 1)
            {
                Object.Destroy(shipTransform.GetChild(0).gameObject);
            }

            GameObject shipModel = _shipModelFactory.Create(_shipModelEnumId, shipTransform);
            
            return generatedShip;
        }

        private Ship BuildFromScratch()
        {
            GameObject generatedShipObject = GameObject.CreatePrimitive(PrimitiveType.Cube); // new GameObject();
            Ship generatedShip = generatedShipObject.AddComponent<Ship>();
            ShipFiringMediator shipFiringMediator = generatedShipObject.AddComponent<ShipFiringMediator>();
            
            ConfigureShip(generatedShip, shipFiringMediator);

            return generatedShip;
        }

        private void ConfigureShip(Ship generatedShip, ShipFiringMediator shipFiringMediator)
        {
            generatedShip.speed = _speed;
            generatedShip.Configure(new FreeMovementStrategy());
            generatedShip.Configure(new UnityInputAdapter());
            // generatedShip.Configure(new AIInputAdapter(generatedShip));

            shipFiringMediator.ProjectileEnumId = _projectileEnumId;
            shipFiringMediator.Configure(_projectileFactory);
            shipFiringMediator.Configure(generatedShip);
        }
    }
}

#region old stuff

// TODO: implement all methods & use
// It'll be a monobehaviour since I'd like to serialize them as prefabs to later have
// a factory of ShipBuilders which I'd use directly to get the preconfigured builder (?)

// [CreateAssetMenu(fileName = "ShipBuilder", menuName = "ScriptableObjects/ShipBuilder", order = 1)]

// public Ship SharedPrototypeShip
// {
//     get => _prototypeShip;
//     set => _prototypeShip = value;
// }

// private void Awake()
// {
//     this.WithPosition(Vector3.zero)
//         .WithRotation(Quaternion.identity);
// }

// public ShipBuilder(ProjectileFactory projectileFactory)
// {
//     this.WithPosition(Vector3.zero)
//         .WithRotation(Quaternion.identity)
//         .WithProjectileFactory(projectileFactory);
//
//     // It's a not-so-good idea to load from a resource that can change at anytime
//     // if (!SharedPrototypeShip)
//     // {
//     //     SharedPrototypeShip = Resources.Load<Ship>("Spaceship_template");
//     // }
// }
#endregion