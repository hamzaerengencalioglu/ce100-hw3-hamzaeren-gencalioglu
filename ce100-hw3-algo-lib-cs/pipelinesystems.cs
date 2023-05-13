using System;
using System.Collections.Generic;
using System.Dynamic;

public class pipelinesystems
{
    private int treeCount; // Declaring a private integer variable named treeCount
    private List<Tree> trees; // Declaring a private List of Tree objects named trees
    private List<Edge> pipeline; // Declaring a private List of Edge objects named pipeline

    /// @brief Initializes a new instance of the pipelinesystems class with the specified tree count.
    /// @param treeCount The number of trees in the pipeline system.
    public pipelinesystems(int treeCount)
    {
        if (treeCount < 10)
        {
            throw new ArgumentException("The minimum number of trees must be 10 or more.");
        }

        this.treeCount = treeCount;
        trees = GenerateRandomTreePositions(treeCount);
        pipeline = new List<Edge>();
    }

    /// @brief Generates random positions for the trees in the pipeline system.
    /// @param count The number of trees to generate positions for.
    /// @returns A list of Tree objects with random positions.
    private List<Tree> GenerateRandomTreePositions(int count)
    {
        Random random = new Random();
        List<Tree> treeList = new List<Tree>();

        for (int i = 0; i < count; i++)
        {
            int x = random.Next(1, 100);
            int y = random.Next(1, 100);
            treeList.Add(new Tree(x, y));
        }

        return treeList;
    }

    /// @brief Connects the trees in the pipeline system using the Kruskal algorithm.
    public void ConnectTrees()
    {
        List<Edge> allEdges = new List<Edge>();

        for (int i = 0; i < treeCount - 1; i++)
        {
            Tree tree1 = trees[i];
            Tree tree2 = trees[i + 1];
            double distance = CalculateDistance(tree1, tree2);
            allEdges.Add(new Edge(i, i + 1, distance));
        }

        allEdges.Sort((x, y) => x.Distance.CompareTo(y.Distance));

        UnionFind uf = new UnionFind(treeCount);
        foreach (Edge edge in allEdges)
        {
            if (uf.Find(edge.From) != uf.Find(edge.To))
            {
                uf.Union(edge.From, edge.To);
                pipeline.Add(edge);
            }
        }
    }

    /// @brief Gets the description of the pipeline system.
    /// @returns A list of strings describing the pipeline connections between trees.
    public List<string> GetPipelineDescription()
    {
        List<string> descriptions = new List<string>();
        foreach (Edge edge in pipeline)
        {
            string description = $"Tree {edge.From} is connected to Tree {edge.To} with a pipeline of length {edge.Distance}.";
            descriptions.Add(description);
        }

        return descriptions;
    }

    /// @brief Calculates the distance between two trees.
    /// @param tree1 The first tree.
    /// @param tree2 The second tree.
    /// @returns The distance between the two trees.
    private double CalculateDistance(Tree tree1, Tree tree2)
    {
        int xDiff = tree1.X - tree2.X;
        int yDiff = tree1.Y - tree2.Y;
        return Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
    }

    /// @brief Represents a tree in the pipeline system.
    private class Tree
    {
        public int X { get; }
        public int Y { get; }

        public Tree(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    /// @brief Represents an edge (pipeline) between two trees in the pipeline system.
    private class Edge
    {
        public int From { get; }
        public int To { get; }
        public double Distance { get; }

        public Edge(int from, int to, double distance)
        {
            From = from;
            To = to;
            Distance = distance;
        }
    }

    /// @brief Represents a disjoint-set data structure used in the Kruskal algorithm.
    private class UnionFind
    {
        private int[] parent;
        private int[] rank;

        public UnionFind(int size)
        {
            parent = new int[size];
            rank = new int[size];

            for (int i = 0; i < size; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }
        // Defining a public method named Find that takes an integer argument named x and returns an integer
        public int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]);
            }

            return parent[x];
        }

        public void Union(int x, int y)// Defining a public method named Union that takes two integer arguments named x and y and returns nothing
        {
            // Calling the Find method with x and y as an argument and assigning its return value to the rootX and rootY variable
            int rootX = Find(x);
            int rootY = Find(y);
            // Checking if the element at index rootX in the rank array is less than the element at index rootY in the rank array
            if (rank[rootX] < rank[rootY])
            {
                parent[rootX] = rootY;
            }
            // If it is less, assigning the value of rootY to the element at index rootX in the parent array
            else if (rank[rootX] > rank[rootY])
            {
                parent[rootY] = rootX;
            }
            // If neither of the previous conditions are true
            else
            {
                parent[rootY] = rootX;
                rank[rootX]++;
            }
        }
    }
}



