using System;
using System.Collections.Generic;
using System.Globalization;
using Drexel.Collections.Generic.Directed.Weighted;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drexel.Collections.Graphs.Tests
{
    [TestClass]
    public class Sandbox
    {
        private static TestContext TestContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Sandbox.TestContext = context;
        }

        [TestMethod]
        public void TestMethod1()
        {
            Pseudograph<string, int, Edge<string, int>> graph =
                new Pseudograph<string, int, Edge<string, int>>(
                    EqualityComparer<string>.Default,
                    EqualityComparer<Edge<string, int>>.Default);

            Assert.IsTrue(graph.Add("foo"));
            Assert.IsTrue(graph.Add("bar"));
            Assert.IsTrue(graph.Add(vertex: null));
            Assert.IsTrue(graph.Add("baz"));

            Assert.IsFalse(graph.Add("foo"));
            Assert.IsFalse(graph.Add("baz"));

            graph.Add(new Edge<string, int>("foo", "bar", 10));
            graph.Add(new Edge<string, int>("foo", "bar", 12));

            graph.Add(new Edge<string, int>(null, "baz", 4));
            graph.Add(new Edge<string, int>(null, null, 1));

            foreach (string vertex in graph.Vertices)
            {
                Sandbox.TestContext.WriteLine(vertex ?? "<null>");
            }

            Sandbox.TestContext.WriteLine("=======");

            foreach (Edge<string, int> edge in graph.Edges)
            {
                Sandbox.TestContext.WriteLine(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "{0} to {1} costs {2}",
                        edge.Head ?? "<null>",
                        edge.Tail ?? "<null>",
                        edge.Weight));
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            NullDictionary<string?, int> foo = new NullDictionary<string?, int>(EqualityComparer<string?>.Default);
            foo.Add(null, 1);
            Assert.ThrowsException<ArgumentException>(() => foo.Add(null, 2));
        }
    }
}
