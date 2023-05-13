using ce100_hw3_algo_lib_cs;
using System.Collections;
namespace ce100_hw3_algo_test_cs {
public class UnitTest {

  [Fact]
  public void HuffmanCoding_Txt_Test() {
    string mp3FilePath = "mp3-txt-folder/GoodTime-Input.mp3";
    string txtFilePath = "mp3-txt-folder/Txt-Input.txt";
    long mp3FileSize = new FileInfo(mp3FilePath).Length;
    string loremIpsum = Huffman.GenerateLoremIpsum(mp3FileSize);
    File.WriteAllText(txtFilePath, loremIpsum);
    string inputFilePath = "mp3-txt-folder/Txt-Input.txt";
    string compressedFilePath = "mp3-txt-folder/Txt-Compressed.bin";
    string decompressedFilePath = "mp3-txt-folder/Txt-Output.txt";
    string input = File.ReadAllText(inputFilePath);
    Huffman.HuffmanTree tree = new Huffman.HuffmanTree();
    tree.Build(input);
    BitArray encoded = tree.Encode(input);
    using (FileStream compressedFileStream = new FileStream(compressedFilePath, FileMode.Create)) {
      using (BinaryWriter writer = new BinaryWriter(compressedFileStream)) {
        Huffman.WriteBitArray(writer, encoded);
      }
    }
    using (FileStream compressedFileStream = new FileStream(compressedFilePath, FileMode.Open)) {
      using (BinaryReader reader = new BinaryReader(compressedFileStream)) {
        BitArray encodedFromFile = Huffman.ReadBitArray(reader, compressedFileStream.Length);
        string decoded = tree.Decode(encodedFromFile);
        File.WriteAllText(decompressedFilePath, decoded);
      }
    }
    string decompressed = File.ReadAllText(decompressedFilePath);
    Assert.Equal(input, decompressed);
  }


  [Fact]
  public void HuffmanCoding_Mp3_Test() {
    string inputFilePath = "mp3-txt-folder/GoodTime-Input.mp3";
    string compressedFilePath = "mp3-txt-folder/GoodTime-Compressed.bin";
    string decompressedFilePath = "mp3-txt-folder/GoodTime-Output.mp3";
    // Read the file contents
    byte[] input = File.ReadAllBytes(inputFilePath);
    // Create Huffman tree and encode the input file
    Huffman.HuffmanTree_mp3 tree = new Huffman.HuffmanTree_mp3();
    tree.Build(input);
    BitArray encoded = tree.Encode(input);
    // Write the encoded bits to binary file
    using (FileStream compressedFileStream = new FileStream(compressedFilePath, FileMode.Create)) {
      using (BinaryWriter writer = new BinaryWriter(compressedFileStream)) {
        Huffman.WriteBitArray(writer, encoded);
      }
    }
    // Read the compressed file and the encoded bits
    using (FileStream compressedFileStream = new FileStream(compressedFilePath, FileMode.Open)) {
      using (BinaryReader reader = new BinaryReader(compressedFileStream)) {
        BitArray encodedFromFile = Huffman.ReadBitArray(reader, compressedFileStream.Length);
        // Decode the encoded bits with Huffman tree
        byte[] decodedBytes = tree.Decode(encodedFromFile);
        // Write the decoded bytes as the original file
        File.WriteAllBytes(decompressedFilePath, decodedBytes);
      }
    }
    // Read the decompressed file and compare with the original file
    byte[] decompressedBytes = File.ReadAllBytes(decompressedFilePath);
    Assert.Equal(input, decompressedBytes);
  }


  [Fact]
  public void TestGetAssemblySteps() {
    // Select an IKEA product (Example: HEMNES bookcase)
    string name = "HEMNES bookcase";
    string link = "https://www.ikea.com/catalog/products/00245644/hemnes-bookcase/";
    // Define the parts and dependencies of the product (Example: A, B, C, D, E, F, G, H, I, J, K, L)
    List<string> A = new List<string>(); // A: Bottom part of the bookcase. No dependencies.
    List<string> B = new List<string>(); // B: Top part of the bookcase. No dependencies.
    List<string> C = new List<string>(); // C: Back part of the bookcase. No dependencies.
    List<string> D = new List<string>() {
      "A", "B"
    }; // D: Right side part of the bookcase. Dependencies: A and B.
    List<string> E = new List<string>() {
      "A", "B"
    }; // E: Left side part of the bookcase. Dependencies: A and B.
    List<string> F = new List<string>() {
      "D", "E"
    }; // F: Middle shelf of the bookcase. Dependencies: D and E.
    List<string> G = new List<string>() {
      "D", "E"
    }; // G: Bottom shelf of the bookcase. Dependencies: D and E.
    List<string> H = new List<string>() {
      "D", "E"
    }; // H: Top shelf of the bookcase. Dependencies: D and E.
    List<string> I = new List<string>() {
      "D", "G"
    }; // I: Right door of the bookcase. Dependencies: D and G.
    List<string> J = new List<string>() {
      "E", "G"
    }; // J: Left door of the bookcase. Dependencies: E and G.
    List<string> K = new List<string>() {
      "I", "G"
    }; // K: Right hinge of the bookcase. Dependencies: I and G.
    List<string> L = new List<string>() {
      "J", "G"
    }; // L: Left hinge of the bookcase. Dependencies: J and G.
    // IKEAAssemblyGuide sınıfından bir nesne oluştur
    IkeaAssemblyGuide guide = new IkeaAssemblyGuide(name, link);
    // Parçaları ve bağımlılıklarını nesneye ekle
    guide.AddPart("A", A);
    guide.AddPart("B", B);
    guide.AddPart("C", C);
    guide.AddPart("D", D);
    guide.AddPart("E", E);
    guide.AddPart("F", F);
    guide.AddPart("G", G);
    guide.AddPart("H", H);
    guide.AddPart("I", I);
    guide.AddPart("J", J);
    guide.AddPart("K", K);
    guide.AddPart("L", L);
    // Parçalar üzerinde topolojik sıralama yap
    guide.TopologicalSort();
    // Montaj işleminin adımlarını al
    ArrayList stepsArrayList = guide.GetAssemblySteps();
    // ArrayList'i List<string>'e dönüştür
    List<string> steps = new List<string>();

    foreach (var step in stepsArrayList) {
      steps.Add((string)step);
    }

    // Create a list of expected steps
    List<string> expectedSteps = new List<string>() {
      "Assemble part A",
      "Assemble part B",
      "Assemble part C",
      "Assemble part D",
      "Assemble part E",
      "Assemble part F",
      "Assemble part G",
      "Assemble part H",
      "Assemble part I",
      "Assemble part J",
      "Assemble part K",
      "Assemble part L"
    };
    Assert.Equal(expectedSteps, steps);
  }

       
            [Fact] // Applying the Fact attribute to the following method to indicate that it is a test method
            public void ConnectTrees_ValidTreeCount_GeneratesPipeline() // Defining a public method named ConnectTrees_ValidTreeCount_GeneratesPipeline that takes no arguments and returns nothing
            {
                // Arrange
                int treeCount = 15; // Declaring an integer variable named treeCount and initializing it with the value of 15
                pipelinesystems garden = new pipelinesystems(treeCount); // Creating a new pipelinesystems object named garden with treeCount as an argument

                // Act
                garden.ConnectTrees(); // Calling the ConnectTrees method on the garden object
                List<string> pipelineDescription = garden.GetPipelineDescription(); // Calling the GetPipelineDescription method on the garden object and assigning its return value to the pipelineDescription variable

                // Assert
                Assert.NotEmpty(pipelineDescription); // Asserting that the pipelineDescription list is not empty using the NotEmpty method of the Assert class
                Assert.Equal(treeCount - 1, pipelineDescription.Count); // Asserting that the value of treeCount - 1 is equal to the Count property of the pipelineDescription list using the Equal method of the Assert class
            }

            [Fact] // Applying the Fact attribute to the following method to indicate that it is a test method
            public void ConnectTrees_TooFewTrees_ThrowsArgumentException() // Defining a public method named ConnectTrees_TooFewTrees_ThrowsArgumentException that takes no arguments and returns nothing
            {
                // Arrange
                int treeCount = 5; // Declaring an integer variable named treeCount and initializing it with the value of 5

                // Act & Assert
                Assert.Throws<ArgumentException>(() => new pipelinesystems(treeCount)); // Asserting that calling the pipelinesystems constructor with treeCount as an argument throws an ArgumentException using the Throws method of the Assert class
            }
      
        
            
            
            [Fact]
            public void BelmannFord_Test()
            {
              
                // Create a graph with 5 vertices
                int verticesCount = 5;
                BellmanFord bellmanFord = new BellmanFord(verticesCount);

                // Add edges to the graph
                bellmanFord.AddEdge(0, 1, 6);
                bellmanFord.AddEdge(0, 2, 7);
                bellmanFord.AddEdge(1, 3, 5);
                bellmanFord.AddEdge(2, 3, -2);
                bellmanFord.AddEdge(3, 4, -4);

                // Find the shortest path from vertex 0 to vertex 4
                int[] shortestPath = bellmanFord.FindShortestPath(0, 4);

                // Validate the result
                int[] expectedPath = { 0, 2, 3, 4 };
                Assert.Equal(expectedPath, shortestPath);
                
            }
        
    }

}
