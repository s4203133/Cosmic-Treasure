using LMO.CustomEvents;

namespace LMO.Interfaces {
    public interface ICustomEvent {

        public void Initialise(EventManager manager);

        public void SubscribeEvents();

        public void UnsubscribeEvents();
    }
}