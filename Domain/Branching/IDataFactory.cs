using Nt.Syntax.Structures;

namespace Nt.Syntax.Branching
{
    public interface IDataFactory<T> where T : IData
    {
        T Create();
    }

}
