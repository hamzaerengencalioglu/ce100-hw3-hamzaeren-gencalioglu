using ce100_hw3_algo_lib_cs;
using System.Collections;


namespace ce100_hw3_algo_test_cs {
public class UnitTest {

  [Fact]
  public void HuffmanCoding_Txt_Test() {
    string mp3FilePath = "D:\\a\\ce100-hw3-hamzaeren-gencalioglu\\ce100-hw3-hamzaeren-gencalioglu\\ce100-hw3-algo-test-cs\\bin\\Release\\net7.0\\GoodTime_Input.mp3";
    string txtFilePath = "Txt_Input.txt";
    long mp3FileSize = new FileInfo(mp3FilePath).Length;
    string loremIpsum = Huffman.GenerateLoremIpsum(mp3FileSize);
    File.WriteAllText(txtFilePath, loremIpsum);
    string inputFilePath = "Txt_Input.txt";
    string compressedFilePath = "Txt_Compressed.bin";
    string decompressedFilePath = "Txt_Output.txt";
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
    string inputFilePath = "GoodTime_Input.mp3";
    string compressedFilePath = "GoodTime_Compressed.bin";
    string decompressedFilePath = "GoodTime_Output.mp3";
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
    // Alınan adımların beklenen adımlarla eşit olduğunu kontrol et
    Assert.Equal(expectedSteps, steps);
  }
}

}
