using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Linq;

namespace Match3
{
    public class Match3 : MonoBehaviour
    {
        [SerializeField] int width = 8;
        [SerializeField] int height = 8;
        [SerializeField] float cellSize = 1f;
        [SerializeField] Vector3 originPosition = Vector3.zero;
        [SerializeField] bool debug = true;

        [SerializeField] Gem gemPrefab;
       
        GridSystem2D<GridObject<Gem>> grid;

        InputReader inputReader;
        [SerializeField] Ease ease = Ease.InQuad;
        Vector2Int selectedGem = Vector2Int.one * -1;

        System.Random random;
        List<char> letterStack;
        MatrixWordValidator validator;

        void Awake()
        {
            inputReader = GetComponent<InputReader>();
        }


        void Start()
        {
            InitializeGrid();
            inputReader.Fire += OnSelectGem;
        }

        void OnDestroy()
        {
            inputReader.Fire -= OnSelectGem;
        }

        void OnSelectGem()
        {
            var gridPos = grid.GetXY(Camera.main.ScreenToWorldPoint(inputReader.Selected));

            if (!IsValidPosition(gridPos) || IsEmptyPosition(gridPos)) return;

            if (selectedGem == gridPos)
            {
                DeselectGem();

            }
            else if (selectedGem == Vector2Int.one * -1)
            {
                SelectGem(gridPos);

            }
            else
            {
                StartCoroutine(RunGameLoop(selectedGem, gridPos));
            }
        }

        void DeselectGem() => selectedGem = new Vector2Int(-1, -1);
        void SelectGem(Vector2Int gridPos) => selectedGem = gridPos;
        bool IsEmptyPosition(Vector2Int gridPosition) => grid.GetValue(gridPosition.x, gridPosition.y) == null;

        bool IsValidPosition(Vector2 gridPosition)
        {
            return gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height;
        }

        IEnumerator RunGameLoop(Vector2Int gridPosA, Vector2Int gridPosB)
        {
            yield return StartCoroutine(SwapGems(gridPosA, gridPosB));

            //// Matches?
            //List<Vector2Int> matches = FindMatches();
            //// TODO: Calculate score
            //// Make Gems explode
            //yield return StartCoroutine(ExplodeGems(matches));
            //// Make gems fall
            //yield return StartCoroutine(MakeGemsFall());
            //// Fill empty spots
            //yield return StartCoroutine(FillEmptySpots());

            //// TODO: Check if game is over

            DeselectGem();
        }

        IEnumerator SwapGems(Vector2Int gridPosA, Vector2Int gridPosB)
        {
            var gridObjectA = grid.GetValue(gridPosA.x, gridPosA.y);
            var gridObjectB = grid.GetValue(gridPosB.x, gridPosB.y);

            // See README for a link to the DOTween asset
            gridObjectA.GetValue().transform
                .DOLocalMove(grid.GetWorldPositionCenter(gridPosB.x, gridPosB.y), 0.5f)
                .SetEase(ease);
            gridObjectB.GetValue().transform
                .DOLocalMove(grid.GetWorldPositionCenter(gridPosA.x, gridPosA.y), 0.5f)
                .SetEase(ease);

            grid.SetValue(gridPosA.x, gridPosA.y, gridObjectB);
            grid.SetValue(gridPosB.x, gridPosB.y, gridObjectA);

            yield return new WaitForSeconds(0.5f);
        }


        void InitializeGrid()
        {
            //init random generator 
            random = new System.Random();
            //init stack for letters
            letterStack = new List<char>();
            //load dictionary into json array
            List<string> dictionary = FileHandler.ReadFromJSON("",GameSettings.FILENAME_ENG_DICTIONARY_JSON);
            validator = new MatrixWordValidator();
            validator.BuildDictionary(dictionary);

            //populate the letter stack
            GenerateMatrixFromJsonArray(dictionary);


            //init the grid
            grid = GridSystem2D<GridObject<Gem>>.VerticalGrid(width, height, cellSize, originPosition, debug);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    CreateGem(x, y);
                }
            }
        }

        void CreateGem(int x, int y)
        {
            Vector3 coord = grid.GetWorldPositionCenter(x, y);
            var gem = Instantiate(gemPrefab, coord, Quaternion.identity, transform);

            //pull the letter from the letter stack
            char randomLetter = PullLetterFromStack();

            gem.setLetter(randomLetter.ToString(), coord);
            var gridObject = new GridObject<Gem>(grid, x, y);
            gridObject.SetValue(gem);
            grid.SetValue(x, y, gridObject);
        }

        static char[,] ConvertListToCharArray(List<char> charList, int rows, int cols)
        {
            char[,] charArray = new char[rows, cols];

            int index = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    charArray[i, j] = charList[index];
                    index++;
                }
            }

            return charArray;
        }

        public void GenerateMatrixFromJsonArray(List<string> words)
        {
   
            int matrixLength = width * height;

            for (int i = 0; i < matrixLength; i++)
            {
                int randomIndex = random.Next(0, words.Count);
                string selectedValue = words[randomIndex];

                // Count the letters and add to the stack
                foreach (char letter in selectedValue)
                {
                    letterStack.Add(letter);
                }
            }

            // Shuffle the letter stack
            ShuffleLetterStack();

            char[,] matrix = ConvertListToCharArray(letterStack, width, height );
            Debug.Log(matrix);
            //todo validate the letter stack or shuffle again
            // Validate words and get new matrix and list of found words
            var (newMatrix, foundWords) = validator.ValidateWords(matrix, width, height);

            //// Print new matrix
            //Debug.Log("New Matrix:");
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Debug.Log(newMatrix[i, j] + " " + i +","+j);
                }

            }

            //// Print found words
            //Debug.Log("Found Words:");
            foreach (var word in foundWords)
            {
                Debug.Log(word);
            }


        }

        private void ShuffleLetterStack()
        {
            int n = letterStack.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                char value = letterStack[k];
                letterStack[k] = letterStack[n];
                letterStack[n] = value;
            }
        }

        public char PullLetterFromStack()
        {
            if (letterStack.Count > 0)
            {
                char letter = letterStack[letterStack.Count - 1];
                letterStack.RemoveAt(letterStack.Count - 1);
                return letter;
            }
            else
            {
                return 'x';
            }
        }

    }
}