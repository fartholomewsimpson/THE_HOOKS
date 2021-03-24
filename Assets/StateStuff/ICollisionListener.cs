using UnityEngine;

namespace StateStuff
{
    public interface ICollisionListener
    {
        void HandleCollision(Collision2D collision);
    }
}
