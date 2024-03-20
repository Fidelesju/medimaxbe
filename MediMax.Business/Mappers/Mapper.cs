using MediMax.Business.Mappers.Interface;

namespace MediMax.Business.Mappers
{
    public class Mapper<T> : IMapper<T>
    {
        protected T? BaseMapping;

        public void SetBaseMapping(T baseMapping)
        {
            BaseMapping = baseMapping;
        }
    }
}