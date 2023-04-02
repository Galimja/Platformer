using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{
    public class MarshingSquareController
    {
        private Tilemap _tilemap;
        private Tile _tile;
        private SquareGrid _grid;

        public void GenerateGrid(int[,] map, float squareSize)
        {
            _grid = new SquareGrid(map, squareSize);
        }

        public void DrawTile(bool active, Vector3 position)
        {
            if (active)
            {
                Vector3Int tilePos = new Vector3Int((int)position.x, (int)position.y, 0);
                _tilemap.SetTile(tilePos, _tile);
            }
        }

        public void DrawTiles(Tilemap tilemapG, Tile ground)
        {
            if (_grid == null)
                return;

            _tile = ground;
            _tilemap = tilemapG;

            for (int x = 0; x < _grid.Squares.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.Squares.GetLength(1); y++)
                {
                    DrawTile(_grid.Squares[x, y].TL.active, _grid.Squares[x, y].TL.position);
                    DrawTile(_grid.Squares[x, y].TR.active, _grid.Squares[x, y].TR.position);
                    DrawTile(_grid.Squares[x, y].BL.active, _grid.Squares[x, y].BL.position);
                    DrawTile(_grid.Squares[x, y].BR.active, _grid.Squares[x, y].BR.position);
                }
            }
        }

    }

    public class Node
    {
        public Vector3 position;

        public Node(Vector3 pos)
        {
            position = pos;
        }
    }

    public class ControllNode : Node
    {
        public bool active;

        public ControllNode(Vector3 pos, bool active) :base(pos)
        {
            this.active = active;
        }
    }

    public class Square
    {
        public ControllNode TL, TR, BL, BR;

        public Square(ControllNode tl, ControllNode tr, ControllNode bl, ControllNode br)
        {
            TL = tl;
            TR = tr;
            BL = bl;
            BR = br;
        }
    }

    public class SquareGrid
    {
        public Square[,] Squares;

        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);

            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            float size = squareSize / 2;

            float width = -mapWidth / 2;
            float height = -mapHeight / 2;

            ControllNode[,] controllNodes = new ControllNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    Vector3 position = new Vector3(width + x * squareSize + size, height + y * squareSize + size, 0);
                    controllNodes[x, y] = new ControllNode(position, map[x, y] == 1);
                }
            }

            Squares = new Square[nodeCountX - 1, nodeCountY - 1];

            for (int x = 0; x < nodeCountX - 1; x++)
            {
                for (int y = 0; y < nodeCountY - 1; y++)
                {
                    Squares[x, y] = new Square(controllNodes[x, y + 1], controllNodes[x + 1, y], 
                                               controllNodes[x + 1, y + 1], controllNodes[x, y]);
                    
                }
            }
        }
    }
}