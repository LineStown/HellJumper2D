using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SCSIA
{
    public abstract class BaseRendererEffectAction : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        protected SpriteRenderer _spriteRenderer;
        protected bool _active;

        //############################################################################################
        // PUBLIC  METHODS
        //############################################################################################
        public void StartExecute(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
            _active = true;
            StartInternalExecute();
        }

        public void StopExecute()
        {
            _active = false;
            StopInternalExecute();
        }

        //############################################################################################
        // PROTECTED  METHODS
        //############################################################################################
        protected abstract void StartInternalExecute();
        protected abstract void StopInternalExecute();
    }
}
