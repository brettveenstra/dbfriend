using System.Collections.Generic;
using System.Diagnostics;
using DbFriend.Core.Provider;
using QuickGraph;
using QuickGraph.Algorithms;

namespace DbFriend.Core.Graph
{
    public class DependencyGraph : IDependencyGraph
    {
        private readonly AdjacencyGraph<IDbObject, IEdge<IDbObject>> graph = new AdjacencyGraph<IDbObject, IEdge<IDbObject>>();

        public IEnumerable<IDbObject> Dependencies
        {
            get
            {
                foreach (IDbObject dbObject in graph.TopologicalSort())
                {
                    yield return dbObject;
                }
            }
        }

        public IEnumerable<IDbObject> DbObjects
        {
            get
            {
                List<IDbObject> dbObjects = new List<IDbObject>(graph.Vertices);
                foreach (IDbObject dbObject in dbObjects)
                {
                    yield return dbObject;
                }
            }
        }

        public void AddDependency(IDbObject child, IDbObject parent)
        {
            if (child.Id == parent.Id)
            {
                return;
            }
            if (graph.ContainsVertex(child) == false)
            {
                AddDbObject(child);
            }

            if (graph.ContainsVertex(parent) == false)
            {
                AddDbObject(parent);
            }


            DbDependencyEdge edge = new DbDependencyEdge(parent, child);
            if (graph.ContainsEdge(edge))
            {
                return;
            }
            Debug.WriteLine(string.Format("Edge {1} -> {0}", parent.Name, child.Name));
            graph.AddEdge(edge);
        }

        public void AddDbObject(IDbObject dbObject)
        {
            graph.AddVertex(dbObject);
        }

    }
}