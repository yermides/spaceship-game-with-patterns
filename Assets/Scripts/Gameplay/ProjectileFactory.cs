using Object = UnityEngine.Object;

namespace Gameplay
{
    public class ProjectileFactory
    {
        private readonly ProjectileFactoryConfiguration _projectileFactoryConfiguration;
        
        public ProjectileFactory(ProjectileFactoryConfiguration projectileFactoryConfiguration)
        {
            _projectileFactoryConfiguration = Object.Instantiate(projectileFactoryConfiguration);
        }

        public Projectile Create(ProjectileEnumId id)
        {
            Projectile projectile = _projectileFactoryConfiguration.GetPrefabById(id);
            return Object.Instantiate(projectile);
        }
    }
}