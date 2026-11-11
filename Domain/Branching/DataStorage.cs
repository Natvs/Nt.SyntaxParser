namespace Nt.Syntax.Branching
{
    internal class DataStorage()
    {
        private static DataStorage? _instance;
        private Dictionary<int, IData> ObjData { get; } = [];
        
        public static DataStorage GetInstance()
        {
            if (_instance != null) return _instance;
            else
            {
                _instance = new DataStorage();
                return _instance;
            }
        }

        public void AddData(object obj, IData data)
        {
            var hash = obj.GetHashCode();
            ObjData.Add(hash, data);
        }

        public IData? GetData(object obj)
        {
            var hash = obj.GetHashCode();
            if (ObjData.TryGetValue(hash, out IData? value)) return value;
            else return null;
        }
    }
}
