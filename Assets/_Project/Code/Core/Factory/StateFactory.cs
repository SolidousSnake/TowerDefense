using _Project.Code.Core.Fsm;
using VContainer;

namespace _Project.Code.Core.Factory
{
    public class StateFactory
    {
        [Inject] private readonly IObjectResolver _objectResolver;
        
        public T Create<T>(Lifetime lifetime = Lifetime.Scoped) where T : IState
        {
            var registrationBuilder = new RegistrationBuilder(typeof(T), lifetime);
            return (T)_objectResolver.Resolve(registrationBuilder.Build());
        }
    }
}