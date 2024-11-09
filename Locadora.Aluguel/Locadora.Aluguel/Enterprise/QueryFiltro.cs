using System.Linq.Expressions;

namespace Locadora.Aluguel.Enterprise
{
    public class QueryFiltro<T> where T : class
    {
        public int? Skip { get; set; }

        public int? Take { get; set; }

        public Expression<Func<T, object>>? OrderBy { get; set; }

        public bool OrderByDescending { get; set; } = false;
    }
}