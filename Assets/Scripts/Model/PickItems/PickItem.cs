using UnityEngine;


namespace Assets.Scripts.Model.PickItems
{
    public abstract class PickItem : BaseObjectScene
    {
        protected SpaceshipMove spaceshipMove;
        protected SpaceshipFire spaceshipFire;
        protected SpaceshipHealth spaceshipHealth;
        protected SpaceshipShield spaceshipShield;
    }
}