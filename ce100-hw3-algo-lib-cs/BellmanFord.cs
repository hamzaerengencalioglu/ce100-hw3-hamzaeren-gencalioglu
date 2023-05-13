using System;
using System.Collections.Generic;

/// @brief Bellman-Ford algorithm implementation for finding the shortest path in a graph.
/// @details This class represents the Bellman-Ford algorithm implementation.
public class BellmanFord
{
    private int verticesCount; ///< The number of vertices in the graph.
    private List<Edge1> edges; ///< The list of edges in the graph.

    /// @brief Constructs a BellmanFord object with the specified number of vertices.
    /// @param verticesCount The number of vertices in the graph.
    public BellmanFord(int verticesCount)
    {
        this.verticesCount = verticesCount;
        edges = new List<Edge1>();
    }

    /// @brief Adds an edge to the graph.
    /// @param source The source vertex of the edge.
    /// @param destination The destination vertex of the edge.
    /// @param weight The weight/cost of the edge.
    public void AddEdge(int source, int destination, int weight)
    {
        edges.Add(new Edge1(source, destination, weight));
    }

    /// @brief Finds the shortest path from a source vertex to a destination vertex.
    /// @param source The source vertex.
    /// @param destination The destination vertex.
    /// @return An array representing the shortest path from the source vertex to the destination vertex.
    public int[] FindShortestPath(int source, int destination)
    {
        int[] distances1 = new int[verticesCount];
        int[] previous = new int[verticesCount];

        // Step 1: Initialize distances
        for (int i = 0; i < verticesCount; i++)
        {
            distances1[i] = int.MaxValue;
            previous[i] = -1;
        }
        distances1[source] = 0;

        // Step 2: Relax edges repeatedly
        for (int i = 0; i < verticesCount - 1; i++)
        {
            foreach (Edge1 edge in edges)
            {
                if (distances1[edge.Source] != int.MaxValue && distances1[edge.Source] + edge.Weight < distances1[edge.Destination])
                {
                    distances1[edge.Destination] = distances1[edge.Source] + edge.Weight;
                    previous[edge.Destination] = edge.Source;
                }
            }
        }

        // Step 3: Check for negative-weight cycles
        foreach (Edge1 edge in edges)
        {
            if (distances1[edge.Source] != int.MaxValue && distances1[edge.Source] + edge.Weight < distances1[edge.Destination])
            {
                throw new Exception("Negative-weight cycle detected. Bellman-Ford algorithm cannot handle negative-weight cycles.");
            }
        }

        // Step 4: Reconstruct the shortest path
        List<int> path = new List<int>();
        int current = destination;
        while (current != -1)
        {
            path.Insert(0, current);
            current = previous[current];
        }

        return path.ToArray();
    }

    /// @brief Represents an edge in the graph.
    private class Edge1
    {
        public int Source { get; } ///< The source vertex of the edge.
        public int Destination { get; } ///< The destination vertex of the edge.
        public int Weight { get; } ///< The weight/cost of the edge.

        /// @brief Constructs an Edge object with the specified source, destination, and weight.
        /// @param source The source vertex of the edge.
        /// @param destination The destination vertex of the edge.
        /// @param weight The weight/cost of the edge.
        public Edge1(int source, int destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }
    }
}

