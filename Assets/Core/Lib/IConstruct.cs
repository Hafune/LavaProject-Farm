using Reflex.Scripts.Attributes;

namespace Lib
{
    public interface IConstruct
    {
        [Inject]
        public void Construct(IMyContext context);
    }
}