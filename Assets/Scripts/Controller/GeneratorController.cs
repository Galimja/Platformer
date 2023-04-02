using UnityEngine;
using UnityEngine.Tilemaps;


namespace PlatformerMVC
{
    public class GeneratorController
    {
        private Tilemap _tilemap;
        private Tile _tile;

        private int _mapHeight;
        private int _mapWidth;
        private int _fillPercent;
        private int _smoothPercent;
        
        private int[,] _map;

        private bool _borders;

        private MarshingSquareController _controller;

        public GeneratorController(GeneratorLevelView view)
        {
            _tilemap = view._tilemap;
            _tile = view._tile;
            _mapHeight = view._mapHeight;
            _mapWidth = view._mapWidth;
            _fillPercent = view._fillPercent;
            _smoothPercent = view._smoothPercent;
            _borders = view._borders;

            _map = new int[_mapWidth, _mapHeight];

            
        }

        public void Start()
        {
            FillMap();

            for (int i = 0; i < _smoothPercent; i++)
            {
                SmoothMap();
            }

            _controller = new MarshingSquareController();
            
            _controller.GenerateGrid(_map, 1);
            _controller.DrawTiles(_tilemap, _tile);

            //DrawTiles();
        }
         
        public void FillMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (_borders)
                    {
                        if (x == 0 || x == _mapWidth - 1 || y == 0 || y == _mapHeight - 1)
                        {
                            _map[x, y] = 1;
                            continue;
                        }
                    }
                    _map[x, y] = Random.Range(0, 100) < _fillPercent ? 1 : 0;
                    
                }
            }
        }
        
        public void SmoothMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    int neighbour = GetNeighbour(x, y);

                    if (neighbour > 4)
                    {
                        _map[x, y] = 1;
                    } else if (neighbour < 4)
                    {
                        _map[x, y] = 0;
                    }

                }
            }
            
        }

        public int GetNeighbour(int x, int y)
        {
            int count = 0;


            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < _mapWidth && j >= 0 && j < _mapHeight)
                    {
                        if (i != x || j != y)
                        {
                            count += _map[i, j];
                        }
                    } else
                    {
                        count++;
                    }
                       
                    
                }
            }

            return count;
        }

        public void DrawTiles()
        {
            if (_map == null) return;

            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (_map[x, y] == 1)
                    {
                        Vector3Int tilePos = new Vector3Int(-_mapWidth / 2 + x, _mapHeight / 2 + y, 0);

                        _tilemap.SetTile(tilePos, _tile);
                    }

                }
            }
        }

        void Update()
        {

        }
    }
}