using _Project.Code.Core.StateMachine;
using VContainer;

namespace _Project.Code.Core.Factory
{
    public class StateFactory
    {
        [Inject] private readonly IObjectResolver _objectResolver;

        public T Create<T>(Lifetime lifetime) where T : IState
        {
            var registrationBuilder = new RegistrationBuilder(typeof(IState), lifetime);
            return (T)_objectResolver.Resolve(registrationBuilder.Build());
        }
    }
}