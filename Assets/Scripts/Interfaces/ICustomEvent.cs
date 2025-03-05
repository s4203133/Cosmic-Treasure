
namespace LMO {
    public interface ICustomEvent {

        public void Initialise(EventManager manager);

        public void SubscribeEvents();

        public void UnsubscribeEvents();
    }
}