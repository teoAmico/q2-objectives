using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; set; }
    public bool IsEndOfWord { get; set; }

    public TrieNode()
    {
        Children = new Dictionary<char, TrieNode>();
        IsEndOfWord = false;
    }
}

class Trie
{
    public TrieNode root;

    public Trie()
    {
        root = new TrieNode();
    }

    public void Insert(string word)
    {
        TrieNode node = root;
        foreach (char ch in word)
        {
            if (!node.Children.ContainsKey(ch))
            {
                node.Children[ch] = new TrieNode();
            }
            node = node.Children[ch];
        }
        node.IsEndOfWord = true;
    }

    public void InsertRange(List<string> words)
    {
        foreach (string word in words)
        {
            Insert(word);
        }
    }

    public bool Search(string word)
    {
        TrieNode node = root;
        foreach (char ch in word)
        {
            if (!node.Children.ContainsKey(ch))
            {
                return false;
            }
            node = node.Children[ch];
        }
        return node.IsEndOfWord;
    }
}

class MatrixWordValidator
{
    public Trie dictionaryTrie;

    public MatrixWordValidator()
    {
        dictionaryTrie = new Trie();
    }

    public void BuildDictionary(List<string> dictionary)
    {
        dictionaryTrie.InsertRange(dictionary);
    }

    public (char[,], List<string>) ValidateWords(char[,] matrix, int rows, int cols)
    {
        List<string> foundWords = new List<string>();
        List<(int, int)> lettersToRemove = new List<(int, int)>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                ValidateWordsBFS(matrix, rows, cols, i, j, foundWords, lettersToRemove);
            }
        }

        // Create a new matrix with letters removed
        char[,] newMatrix = (char[,])matrix.Clone();

        //foreach (var (i, j) in lettersToRemove)
        //{
        //    newMatrix[i, j] = '\0';
        //}

        return (newMatrix, foundWords);
    }

    private void ValidateWordsBFS(char[,] matrix, int rows, int cols, int startRow, int startCol, List<string> foundWords, List<(int, int)> lettersToRemove)
    {
        // Use a boolean matrix to track visited positions
        bool[,] visited = new bool[rows, cols];

        Queue<(int, int, string, TrieNode)> queue = new Queue<(int, int, string, TrieNode)>();
        queue.Enqueue((startRow, startCol, matrix[startRow, startCol].ToString(), dictionaryTrie.root));
        visited[startRow, startCol] = true;

        while (queue.Count > 0)
        {
            var (i, j, currentWord, currentNode) = queue.Dequeue();

            if (currentNode.IsEndOfWord)
            {
                foundWords.Add(currentWord);

                for (int k = 0; k < currentWord.Length; k++)
                {
                    lettersToRemove.Add((i - k * (i - startRow), j - k * (j - startCol)));
                }
            }

            // Explore only valid neighbors that haven't been visited
            ExploreNeighbours(matrix, visited, rows, cols, i, j, currentWord, currentNode, queue);
        }
    }

    private void ExploreNeighbours(char[,] matrix, bool[,] visited, int rows, int cols, int i, int j, string currentWord, TrieNode currentNode, Queue<(int, int, string, TrieNode)> queue)
    {
        // Directions: up, right, down, left, diagonal up-right, diagonal down-right, diagonal down-left, diagonal up-left
        int[] dirX = { -1, 0, 1, 0, 1, 1, -1, -1 };
        int[] dirY = { 0, 1, 0, -1, 1, -1, -1, 1 };

        for (int d = 0; d < 8; d++)
        {
            int newRow = i + dirX[d];
            int newCol = j + dirY[d];

            // Check if the new position is within the bounds of the matrix
            // and if the letter at that position is a valid continuation in the trie
            if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols && !visited[newRow, newCol] && currentNode.Children.ContainsKey(matrix[newRow, newCol]))
            {
                visited[newRow, newCol] = true;
                queue.Enqueue((newRow, newCol, currentWord + matrix[newRow, newCol], currentNode.Children[matrix[newRow, newCol]]));
            }
        }
    }

    //public void ValidateWordsBFS(char[,] matrix, int rows, int cols, int startRow, int startCol, List<string> foundWords, List<(int, int)> lettersToRemove)
    //{
    //    Debug.Log("processing cell: " + rows +","+ cols);
    //    Queue<(int, int, string, TrieNode)> queue = new Queue<(int, int, string, TrieNode)>();
    //    queue.Enqueue((startRow, startCol, matrix[startRow, startCol].ToString(), dictionaryTrie.root));

    //    while (queue.Count > 0)
    //    {
    //        var (i, j, currentWord, currentNode) = queue.Dequeue();
    //        Debug.Log("processing: " + currentWord);
    //        if (currentNode.IsEndOfWord)
    //        {
    //            // Add the found word to the list
    //            foundWords.Add(currentWord);

    //            // Add the positions of letters to be removed
    //            for (int k = 0; k < currentWord.Length; k++)
    //            {
    //                lettersToRemove.Add((i - k * (i - startRow), j - k * (j - startCol)));
    //            }
    //        }

    //        matrix[i, j] = '\0'; // Mark the cell as visited

    //        ExploreNeighbours(matrix, rows, cols, i - 1, j, currentWord, currentNode, queue);
    //        ExploreNeighbours(matrix, rows, cols, i + 1, j, currentWord, currentNode, queue);
    //        ExploreNeighbours(matrix, rows, cols, i, j - 1, currentWord, currentNode, queue);
    //        ExploreNeighbours(matrix, rows, cols, i, j + 1, currentWord, currentNode, queue);

    //        matrix[i, j] = currentWord.Last(); // Backtrack - unmark the cell
    //    }
    //}



    //public void ExploreNeighbours(char[,] matrix, int rows, int cols, int i, int j, string currentWord, TrieNode currentNode, Queue<(int, int, string, TrieNode)> queue)
    //{
    //    // Directions: up, right, down, left, diagonal up-right, diagonal down-right, diagonal down-left, diagonal up-left
    //    int[] dirX = { -1, 0, 1, 0, 1, 1, -1, -1 };
    //    int[] dirY = { 0, 1, 0, -1, 1, -1, -1, 1 };

    //    for (int d = 0; d < 8; d++)
    //    {
    //        int newRow = i + dirX[d];
    //        int newCol = j + dirY[d];

    //        // Check if the new position is within the bounds of the matrix
    //        // and if the letter at that position is a valid continuation in the trie
    //        if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols && matrix[newRow, newCol] != '\0' && currentNode.Children.ContainsKey(matrix[newRow, newCol]))
    //        {
    //            queue.Enqueue((newRow, newCol, currentWord + matrix[newRow, newCol], currentNode.Children[matrix[newRow, newCol]]));
    //        }
    //    }
    //}
}

class Program
{
    static void Main()
    {
        List<string> dictionary = new List<string> { "cat", "cot", "coty", "cotys", "cots", "cotsy" };
        char[,] matrix = {
            { 'c', 'a', 't', 'y' },
            { 'o', 's', 't', 's' },
            { 't', 'o', 'c', 'o' },
            { 'y', 'c', 's', 't' }
        };

        MatrixWordValidator validator = new MatrixWordValidator();
        validator.BuildDictionary(dictionary);

        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        Console.WriteLine("Validating words in the matrix:");

        // Validate words and get new matrix and list of found words
        var (newMatrix, foundWords) = validator.ValidateWords(matrix, rows, cols);

        // Print new matrix
        Console.WriteLine("New Matrix:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(newMatrix[i, j] + " ");
            }
            Console.WriteLine();
        }

        // Print found words
        Console.WriteLine("Found Words:");
        foreach (var word in foundWords)
        {
            Console.WriteLine(word);
        }
    }
}


