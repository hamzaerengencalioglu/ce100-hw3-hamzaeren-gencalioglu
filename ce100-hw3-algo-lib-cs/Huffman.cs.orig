using System.Collections;


namespace ce100_hw3_algo_lib_cs {

    /**
     * @name Huffman
     * 
     * @brief Build the Huffman tree based on the source text.
     *
     * @param Source The source text used to build the Huffman tree.
     **/

    public class Huffman {

  public class Node {
    public char Symbol {
      get;
      set;
    }
    public int Frequency {
      get;
      set;
    }
    public Node Right {
      get;
      set;
    }
    public Node Left {
      get;
      set;
    }

    public List<bool> Traverse(char symbol, List<bool> data) {
      if (Right == null && Left == null) {
        // If the symbol is equal to this node's symbol, return the data.
        if (symbol.Equals(this.Symbol)) {
          return data;
        }
        // If the symbol is not equal to this node's symbol, return null.
        else {
          return null;
        }
      } else {
        // Defines variables to hold left and right sub-branches.
        List<bool> left = null;
        List<bool> right = null;

        // If there is a left lower branch
        if (Left != null) {
          // Creates the left path.
          List<bool> leftPath = new List<bool>();
          leftPath.AddRange(data);
          leftPath.Add(false);
          // Performs a symbol search on the lower left branch.
          left = Left.Traverse(symbol, leftPath);
        }

        // If there is a right lower branch
        if (Right != null) {
          // Creates the right path.
          List<bool> rightPath = new List<bool>();
          rightPath.AddRange(data);
          rightPath.Add(true);
          // Starts searching for the symbol in the lower right branch.
          right = Right.Traverse(symbol, rightPath);
        }

        // Returns the left path if it found the left sub-branch.
        if (left != null) {
          return left;
        }
        // Returns the right path if found in the right sub-branch.
        else {
          return right;
        }
      }
    }
  }
  public static void WriteBitArray(BinaryWriter writer, BitArray bits) {
    // Converts bit array to byte array.
    byte[] bytes = new byte[(bits.Length + 7) / 8];
    bits.CopyTo(bytes, 0);
    // Prints the byte array to the printer.
    writer.Write(bytes);
  }

  public static BitArray ReadBitArray(BinaryReader reader, long byteCount) {
    // List to hold bits.
    List<bool> bits = new List<bool>();
    // Reads the byte array from the reader.
    byte[] bytes = reader.ReadBytes((int)byteCount);

    // For each byte
    foreach (byte b in bytes) {
      // Checks 8 bits one by one.
      for (int i = 0; i < 8; i++) {
        // If bit is 1, it adds true to the list.
        bits.Add((b & (1 << i)) != 0);
      }
    }

    // Creates and returns an array of bits.
    return new BitArray(bits.ToArray());
  }
  public class HuffmanTree {
    private List<Node> nodes = new List<Node>();
    public Node Root {
      get;
      set;
    }
    public Dictionary<char, int> Frequencies = new Dictionary<char, int>();

    public void Build(string source) {
      // For each character of the source text
      for (int i = 0; i < source.Length; i++) {
        // If it is not in the dictionary, appends it and sets its frequency to 0.
        if (!Frequencies.ContainsKey(source[i])) {
          Frequencies.Add(source[i], 0);
        }

        // Increase the frequency by one.
        Frequencies[source[i]]++;
      }

      // For each symbol in the dictionary
      foreach (KeyValuePair<char, int> symbol in Frequencies) {
        // Creates a node and adds it to the list.
        nodes.Add(new Node() {
          Symbol = symbol.Key, Frequency = symbol.Value
        });
      }

      // Until the list is single element
      while (nodes.Count > 1) {
        // Sorts the list by frequency.
        List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

        // If there are at least two elements
        if (orderedNodes.Count >= 2) {
          // Gets the first two elements.
          List<Node> taken = orderedNodes.Take(2).ToList<Node>();
          // Creates the common parent of these two elements.
          Node parent = new Node() {
            Symbol = '*',
            Frequency = taken[0].Frequency + taken[1].Frequency,
            Left = taken[0],
            Right = taken[1]
          };
          // Removes the received elements from the list.
          nodes.Remove(taken[0]);
          nodes.Remove(taken[1]);
          // Ebeveyni listeye ekler.
          nodes.Add(parent);
        }

        // Updates the root node.
        this.Root = nodes.FirstOrDefault();
      }
    }

    public BitArray Encode(string source) {
      // List to hold encoded text
      List<bool> encodedSource = new List<bool>();

      // For each character of the source text
      for (int i = 0; i < source.Length; i++) {
        // Finds the encoded version of the character in the tree.
        List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
        // Adds the encoded character to the list.
        encodedSource.AddRange(encodedSymbol);
      }

      // Converts list elements to a bit array.
      BitArray bits = new BitArray(encodedSource.ToArray());
      // Returns the bit array.
      return bits;
    }

    public string Decode(BitArray bits) {
      // Variable to hold the current node
      Node current = this.Root;
      // Decoded text
      string decoded = "";

      // For each bit of the bit array
      foreach (bool bit in bits) {
        // If bit is 1, it goes to the lower right branch.
        if (bit) {
          if (current.Right != null) {
            current = current.Right;
          }
        }
        // If bit is 0, it goes to the lower left branch.
        else {
          if (current.Left != null) {
            current = current.Left;
          }
        }

        // If the leaf has arrived at the node
        if (IsLeaf(current)) {
          // Adds the node's symbol to the text.
          decoded += current.Symbol;
          // Resets the current node to the root.
          current = this.Root;
        }
      }

      // Returns the decoded text.
      return decoded;
    }

    public bool IsLeaf(Node node) {
      // Checks if the node is a leaf.
      // If it has no left and right child branches, it is a leaf
      return (node.Left == null && node.Right == null);
    }

  }
        /**
     * @name HuffmanTree_mp3
     * 
     * @brief Build the Huffman tree based on the source mp3.
     *
     * @param Source The source mp3 used to build the Huffman tree.
     **/

        public class Node_mp3 {
    public byte Symbol {
      get;
      set;
    }
    public int Frequency {
      get;
      set;
    }
    public Node_mp3 Left {
      get;
      set;
    }
    public Node_mp3 Right {
      get;
      set;
    }

    public List<bool> Traverse_mp3(byte? symbol, List<bool> data) {
      if (Left == null && Right == null) {
        // Returns the data if the symbol is equal to this node's symbol.
        if (Symbol == symbol) {
          return data;
        }
        // If not, returns null.
        else {
          return null;
        }
      } else {
        // Variables to hold left and right sub-branches
        List<bool> left = null;
        List<bool> right = null;

        // If there is a left lower branch
        if (Left != null) {
          // Creates the left path.
          List<bool> leftPath = new List<bool>();
          leftPath.AddRange(data);
          leftPath.Add(false);
          // Searches for the symbol in the lower left branch.
          left = Left.Traverse_mp3(symbol, leftPath);
        }

        // If there is a lower right branch
        if (Right != null) {
          // Creates the right path.
          List<bool> rightPath = new List<bool>();
          rightPath.AddRange(data);
          rightPath.Add(true);
          // Searches for the symbol in the lower right branch.
          right = Right.Traverse_mp3(symbol, rightPath);
        }

        // Returns the left path if it found the left sub-branch.
        if (left != null) {
          return left;
        }
        // Returns the right path if found in the right sub-branch.
        else {
          return right;
        }
      }
    }
  }

  public class HuffmanTree_mp3 {
    private List<Node_mp3> nodes = new List<Node_mp3>();
    public Node_mp3 Root {
      get;
      set;
    }
    public Dictionary<byte, int> Frequencies = new Dictionary<byte, int>();

    public void Build(byte[] source) {
      // For each byte of the source array
      for (int i = 0; i < source.Length; i++) {
        // If it is not in the dictionary, add it and set its frequency to 0.
        if (!Frequencies.ContainsKey(source[i])) {
          Frequencies.Add(source[i], 0);
        }

        // Increases the frequency by one.
        Frequencies[source[i]]++;
      }

      // For each symbol in the dictionary
      foreach (KeyValuePair<byte, int> symbol in Frequencies) {
        // Create a node and add it to the list.
        nodes.Add(new Node_mp3() {
          Symbol = symbol.Key, Frequency = symbol.Value
        });
      }

      // Until the list is single element
      while (nodes.Count > 1) {
        // Sorts the list by frequency.
        List<Node_mp3> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node_mp3>();

        // If there are at least two elements
        if (orderedNodes.Count >= 2) {
          // Gets the first two elements.
          List<Node_mp3> taken = orderedNodes.Take(2).ToList<Node_mp3>();
          // Creates the common parent of these two elements.
          Node_mp3 parent = new Node_mp3() {
            Symbol = byte.MaxValue, // The parent has no symbol.
            Frequency = taken[0].Frequency + taken[1].Frequency,
            Left = taken[0],
            Right = taken[1]
          };
          // Removes the received elements from the list.
          nodes.Remove(taken[0]);
          nodes.Remove(taken[1]);
          // Adds the parent to the list.
          nodes.Add(parent);
        }

        // Updates the root node.
        this.Root = nodes.FirstOrDefault();
      }
    }
    public BitArray Encode(byte[] source) {
      // List to hold the encoded array
      List<bool> encodedSource = new List<bool>();

      // For each byte of the source array
      for (int i = 0; i < source.Length; i++) {
        // Finds the encoded byte in the tree.
        List<bool> encodedSymbol = this.Root.Traverse_mp3(source[i], new List<bool>());
        // Add the encoded byte to the list
        encodedSource.AddRange(encodedSymbol);
      }

      // Converts list elements to a bit array.
      BitArray bits = new BitArray(encodedSource.ToArray());
      // Returns the bit array.
      return bits;
    }

    public byte[] Decode(BitArray bits) {
      // Variable to hold the current node
      Node_mp3 current = this.Root;
      // List to hold the decoded array
      List<byte> decoded = new List<byte>();

      // For each bit of the bit array
      foreach (bool bit in bits) {
        // If bit is 1, it goes to the lower right branch.
        if (bit) {
          if (current.Right != null) {
            current = current.Right;
          }
        }
        // If bit is 0, it goes to the lower left branch.
        else {
          if (current.Left != null) {
            current = current.Left;
          }
        }

        // If the leaf has arrived at the node
        if (IsLeaf(current)) {
          // Adds the node's symbol to the list.
          decoded.Add(current.Symbol);
          // Resets the current node to the root.
          current = this.Root;
        }
      }

      // Returns the decoded array.
      return decoded.ToArray();
    }

    public bool IsLeaf(Node_mp3 node) {
      // Checks if the node is a leaf.
      // If it has no left or right child branches, it is a leaf.
      return (node.Left == null && node.Right == null);
    }
  }

        /**
         * @name GenerateLoremIpsum
         * 
         * @brief Generates Lorem Ipsum text with the specified length.
         *
         * @param Source The source text used to build the Huffman tree.
         * 
         * @param The generated Lorem Ipsum text.
         **/
        public static string GenerateLoremIpsum(long length) {
    // Defines a variable to hold the text Lorem ipsum.
    string loremIpsumText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                            "Sed non risus. Suspendisse lectus tortor, dignissim sit amet, adipiscing nec, ultricies sed, dolor. " +
                            "Cras elementum ultrices diam. Maecenas ligula massa, varius a, semper congue, euismod non, mi.";
    // Calculates how many times the text should be repeated.
    int repeatCount = (int)Math.Ceiling((double)length / loremIpsumText.Length);
    // Variable to hold repeated text
    string loremIpsum = "";

    // Repeats the text the desired number of times.
    for (int i = 0; i < repeatCount; i++) {
      loremIpsum += loremIpsumText;
    }

    // Truncates the text to the desired length.
    loremIpsum = loremIpsum.Substring(0, (int)length);
    // Returns the text.
    return loremIpsum;
  }


}
}