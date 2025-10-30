namespace Nt.Syntax.Structures
{
    public class ImportPath
    {
        /// <summary>
        /// Instantiates an empty import path
        /// </summary>
        public ImportPath() { }
        /// <summary>
        /// Instantiates an import path with the given list of paths
        /// </summary>
        /// <param name="paths">List of paths used to initialise import path</param>
        public ImportPath(List<string> paths) { Path = paths; }

        public List<string> Path { get; } = [];

        internal void Reset() { Path.Clear(); }

        public override string ToString() => string.Join(";", Path);
    }
}
