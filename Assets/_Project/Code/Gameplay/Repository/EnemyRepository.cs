using System.Collections.Generic;
using _Project.Code.Gameplay.Enemy;
using _Project.Code.Utils;

namespace _Project.Code.Gameplay.Repository
{
    public class EnemyRepository
    {
        private readonly List<EnemyFacade> _list;

        public EnemyRepository()
        {
            _list = new List<EnemyFacade>(Constants.DefaultCapacity);
        }

        public IEnumerable<EnemyFacade> List => _list;

        public void Add(EnemyFacade enemy)
        {
            if (!_list.Contains(enemy))
                _list.Add(enemy);
        }

        public void Remove(EnemyFacade enemy)
        {
            if (!_list.Contains(enemy))
                _list.Remove(enemy);
        }
    }
}