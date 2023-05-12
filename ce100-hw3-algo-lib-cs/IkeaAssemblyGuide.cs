using System.Collections;


/**
* @name IkeaAssemblyGuide
* 
* @brief Construct how to assemble pieces of furniture using IkeaAssemblyGuide.
*
* @param Creates assembly guide for assembling parts using IkeaAssemblyGuide.
**/
public class IkeaAssemblyGuide
{
    // Product name and link
    private string productName;
    private string productLink;


    // A dictionary of the product's parts and dependencies
    private Dictionary<string, List<string>> parts;

    // A stack that holds the assembly sequence of parts
    private Stack<string> order;

    // An ArrayList holding the steps of the assembly process
    private ArrayList steps;

    // Constructor method
    public IkeaAssemblyGuide(string name, string link)
    {
        productName = name;
        productLink = link;
        parts = new Dictionary<string, List<string>>();
        order = new Stack<string>();
        steps = new ArrayList();
    }

    // Method to add a fragment and its dependencies to the dictionary
    public void AddPart(string part, List<string> dependencies)
    {
        parts.Add(part, dependencies);
    }

    // Method that performs topological sorting on fragments with DFS
    public void TopologicalSort()
    {
        // A set that holds visited fragments
        HashSet<string> visited = new HashSet<string>();

        // Call DFS for each item in the dictionary
        foreach (string part in parts.Keys)
        {
            DFS(part, visited);
        }
    }

    // Helper method that does DFS for a part
    private void DFS(string part, HashSet<string> visited)
    {
        // Returns if the track has been visited.
        if (visited.Contains(part))
        {
            return;
        }

        // Marks the track as visited.
        visited.Add(part);

        // If the fragment has dependencies, it calls DFS for them.
        if (parts[part].Count > 0)
        {
            foreach (string dependency in parts[part])
            {
                DFS(dependency, visited);
            }
        }

        // Adds the fragment to the stack.
        order.Push(part);
    }

    // Method that returns the steps of the assembly process in an ArrayList with textual descriptions
    public ArrayList GetAssemblySteps()
    {
        // Creates a step for each item in the stack and adds them to the ArrayList in reverse order.
        ArrayList steps = new ArrayList();

        while (order.Count > 0)
        {
            string part = order.Pop();
            string step = "Assemble part " + part;
            steps.Insert(0, step);
        }

        // Returns the inverted ArrayList.
        return steps;
    }


}