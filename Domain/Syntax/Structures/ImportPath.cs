namespace Nt.Syntax.Structures
{
    /// <summary>
    /// Represents a collection of import path for further file importations.
    /// </summary>
    public class ImportPath
    {
        private List<string> Path { get; } = [];

        /// <summary>
        /// Returns a copy of the current path as a list of node names.
        /// </summary>
        /// <returns>A list of strings representing the sequence of node names in the current path.</returns>
        internal List<string> GetPath()
        {
            return [.. Path];
        }
        /// <summary>
        /// Adds the specified path to the collection of paths.
        /// </summary>
        /// <param name="path">The path to add to the collection.</param>
        internal void AddPath(string path) { Path.Add(path); }
        /// <summary>
        /// Removes all elements from the current path, resetting it to an empty state.
        /// </summary>
        internal void ClearPath() { Path.Clear(); }


        /// <summary>
        /// Get a string representation of all the path in this instance
        /// </summary>
        /// <returns>String with paths separted by ';'</returns>
        public override string ToString() => string.Join(";", Path);
    }
}
